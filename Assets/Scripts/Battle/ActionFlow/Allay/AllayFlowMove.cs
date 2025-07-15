using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class AllayFlowMove :FlowBase {
  private PlayerUnit owner = null;

  private void UpdateEnemyUnitTarget() {
    target = null;
    float length = float.MaxValue;
    foreach(EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      if(unit.IsDead()) continue;
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance)) {
        Step((int)AllyUnitAct.Attack);
        return;
      }
      //ŽË’öŠO‚È‚çˆÚ“®
      if(distance < length && owner.IsSearchRange(distance)) {
        length = distance;
        target = unit.gameObject;
      }
    }
  }
  private void UpdateTowerTarget() {
    if(target != null) return;
    if(Tower.Instance() == null) return;
    target = Tower.Instance().SetTaret(1);
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(1), owner.transform.position);
    if(owner.IsAttackRange(distance)) {
      Step((int)AllyUnitAct.Attack);
      return;
    }
  }

  private void Move() {
    Vector3 direction = Vector3.zero;
    if(target != null) direction = (target.transform.position - owner.transform.position).normalized;
    else direction = new Vector3(1, 0, 0);
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
    UpdateTowerTarget();
    Move();
  }
}
