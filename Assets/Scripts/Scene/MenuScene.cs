using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : SceneBase {

  public Button button;
  //public DoubleTapButton button;

  public override void Initialize(SceneData data = null) {
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeSceneAsync(SceneName.Game));
    //button.SetOnClickEvent(() => ShowPopup());
  }


  public override void Fainalize() { }

  private void Update() {

  }
}
