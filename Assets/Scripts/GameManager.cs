using UnityEngine;


public class GameManager :MonoBehaviour {
  async void Start() {
    await MasterManager.LoadSeverData();
    GameSceneManager.Instance().ChangeScene(SceneName.Title);
  }
}
