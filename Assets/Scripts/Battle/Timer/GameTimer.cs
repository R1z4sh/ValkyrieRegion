using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {
  [SerializeField] private Text timerCount;
  private float timer = 0f;

  private EnemySpawnMaster enemySpawn;
  private List<SpawnData> spawnList = new();

  private void Initialize(int stageId) {
    enemySpawn = MasterManager.LoadMasterData<EnemySpawnMaster>("Master/M_EnemySpawn");
    spawnList = enemySpawn.GetSpawnList(stageId);
  }

  private void Update() {
    timer += Time.deltaTime;
    SetTimrCount();
    EnemySpawnTrigger();
  }

  private void EnemySpawnTrigger() {
    int time = (int)timer;
    foreach (SpawnData data in spawnList) {
      if (data.time == time)
        EventManager.Trigger("enemyPop" , data);
    }
  }

  private void SetTimrCount() {
    int seconds = Mathf.FloorToInt(timer % 60);
    int minutes = Mathf.FloorToInt((timer / 60) % 60);
    timerCount.text = string.Format("{0:D2}:{1:D2}" , minutes , seconds);
  }
}
