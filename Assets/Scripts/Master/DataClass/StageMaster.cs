using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[Serializable]
public class StageData
{
  public int id;
  public int stage_id;
  public int starge_sort_no;
  public string stage_name;
  public float scale_x;
  public float scale_y;
  public float player_tower_pos_x;
  public float player_tower_pos_y;
  public float player_pos_x;
  public float player_pos_y;
  public float enemy_tower_pos_x;
  public float enemy_tower_pos_y;
  public float enemy_tower_hp;
  public int stage_model;
  public int enemy_boss_popup_time;
  public int enemy_boss_popup_hp;

  public float ScaleX() { return scale_x / LocalDefines.SCALING; }
  public float ScaleY() { return scale_y / LocalDefines.SCALING; }
  public float PlayerPosX() { return player_pos_x / LocalDefines.SCALING; }
  public float PlayerPosY() { return player_pos_y / LocalDefines.SCALING; }
  public float PlayerTowerPosX() { return player_tower_pos_x / LocalDefines.SCALING; }
  public float PlayerTowerPosY() { return player_tower_pos_y / LocalDefines.SCALING; }
  public float EnemyTowerPosX() { return enemy_tower_pos_x / LocalDefines.SCALING; }
  public float EnemyTowerPosY() { return enemy_tower_pos_y / LocalDefines.SCALING; }
  
}


[Serializable]
public class StageMaster :MonoBehaviour {
  private List<StageData> data;

  public StageMaster(List<StageData> data) {
    this.data = data;
  }

  public StageData GetStageData(int stageId) {
    return data.Find((x) => x.stage_id == stageId);
  }

}
