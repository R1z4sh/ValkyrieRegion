using UnityEngine;

//シーン引継ぎ用インターフェイス

public class TitleScene : SceneBase {
  public override void Initialize(SceneData data = null) {
  }
  public override void Fainalize() { }

  private void Update() {
    if (Input.GetMouseButtonDown(0))
      GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }
}
