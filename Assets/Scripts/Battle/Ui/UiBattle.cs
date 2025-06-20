using UnityEngine;

public class UiBattle : MonoBehaviour
{
    [SerializeField]private FloatingJoystick joystick = null;
    [SerializeField] private SummonItemRoot summonItemRoot;
    [SerializeField] private SummonPoint summonPoint;

    public void Initialize(BattleController battleController)
    {
        TroopUnitData troopData = new();
        UnitMaster master = MasterManager.LoadMasterData<UnitMaster>("Master/M_Unit");
        for(int i=0; i < 5; ++i)
        {
            UnitData data = master.GetUnitData(i+1);
            if (data == null) continue;
            troopData.units.Add(data);
        }
        summonItemRoot.Initialize(battleController, troopData);
        summonPoint.Initialize();
    }

    public int GetSummonPoint()
    {
        return summonPoint.GetPoint();
    }   

    public FloatingJoystick GetJoyStick()
    {
        return joystick;
    }
}
