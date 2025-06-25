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
}

[Serializable]
public class EnemySpawnMaster {
  public List<SpawnData> data;

  public List<SpawnData> GetSpawnList(int stageId) {
    return data.FindAll((x) => x.stage_id == stageId);
  }
}
