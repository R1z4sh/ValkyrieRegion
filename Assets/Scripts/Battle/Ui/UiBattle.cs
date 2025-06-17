using UnityEngine;

public class UiBattle : MonoBehaviour
{
    [SerializeField] private SummonItemRoot summonItemRoot;

    public void Initialize()
    {
        TroopUnitData troopData = new TroopUnitData();
        troopData.units = new Unit[5];
        summonItemRoot.initialize(troopData);
    }
}
