using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class GameManager :MonoBehaviour {
  void Start() {
    WWWForm form = new WWWForm();
    form.AddField("user", "devuser");
    form.AddField("pass", "your_password");
    ApiClient.Instance().Fetch<MasterDataWrapper>("http://163.43.218.37/api/dataload", form, MasterManager.LoadSeverData);
    GameSceneManager.Instance().ChangeScene(SceneName.Title);
  }


  private void Data(string data) {
    Debug.Log("çÏê¨ÉfÅ[É^" + data);
  }
}
