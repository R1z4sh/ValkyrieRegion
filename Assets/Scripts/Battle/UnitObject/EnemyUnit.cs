using System.Threading.Tasks;
using UnityEngine;

public class EnemyUnit : UnitBase {
  private Rigidbody2D rb = null;

  public override void Initialize(
   PlayerUnitController playerUnitController ,
   EnemyUnitController enemyUnitController ,
   int unitId , int lv) {
    base.Initialize(playerUnitController , enemyUnitController , unitId , lv);
    string unitIdPath = string.Format("{0:D4}" , unitId);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Enemy" + unitIdPath);
    SetUpMove();
    rb = GetComponent<Rigidbody2D>();
    rb.freezeRotation = true;
  }

  void SetUpMove() {
    switch (status.Action()) {
      case 0:
      case 3:
        actionFlowController.To((int)EnemyUnitAct.CommonMove);
        break;
      case 2:
        actionFlowController.To((int)EnemyUnitAct.TowerMove);
        break;
    }
  }

  protected override async Task OnDead() {
    EventManager.Trigger<EnemyUnit>("enemyUnitDead" , this);
    actionFlowController.Dead();
    //アニメーション待機
    await Task.Delay(1);
    Destroy(this.gameObject);
  }
}
