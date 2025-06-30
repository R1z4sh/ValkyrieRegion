using System;
using System.Collections.Generic;
[Serializable]
public class SpawnData {
  public int stage_id;
  public int m_unit_id;
  public int time;
  public int count;
  public int lv;
  public float position_x;
  public float position_y;
  public int end;
}

[Serializable]
public class EnemySpawnMaster :MasterData {
  public List<SpawnData> data;

  public EnemySpawnMaster(List<SpawnData> data) { this.data = data; }

  public List<SpawnData> GetSpawnList(int stageId) {
    return data.FindAll((x) => x.stage_id == stageId);
  }
}
