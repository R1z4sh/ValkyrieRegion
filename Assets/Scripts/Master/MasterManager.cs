using System.Collections.Generic;
using UnityEngine;

public class MasterData { };

public class MasterManager {
  public static UnitMaster unitMaster = null;
  public static EnemySpawnMaster enemySpawnMaster = null;
  public static LeaderMaster leaderMaster = null;

  public static void LoadSeverData(MasterDataWrapper data) {
    unitMaster = new UnitMaster(data.M_Unit);
    enemySpawnMaster = new EnemySpawnMaster(data.M_EnemySpawn);
    leaderMaster = new LeaderMaster(data.M_Leader);
  }


  public static TMaster LoadLocalMasterData<TMaster>(string path) {
    TextAsset jsonText = Resources.Load<TextAsset>(path);
    if(jsonText == null) {
      Debug.LogError("JSONƒtƒ@ƒCƒ‹‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½");
      return default;
    }
    TMaster masterData = JsonUtility.FromJson<TMaster>(jsonText.text);
    return masterData;
  }
}
