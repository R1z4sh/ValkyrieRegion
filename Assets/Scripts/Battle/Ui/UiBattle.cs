using UnityEngine;

public class UiBattle : MonoBehaviour
{
    [SerializeField] private SummonItemRoot summonItemRoot;

    public void Initialize()
    {
        TroopUnitData troopData = new();
        UnitMaster master = MasterManager.LoadMasterData<UnitMaster>("Master/M_Unit");
        for(int i=0; i < 5; ++i)
        {
            UnitData data = master.GetUnitData(i+1);
            if (data == null) continue;
            troopData.units.Add(data);
        }
        summonItemRoot.initialize(troopData);
    }
}
