using System.Collections;
using UniRx;
using UnityEngine;

public class AllayFlowAttack : FlowBase {
  private PlayerUnit owner = null;
  private bool attackTarget = false;
  public float cool = 0;
  private bool isCountDown = true;


  private IEnumerator Attack() {
    //owner.AttackRangeActive(true);
    isCountDown = true;
    yield return new WaitForSeconds(owner.Status().AttackTime());
    isCountDown = false;
    //owner.AttackRangeActive(false);
    this.cool = owner.Status().AttackCool();
    if (target)
      target.GetComponent<EnemyUnit>().OnDamage(owner.Status().Offense());
    else {
      Debug.Log("Enemy Tower OnDamage");
      Tower.Instance().OnDamage(1 , owner.Status().Offense());
    }
  }

  private void UpdateUnitTarget() {
    target = null;
    foreach (EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      float distance = Vector3.Distance(unit.transform.position , owner.transform.position);
      if (owner.IsAttackRange(distance) && !unit.IsDead()) {
        target = unit.gameObject;
      }
    }
    attackTarget = target != null;
  }

  private void UpdateTowerTarget() {
    //NPC‚ð—Dæ
    if (target != null)
      return;
    attackTarget = false;
    if (Tower.Instance() == null)
      return;
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(1) , owner.transform.position);
    Debug.Log("Distance:" + distance + ",attackRange:[" + owner.Status().MinAttackRange() + "," + owner.Status().MaxAttackRange() + "]");
    if (owner.IsAttackRange(distance)) {
      attackTarget = true;
    }
  }

  private void Update() {
    if (isStop)
      return;

    if (this.flowStatus.Value != (int)AllyUnitAct.Attack)
      return;

    cool = Mathf.Max(0 , cool - Time.deltaTime);
    if (cool > 0 || isCountDown)
      return;

    if (owner == null) {
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    if (owner.IsDead())
      Step((int)AllyUnitAct.Dead);

    UpdateUnitTarget();
    UpdateTowerTarget();


    if (!attackTarget) {
      Step((int)AllyUnitAct.CommonMove);
      Debug.Log("Ally Step Move");
      return;
    }


    StartCoroutine(Attack());
  }
}
