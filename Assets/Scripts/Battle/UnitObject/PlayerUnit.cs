using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit :UnitBase {
  private Rigidbody2D rb = null;
  public override void Initialize(
    PlayerUnitController playerUnitController,
    EnemyUnitController enemyUnitController,
    int unitId, int lv) {
    base.Initialize(playerUnitController, enemyUnitController, unitId, lv);
    string unitIdPath = string.Format("{0:D4}", unitId);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
    SetUpMove();
    rb = GetComponent<Rigidbody2D>();
    rb.freezeRotation = true;
  }

  void SetUpMove() {
    switch(status.Action()) {
      case 0:
      case 3:
        actionFlowController.To((int)AllyUnitAct.CommonMove);
        break;
      case 2:
        actionFlowController.To((int)AllyUnitAct.TowerMove);
        break;
    }
  }

  protected override void OnDead() {
    EventManager.Trigger<PlayerUnit>("PlayerUnitDead", this);
    actionFlowController.To((int)AllyUnitAct.Dead);
  }
}
