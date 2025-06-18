using UnityEngine;


public class UnitStatus
{
   public  UnitStatus(int cost,string unitName,int hp,int offense,float minAttackRange,float maxAttackRange,float attackCool,float move) {
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

    public bool  OnDamage(int damage) {
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

public class UnitBase : MonoBehaviour
{
    protected UnitStatus status = null;
    
    public virtual void Initialize(int unitId,int lv) {
        UnitMaster unitMaster = MasterManager.LoadMasterData<UnitMaster>("Master/M_Unit");
        UnitData data = unitMaster.GetUnitData(unitId);
        status = new UnitStatus(
            data.cost,
            data.unitName,
            StatusCalcurion.calcCommonItem(data.minHp, data.maxHp, data.maxLv, 10),
                StatusCalcurion.calcCommonItem(data.minOffense, data.maxOffense, data.maxLv, 10),
                data.minAttackRange,
                data.maxAttackRange,
                data.attackCool,
                data.move
            );
    }

    public void onDamage(int damage)
    {
        if (status.OnDamage(damage)) onDead();
    }

    public void onDead() { }
}
