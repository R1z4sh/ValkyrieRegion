using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class AllayFlowAttack :FlowBase {
  private PlayerUnit owner = null;
  public float cool = 0;
  private EnemyUnit target = null;

  private IEnumerator Attack() {
    yield return new WaitForSeconds(owner.Status().AttackTime());
    this.cool = owner.Status().AttackCool();
    if(target) {
      target.OnDamage(owner.Status().Offense());
      yield return null;
    }
  }

  private void UpdateUnitTarget() {
    target = null;
    foreach(EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      if(unit.IsDead()) continue;
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance) && !unit.IsDead()) {
        target = unit;
      }
    }
  }

  private void Update() {
    if(isStop) return;
    if(this.flowStatus.Value != (int)AllyUnitAct.Attack) return;
    if(owner == null) {
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    if(owner.IsDead()) Step((int)AllyUnitAct.Dead);
    UpdateUnitTarget();
    if(!target) {
      Step((int)AllyUnitAct.CommonMove);
      return;
    }
    cool = Mathf.Max(0, cool - Time.deltaTime);
    if(cool > 0) return;
    StartCoroutine(Attack());
  }
}
