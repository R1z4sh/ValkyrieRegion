using System.Threading.Tasks;
using UnityEngine;

public class MasterData { };

public class MasterManager {
  public static UnitMaster unitMaster = null;
  public static EnemySpawnMaster enemySpawnMaster = null;
  public static LeaderMaster leaderMaster = null;
  public static StageMaster stageMaster = null;
  public static async Task LoadSeverData() {
    WWWForm form = new WWWForm();
    form.AddField("user", "devuser");
    form.AddField("pass", "your_password");
    MasterDataWrapper data = await ApiClient.Instance().Fetch<MasterDataWrapper>("http://163.43.218.37/api/dataload", form);
    unitMaster = new UnitMaster(data.M_Unit);
    enemySpawnMaster = new EnemySpawnMaster(data.M_EnemySpawn);
    leaderMaster = new LeaderMaster(data.M_Leader);
    stageMaster = new StageMaster(data.m_stages);
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
