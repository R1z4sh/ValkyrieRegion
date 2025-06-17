using System.Reflection;
using UnityEngine;

public class Unit { }

public class TroopUnitData
{
    public Unit[] units = { };
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
            this.transform.parent = obj.transform;
        }
    }
}
