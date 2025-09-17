using UnityEngine;

public class EnemyFlowTowerMove :FlowBase {
  private EnemyUnit owner = null;

  private void UpdateEnemyUnitTarget() {
    target = null;
    float length = float.MaxValue;
    foreach(PlayerUnit unit in playerUnitcontoller.AlliveUnits()) {
      if(unit.IsDead()) continue;
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance)) {
        Step((int)EnemyUnitAct.Attack);
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
    target = Tower.Instance().SetTaret(0);
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(0), owner.transform.position);
    if(owner.IsAttackRange(distance)) {
      Step((int)EnemyUnitAct.Attack);
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
    if(this.flowStatus.Value != (int)EnemyUnitAct.TowerMove) return;
    if(owner == null) {
      owner = transform.parent.GetComponent<EnemyUnit>();
      return;
    }
    if(owner.IsDead()) {
      Step((int)EnemyUnitAct.Dead);
      return;
    }
    UpdateEnemyUnitTarget();
    UpdateTowerTarget();
    Move();
  }
}
