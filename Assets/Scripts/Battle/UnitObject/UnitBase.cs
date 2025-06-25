using UnityEngine;
using UnityEngine.UI;

public class UnitStatus {
  public UnitStatus(int cost, string unitName, int hp, int offense, float minAttackRange, float maxAttackRange, float attackCool, float move) {
    this.cost = cost;
    this.unitName = unitName;
    this.hp = hp;
    this.offense = offense;
    this.minAttackRange = minAttackRange;
    this.maxAttackRange = maxAttackRange;
    this.attackCool = attackCool;
    this.move = move;
  }
  public int Cost() { return cost; }
  public string UnitName() { return unitName; }
  public int Hp() { return hp; }
  public int Offense() { return offense; }
  public float MinAttackRange() { return minAttackRange; }
  public float MaxAttackRange() { return maxAttackRange; }
  public float AttackCool() { return attackCool; }
  public float Move() { return move; }

  public bool OnDamage(int damage) {
    hp -= damage;
    return hp <= 0;
  }
  private int cost;
  private string unitName;
  private int hp;
  private int offense;
  private float minAttackRange;
  private float maxAttackRange;
  private float attackCool;
  private float move;
}

enum UnitActionStatus {
  None = 0,
  Move = 1,
  Attack = 2,
  Dead = 4
}

public class UnitBase :MonoBehaviour {
  [SerializeField] protected SpriteRenderer unitImage;
  protected UnitStatus status = null;
  protected int unitActionStatus = 0;

  protected PlayerUnitController playerUnitController = null;
  protected EnemyUnitController enemyUnitController = null;

  public virtual void Initialize(
    PlayerUnitController playerUnitController,
    EnemyUnitController enemyUnitController,
    int unitId, int lv) {
    Debug.Log("UnitInitialize");
    this.playerUnitController = playerUnitController;
    this.enemyUnitController = enemyUnitController;
    UnitMaster unitMaster = MasterManager.LoadMasterData<UnitMaster>("Master/M_Unit");
    UnitData data = unitMaster.GetUnitData(unitId);
    status = new UnitStatus(
        data.cost,
        data.unit_name,
        StatusCalcurion.calcCommonItem(data.min_hp, data.max_hp, data.max_lv, 10),
            StatusCalcurion.calcCommonItem(data.min_offense, data.max_offense, data.max_lv, 10),
            data.min_attack_range,
            data.max_attack_range,
            data.attack_cool,
            data.move
        );
    this.unitActionStatus = (int)UnitActionStatus.Move;
  }

  protected virtual void Move() {

  }

  protected virtual void Attack() {

  }

  protected virtual void Dead() {

  }

  public void UnitActionControll() {
    switch((UnitActionStatus)unitActionStatus) {
      case UnitActionStatus.Move:
        Move();
        break;
      case UnitActionStatus.Attack:
        Attack();
        break;
      case UnitActionStatus.Dead:
        Dead();
        break;

      case UnitActionStatus.None:
        break;
      default:
        Debug.LogError("Unknown UnitActionStatus: " + unitActionStatus);
        break;
    }
  }


  private void Update() {
    UnitActionControll();
  }

  public void OnDamage(int damage) {
    if(status.OnDamage(damage))
      OnDead();
  }

  public void OnDead() {
    //Ž€‚ñ‚¾‚±‚Æ‚ð’Ê’m
    EventManager.Trigger<UnitBase>("onDeadUnit", this);
    unitActionStatus = (int)UnitActionStatus.Dead;
  }
}
