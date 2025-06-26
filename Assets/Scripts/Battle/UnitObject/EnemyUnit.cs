using UnityEngine;

public class EnemyUnit :UnitBase {

  private bool lastUnit = false;

  public void SetLastUnit(bool flag) {
    lastUnit = flag;
  }

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
    if(lastUnit) EventManager.Trigger<bool>("gameClear", true);
  }
}
