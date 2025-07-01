using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ApiData<T> {
  public int code;
  public string err_msg;
  public T result;
}

public class ApiClient :MonoBehaviour {
  static ApiClient instance = null;
  public static ApiClient Instance() {
    return instance;
  }

  private void Awake() {
    instance = this;
  }

  public async Task<T> Fetch<T>(string url, WWWForm data) {
    try {
      using(UnityWebRequest www = UnityWebRequest.Post(url, data)) {
        Debug.Log("Sending request to: " + url);

        // SendWebRequest()を 'await' で待つ
        // これにより、コルーチンの yield return と同様の非同期待機が実現される
        var asyncOperation = www.SendWebRequest();

        while(!asyncOperation.isDone) {
          // 任意で進捗などをここで確認できる
          await Task.Yield(); // メインスレッドをブロックせず、1フレーム待つ
        }

        if(www.result != UnityWebRequest.Result.Success) {
          Debug.LogError($"API Error: {www.error}");
          throw new Exception($"API Error: {www.error}");
        } else {
          string resultJson = www.downloadHandler.text;
          Debug.Log("API Success: " + resultJson);
          ApiData<T> response = JsonUtility.FromJson<ApiData<T>>(resultJson);
          return response.result;
        }
      }
    }
    catch(Exception ex) {
      Debug.LogError($"An exception occurred during API call: {ex.Message}");
      throw;
    }
  }

  private IEnumerator Execute<T>(string url, WWWForm data, Action<T> success) {
    UnityWebRequest www = UnityWebRequest.Post(url, data);
    Debug.Log("Sending request to: " + url);
    yield return www.SendWebRequest();
    if(www.result != UnityWebRequest.Result.Success) {
      Debug.LogError($"API Error: {www.error}");
    } else {
      string result = www.downloadHandler.text;
      Debug.Log("API Success: " + result);
      ApiData<T> response = JsonUtility.FromJson<ApiData<T>>(result);
      success?.Invoke(response.result);
    }
  }
}
