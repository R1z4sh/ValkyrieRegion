using System.Collections;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFlowAttack : FlowBase {
  private EnemyUnit owner = null;


  public override void Initialize(
 ReactiveProperty<int> flowStatus ,
 PlayerUnitController playerUnitcontoller ,
 EnemyUnitController enemyUnitController ,
 GameObject target = null) {
    base.Initialize(flowStatus , playerUnitcontoller , enemyUnitController , target);
    Attack();
  }

  private async Task Attack() {
    //owner.AttackRangeActive(true);
    //isCountDown = true;
    await Task.Delay((int)(owner.Status().AttackTime()));
    //isCountDown = false;
    //owner.AttackRangeActive(false);
    //this.cool = owner.Status().AttackCool();

    TargetToUnit();
    TargetToTower();

    await Task.Delay((int)owner.Status().AttackCool());

    if (!target) {
      Step((int)AllyUnitAct.CommonMove);
      return;
    }

    Attack();
  }

  private void TargetToTower() {
    AllyTower tower = target.GetComponent<AllyTower>();
    if (!tower)
      return;
    tower.OnDamage(owner.Status().Offense());
  }

  private void TargetToUnit() {
    PlayerUnit playerUnit = target.GetComponent<PlayerUnit>();
    if (!playerUnit)
      return;
    playerUnit.OnDamage(owner.Status().Offense());
  }
}

