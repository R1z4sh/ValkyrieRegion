using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : SceneBase {
    [SerializeField] private UiBattle uiBattle;
  public override void Initialize(SceneData data = null) {
        uiBattle.Initialize();
    }
}
