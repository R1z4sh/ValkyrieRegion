using System.Collections;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using System.Diagnostics;


public class AllayFlowAttack : FlowBase {
  private PlayerUnit owner = null;
  private bool attackTarget = false;
  public float cool = 0;
  private bool isCountDown = true;

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
    //owner.AttackRangeActive(true);

    Stopwatch sw = new Stopwatch();
    sw.Start();

    await Task.Delay((int)(owner.Status().AttackTime()));
    //owner.AttackRangeActive(false);
    //this.cool = owner.Status().AttackCool();

    TargetToUnit();
    TargetToTower();

    await Task.Delay((int)owner.Status().AttackCool());

    sw.Stop();
    UnityEngine.Debug.Log($"èàóùéûä‘: {sw.ElapsedMilliseconds} ms");

    if (!target) {
      Step((int)AllyUnitAct.CommonMove);
      return;
    }
    await Attack();
  }

  private void TargetToTower() {
    EnemyTower tower = target.GetComponent<EnemyTower>();
    if (!tower)
      return;
    tower.OnDamage(owner.Status().Offense());
  }

  private void TargetToUnit() {
    EnemyUnit enemy = target.GetComponent<EnemyUnit>();
    if (!enemy)
      return;
    enemy.OnDamage(owner.Status().Offense());
  }
}
