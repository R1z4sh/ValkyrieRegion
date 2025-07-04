using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MasterDataWrapper {
  public List<LeaderData> M_Leader;
  public List<SpawnData> M_EnemySpawn;
  public List<UnitData> M_Unit;
  public List<StageData> m_stages;
}
[Serializable]
public class MasterDataList {
  public MasterDataWrapper M_Data;
}
