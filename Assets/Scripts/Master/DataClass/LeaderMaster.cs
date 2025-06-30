using System;
using System.Collections.Generic;
[Serializable]
public class LeaderData {
  public int m_leader_id;
  public string name;
  public int min_hp;
  public int max_hp;
  public int min_offense;
  public int max_offense;
  public float min_attack_range;
  public float max_attack_range;
  public float attack_cool;
  public int max_lv;
  public int attack_type;
}

[Serializable]
public class LeaderMaster :MasterData {
  public List<LeaderData> data;

  public LeaderMaster(List<LeaderData> data) { this.data = data; }

  public List<LeaderData> GetSpawnList(int leaderId) {
    return data.FindAll((x) => x.m_leader_id == leaderId);
  }
}
