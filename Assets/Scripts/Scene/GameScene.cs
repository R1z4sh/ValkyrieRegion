using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : SceneBase {
  [SerializeField] private BattleController battleController;
  public override void Initialize(SceneData data = null) {
    battleController.Initialize();
  }
}
