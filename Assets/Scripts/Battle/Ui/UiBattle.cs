using UnityEngine;

public class UiBattle :MonoBehaviour {
  [SerializeField] private FloatingJoystick joystick = null;
  [SerializeField] private SummonItemRoot summonItemRoot;
  [SerializeField] private SummonPoint summonPoint;
  [SerializeField] private GameTimer timer;

  public void Initialize(BattleController battleController) {
    TroopUnitData troopData = new();
    timer.Initialize(100001);
    UnitMaster master = MasterManager.unitMaster;
    for(int i = 0; i < 5; ++i) {
      UnitData data = master.GetUnitData(i + 1);
      if(data == null)
        continue;
      troopData.units.Add(data);
    }
    summonItemRoot.Initialize(battleController, troopData);
    summonPoint.Initialize();
  }

  public int GetSummonPoint() {
    return summonPoint.GetPoint();
  }

  public FloatingJoystick GetJoyStick() {
    return joystick;
  }
}
