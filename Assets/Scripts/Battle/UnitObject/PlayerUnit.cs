using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit :UnitBase {
  EnemyUnit targetUnit = null;

  public override void Initialize(
    PlayerUnitController playerUnitController,
    EnemyUnitController enemyUnitController,
    int unitId, int lv) {
    base.Initialize(playerUnitController, enemyUnitController, unitId, lv);
    string unitIdPath = string.Format("{0:D4}", unitId);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
  }

  protected override void Move() {
    UpdateTargetUnit();
    if(targetUnit == null) return;
    Vector3 targetPos = targetUnit.transform.position;
    float distance = Vector3.Distance(targetPos, transform.position);
    if(distance <= status.MaxAttackRange()) {
      unitActionStatus = (int)UnitActionStatus.Attack;
      return;
    }
    Vector3 direction = (targetUnit.transform.position - transform.position).normalized;
    transform.position += direction * status.Move() * Time.deltaTime;
  }

  protected override void Attack() {
    if(this.targetUnit != null && attackCoolTime <= 0f) {
      //TODO:攻撃クラスを作成しターゲットを指定、その敵に時間をかけてダメージをヒットさせる
      this.targetUnit.OnDamage(status.Offense());
      attackCoolTime = status.AttackCool();
    }
  }

  private void UpdateTargetUnit() {
    float range = float.MaxValue;
    foreach(EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      float distance = Vector3.Distance(unit.transform.position, transform.position);
      if(distance > status.MaxAttackRange()) {
        continue;
      }
      if(distance < range) {
        range = distance;
        targetUnit = unit;
      }
    }
  }

  protected override void Dead() {
    EventManager.Trigger<PlayerUnit>("PlayerUnitDead", this);
    base.Dead();
  }
}
