using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class GameManager :MonoBehaviour {
  void Start() {
    MasterManager.LoadSeverData();
    GameSceneManager.Instance().ChangeScene(SceneName.Title);
  }

}
