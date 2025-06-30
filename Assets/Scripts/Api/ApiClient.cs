using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class ApiClient :MonoBehaviour {
  static ApiClient instance = null;
  public static ApiClient Instance() {
    return instance;
  }

  private void Awake() {
    instance = this;
  }

  public void Fetch<T>(string url, WWWForm data, Action<T> onSuccess) {
    StartCoroutine(Execute<T>(url, data, onSuccess));
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
