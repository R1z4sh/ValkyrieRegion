using UnityEngine;

public class EnemyFlowMove :FlowBase {
  private EnemyUnit owner = null;

  private void UpdatePlayerUnitTarget() {
    foreach(PlayerUnit unit in playerUnitcontoller.AlliveUnits()) {
      if(unit.IsDead()) continue;
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance)) {
        Step((int)EnemyUnitAct.Attack);
        return;
      }
    }
  }
  private void UpdateTowerTarget() {
    if(Tower.Instance() == null) return;
    float distance = Vector3.Distance(Tower.Instance().transform.position, owner.transform.position);
    if(owner.IsAttackRange(distance)) {
      Step((int)EnemyUnitAct.Attack);
      return;
    }
  }

  private void Move() {
    if(Tower.Instance() == null) return;
    Vector3 direction = (Tower.Instance().transform.position - owner.transform.position).normalized;
    owner.transform.position += direction * owner.Status().Move() * Time.deltaTime;
  }

  private void Update() {
    if(isStop) return;
    if(isStop) return;
    if(this.flowStatus.Value != (int)EnemyUnitAct.CommonMove) return;
    if(owner == null) {
      owner = transform.parent.GetComponent<EnemyUnit>();
      return;
    }
    if(owner.IsDead()) {
      Step((int)EnemyUnitAct.Dead);
      return;
    }
    UpdateTowerTarget();
    UpdatePlayerUnitTarget();
    Move();
  }
}
