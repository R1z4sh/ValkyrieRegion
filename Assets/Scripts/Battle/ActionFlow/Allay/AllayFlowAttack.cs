using System.Collections;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;


public class AllayFlowAttack : FlowBase {
  private PlayerUnit owner = null;

  public override void Initialize(
   ReactiveProperty<int> flowStatus ,
   PlayerUnitController playerUnitcontoller ,
   EnemyUnitController enemyUnitController ,
   GameObject target = null) {
    base.Initialize(flowStatus , playerUnitcontoller , enemyUnitController , target);
    owner = gameObject.transform.parent.parent.GetComponent<PlayerUnit>();
    Attack();
  }

  private async Task Attack() {
    await Task.Delay((int)(owner.Status().AttackTime() * 1000f));

    TargetToUnit();
    TargetToTower();

    await Task.Delay((int)(owner.Status().AttackCool() * 1000f));

    if (!target) {
      Step((int)AllyUnitAct.CommonMove);
      return;
    }
    Attack();
  }

  private void TargetToTower() {
    EnemyTower tower = target.GetComponent<EnemyTower>();
    if (!tower)
      return;
    tower.OnDamage(owner.Status().Offense());
  }

  private void TargetToUnit() {
    EnemyUnit enemy = target.GetComponent<EnemyUnit>();
    if (!enemy) {
      Debug.Log("Enemy NotFound");
      return;
    }
    bool isDead = enemy.OnDamage(owner.Status().Offense());
    if (isDead)
      target = null;
  }
}
