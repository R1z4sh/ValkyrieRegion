using System.Collections.Generic;
using UnityEngine;


public class TroopUnitData
{
    public List<UnitData> units = new();
}

public class SummonItemRoot : MonoBehaviour
{
    [SerializeField] private GameObject summonItem;
    public void Initialize(BattleController controller,TroopUnitData troopData)
    {
        foreach (var unit in troopData.units)
        {
            GameObject obj = Instantiate(summonItem, transform);
            obj.GetComponent<SummonItem>().Initialize(controller,unit);
            this.transform.SetParent(obj.transform, false);
        }
    }
}
