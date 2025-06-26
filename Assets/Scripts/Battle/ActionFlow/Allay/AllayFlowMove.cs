using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class AllayFlowMove :FlowBase {
  private PlayerUnit owner = null;
  private EnemyUnit target = null;


  private void UpdateEnemyUnitTarget() {
    target = null;
    foreach(EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      if(unit.IsDead()) continue;
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance)) {
        Step((int)AllyUnitAct.Attack);
        return;
      }
      //ŽË’öŠO‚È‚çˆÚ“®
      target = unit;
    }
  }


  private void Move() {
    if(!target) return;
    Vector3 direction = (target.transform.position - owner.transform.position).normalized;
    owner.transform.position += direction * owner.Status().Move() * Time.deltaTime;
  }


  private void Update() {
    if(isStop) return;
    if(this.flowStatus.Value != (int)AllyUnitAct.CommonMove) return;
    if(owner == null) {
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    if(owner.IsDead()) {
      Step((int)AllyUnitAct.Dead);
      return;
    }
    UpdateEnemyUnitTarget();
    Move();
  }
}
