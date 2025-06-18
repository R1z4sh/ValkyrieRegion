using System;
using System.Collections.Generic;

[Serializable]
public class UnitData
{
    public int id;
    public int cost;
    public string unitName;
    public int minHp;
    public int maxHp;
    public int minOffense;
    public int maxOffense;
    public float minAttackRange;
    public float maxAttackRange;
    public float attackCool;
    public int maxLv;
    public float move;
    public int action;
    public int attackType;
}


[Serializable]
public class UnitMaster
{
    public List<UnitData> data;

    public UnitData GetUnitData(int id)
    {
        return data.Find((x) => x.id == id);
    }
}
