using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFlowAttack :FlowBase {
  private EnemyUnit owner = null;
  public float cool = 0;
  private bool attackTarget = false;
  private PlayerUnit target = null;
  private bool isCountDown = true;
  private IEnumerator Attack() {
    owner.AttackRangeActive(true);
    isCountDown = false;
    yield return new WaitForSeconds(owner.Status().AttackTime());
    owner.AttackRangeActive(false);
    isCountDown = true;
    this.cool = owner.Status().AttackCool();
    if(target) target.OnDamage(owner.Status().Offense());
    else Tower.Instance().OnDamage(0, owner.Status().Offense());
  }

  private void UpdateUnitTarget() {
    target = null;
    foreach(PlayerUnit unit in playerUnitcontoller.AlliveUnits()) {
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance) && !unit.IsDead()) {
        target = unit;
      }
    }
    attackTarget = target != null;
  }

  private void UpdateTowerTarget() {
    //NPCÇóDêÊ
    if(target != null) return;
    attackTarget = false;
    if(Tower.Instance() == null) return;
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(0), owner.transform.position);
    if(owner.IsAttackRange(distance)) {
      attackTarget = true;
    }
  }

  private void Update() {
    if(isStop) return;
    if(this.flowStatus.Value != (int)EnemyUnitAct.Attack) return;

    if(owner == null) {
      owner = transform.parent.GetComponent<EnemyUnit>();
      return;
    }
    if(owner.IsDead()) Step((int)EnemyUnitAct.Dead);

    UpdateUnitTarget();
    UpdateTowerTarget();

    if(!attackTarget) {
      Step((int)EnemyUnitAct.CommonMove);
      return;
    }
    if(isCountDown) cool = Mathf.Max(0, cool - Time.deltaTime);
    if(cool > 0 || !isCountDown) return;
    StartCoroutine(Attack());
  }
}

