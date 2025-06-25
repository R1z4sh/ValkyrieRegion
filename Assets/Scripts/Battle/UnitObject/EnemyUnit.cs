using UnityEngine;

public class EnemyUnit :UnitBase {
  private PlayerUnit targetUnit = null;

  public override void Initialize(
   PlayerUnitController playerUnitController,
   EnemyUnitController enemyUnitController,
   int unitId, int lv) {
    base.Initialize(playerUnitController, enemyUnitController, unitId, lv);
    string unitIdPath = string.Format("{0:D4}", unitId);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Enemy" + unitIdPath);
  }

  protected override void Move() {
    base.Move();
    UpdateTargetUnit();
    Vector3 targetPos = targetUnit != null ? targetUnit.transform.position : Tower.Instance().transform.position;
    float distance = Vector3.Distance(targetPos, transform.position);
    if(distance <= status.MaxAttackRange()) {
      unitActionStatus |= (int)UnitActionStatus.Attack;
      return;
    }

    if(targetUnit != null) {
      Vector3 direction = (targetUnit.transform.position - transform.position).normalized;

      transform.position += direction * status.Move() * Time.deltaTime;
    } else {
      Vector3 direction = (Tower.Instance().transform.position - transform.position).normalized;
      transform.position += direction * status.Move() * Time.deltaTime;
    }
  }

  private void UpdateTargetUnit() {
    float range = float.MaxValue;
    foreach(PlayerUnit unit in playerUnitController.AlliveUnits()) {
      float distance = Vector3.Distance(unit.transform.position, transform.position);
      if(distance < range) {
        range = distance;
        targetUnit = unit;
      }
    }
  }
}
