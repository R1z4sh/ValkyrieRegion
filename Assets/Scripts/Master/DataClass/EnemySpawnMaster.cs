using System;
using System.Collections.Generic;
[Serializable]
public class SpawnData {
  public int stageId;
  public int unitId;
  public int time;
  public int count;
  public int lv;
}

[Serializable]
public class EnemySpawnMaster {
  public List<SpawnData> data;

  public List<SpawnData> GetSpawnList(int stageId) {
    return data.FindAll((x) => x.stageId == stageId);
  }
}
