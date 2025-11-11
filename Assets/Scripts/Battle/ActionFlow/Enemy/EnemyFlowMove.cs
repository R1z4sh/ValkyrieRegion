using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFlowMove : FlowBase {
  private EnemyUnit owner = null;

  private void UpdatePlayerUnitTarget() {
    float length = float.MaxValue;
    foreach (PlayerUnit unit in playerUnitcontoller.AlliveUnits()) {
      if (unit.IsDead())
        continue;
      float distance = Vector3.Distance(unit.transform.position , owner.transform.position);
      if (owner.IsAttackRange(distance)) {
        Step((int)EnemyUnitAct.Attack);
        return;
      }
      //ŽË’öŠO‚È‚çˆÚ“®
      if (distance < length && owner.IsSearchRange(distance)) {
        length = distance;
        target = unit.gameObject;
      }
    }
  }


  private bool IsTowerTarget() {
    float range = Mathf.Abs(this.owner.transform.position.x - Tower.Instance().TowerPosition(0).x);
    return range < 5;
  }

  private void UpdateTowerTarget() {
    if (target != null)
      return;
    if (Tower.Instance() == null)
      return;
    if (!IsTowerTarget())
      return;

    float distance = Vector3.Distance(Tower.Instance().TowerPosition(0) , owner.transform.position);
    if (owner.IsAttackRange(distance)) {
      target = Tower.Instance().SetTarget(0);
      Step((int)EnemyUnitAct.Attack);
      return;
    }
  }

  private void Move() {
    Vector3 direction = Vector3.zero;
    if (target != null)
      direction = (target.transform.position - owner.transform.position).normalized;
    else
      direction = new Vector3(-1 , 0 , 0);
    owner.transform.position += direction * owner.Status().Move() * Time.deltaTime;
  }

  private void Update() {
    if (isStop)
      return;

    if (this.flowStatus.Value != (int)EnemyUnitAct.CommonMove)
      return;
    if (owner == null) {
      owner = transform.parent.GetComponent<EnemyUnit>();
      return;
    }

    UpdatePlayerUnitTarget();
    UpdateTowerTarget();
    Move();
  }
}
