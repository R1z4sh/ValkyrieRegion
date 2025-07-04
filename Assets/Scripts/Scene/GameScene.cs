using System.Threading.Tasks;
using UnityEngine;

public class BattleData :SceneData {
  public int stageId;
}

public class GameScene :SceneBase {
  [SerializeField] private BattleController battleController;
  private BattleData battleData = null;
  public override async Task Initialize(SceneData data = null) {
    if(data != null) battleData = data as BattleData;
    battleController.Initialize(battleData.stageId);
  }
}
