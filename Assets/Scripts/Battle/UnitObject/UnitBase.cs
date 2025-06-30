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
  [SerializeField] protected HealthBar healthGauge = null;
  protected UnitStatus status = null;
  protected int unitActionStatus = 0;
  protected float attackCoolTime = 0f;
  protected PlayerUnitController playerUnitController = null;
  protected EnemyUnitController enemyUnitController = null;
  protected UnitActionFlowController actionFlowController = null;
  protected int fullHp = 0;
  public bool IsDead() {
    return status == null || status.Hp() <= 0;
  }

  public UnitStatus Status() { return status; }
  public bool IsAttackRange(float distance) { return status.MinAttackRange() < distance && status.MaxAttackRange() > distance; }

  public virtual void Initialize(
    PlayerUnitController playerUnitController,
    EnemyUnitController enemyUnitController,
    int unitId, int lv) {
    this.actionFlowController = new UnitActionFlowController(this, playerUnitController, enemyUnitController);
    this.playerUnitController = playerUnitController;
    this.enemyUnitController = enemyUnitController;
    UnitMaster unitMaster = MasterManager.unitMaster;
    UnitData data = unitMaster.GetUnitData(unitId);
    status = new UnitStatus(
        data.cost,
        data.unit_name,
        StatusCalcurion.calcCommonItem(data.min_hp, data.max_hp, data.max_lv, lv),
            StatusCalcurion.calcCommonItem(data.min_offense, data.max_offense, data.max_lv, lv),
            data.min_attack_range,
            data.max_attack_range,
            data.attack_cool,
            data.move
        );
    this.unitActionStatus = (int)UnitActionStatus.Move;
    this.fullHp = status.Hp();
    healthGauge.SetRate(1f);
  }

  public void OnDamage(int damage) {
    if(status.OnDamage(damage)) OnDead();
    healthGauge.SetRate((float)status.Hp() / this.fullHp);
  }

  protected virtual void OnDead() { }

  private void OnDestroy() {
    Destroy(transform.GetChild(0).gameObject);
  }
}
