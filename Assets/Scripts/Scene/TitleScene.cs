using UnityEngine;

//�V�[�����p���p�C���^�[�t�F�C�X

public class TitleScene : SceneBase {
  public override void Initialize(SceneData data = null) {
  }
  public override void Fainalize() { }

  private void Update() {
    if (Input.GetMouseButtonDown(0))
      GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }
}
