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
    owner = gameObject.transform.parent.parent.GetComponent<EnemyUnit>();
    Attack();
  }

  private async Task Attack() {
    await Task.Delay((int)(owner.Status().AttackTime() * 1000f));

    TargetToUnit();
    TargetToTower();

    await Task.Delay((int)(owner.Status().AttackCool() * 1000f));

    if (!target) {
      Step((int)EnemyUnitAct.CommonMove);
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
    bool isDead = playerUnit.OnDamage(owner.Status().Offense());
    if (isDead)
      target = null;

  }
}

