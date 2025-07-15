using System;
using System.Collections.Generic;

[Serializable]
public class UnitData {
  public int m_unit_id;
  public int cost;
  public string unit_name;
  public int min_hp;
  public int max_hp;
  public int min_offense;
  public int max_offense;
  public float min_attack_range;
  public float max_attack_range;
  public float attack_cool;
  public int max_lv;
  public float move;
  public int action;
  public int attack_type;
  public float attack_time;
  public float search_range;
}


[Serializable]
public class UnitMaster :MasterData {
  public List<UnitData> data;

  public UnitMaster(List<UnitData> data) { this.data = data; }

  public UnitData GetUnitData(int id) {
    return data.Find((x) => x.m_unit_id == id);
  }
}
