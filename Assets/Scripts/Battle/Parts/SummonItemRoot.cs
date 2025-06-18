using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class TroopUnitData
{
    public List<UnitData> units = new();
}

public class SummonItemRoot : MonoBehaviour
{
    [SerializeField] private GameObject summonItem;

    public void initialize(TroopUnitData troopData)
    {
        foreach (var unit in troopData.units)
        {
            GameObject obj = Instantiate(summonItem, transform);
            obj.GetComponent<SummonItem>().Initialize(unit);
            this.transform.SetParent(obj.transform, false);
        }
    }
}
