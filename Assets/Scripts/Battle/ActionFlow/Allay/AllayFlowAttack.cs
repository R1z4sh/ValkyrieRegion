using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class AllayFlowAttack :FlowBase {
  private PlayerUnit owner = null;
  public float cool = 0;
  private bool isCountDown = true;

  private IEnumerator Attack() {
    isCountDown = false;
    owner.AttackRangeActive(true);
    yield return new WaitForSeconds(owner.Status().AttackTime());
    owner.AttackRangeActive(false);
    isCountDown = true;
    this.cool = owner.Status().AttackCool();

    EnemyUnit targetUnit = target.GetComponent<EnemyUnit>();
    Tower targetTower = target.GetComponent<Tower>();
  }


  private void Update() {
    if(isStop) return;
    if(!target) {
      Step((int)AllyUnitAct.CommonMove);
      return;
    }
    if(this.flowStatus.Value != (int)AllyUnitAct.Attack) return;
    if(owner == null) {
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    if(owner.IsDead()) Step((int)AllyUnitAct.Dead);

    if(isCountDown) cool = Mathf.Max(0, cool - Time.deltaTime);
    if(cool > 0 || !isCountDown) return;
    Debug.Log("AllayOnAttack");
    StartCoroutine(Attack());
  }
}
