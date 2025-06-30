using System.Collections;
using UnityEngine;

public class EnemyFlowAttack :FlowBase {
  private EnemyUnit owner = null;
  public float cool = 0;
  private bool attackTarget = false;
  private PlayerUnit target = null;


  private IEnumerator Attack() {
    yield return new WaitForSeconds(owner.Status().AttackTime());
    this.cool = owner.Status().AttackCool();
    if(target) {
      target.OnDamage(owner.Status().Offense());
      yield return null;
    }
    Tower.Instance().OnDamage(owner.Status().Offense());
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
    float distance = Vector3.Distance(Tower.Instance().transform.position, owner.transform.position);
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
    cool = Mathf.Max(0, cool - Time.deltaTime);
    if(cool > 0) return;
    StartCoroutine(Attack());
  }
}

