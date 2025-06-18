using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class PlayerUnitController
{
    static PlayerUnitController instance = null;
    public bool summonState = false;
    private int summonUnitId = 0;
    private List<PlayerUnit> alliveUnits = new();
    public int SummonUnitId() { return summonUnitId; }
    
    public static PlayerUnitController Instance(){
        if (instance == null)instance =  new PlayerUnitController();
        return instance;
    }

    public void Summon(GameObject summonUnit) {
        if (!summonState) return;
        PlayerUnit comp = summonUnit.GetComponent<PlayerUnit>();
        comp.Initialize(summonUnitId, 10);
        summonState = false;
        alliveUnits.Add(comp);
    }

    public bool SetUpSummon(int id){
        if (summonState)
        {
            summonState = false;
            summonUnitId = 0;
            return false;
        }
        summonState = true;
        summonUnitId = id;
        return summonState;
    }


}
