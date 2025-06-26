using UnityEngine;

public class EnemyUnit :UnitBase {
  public override void Initialize(
   PlayerUnitController playerUnitController,
   EnemyUnitController enemyUnitController,
   int unitId, int lv) {
    base.Initialize(playerUnitController, enemyUnitController, unitId, lv);
    string unitIdPath = string.Format("{0:D4}", unitId);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Enemy" + unitIdPath);
    actionFlowController.To((int)EnemyUnitAct.CommonMove);
  }


  protected override void OnDead() {
    EventManager.Trigger<EnemyUnit>("EnemyUnitDead", this);
  }
}
