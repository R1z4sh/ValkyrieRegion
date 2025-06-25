using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class EnemyUnitController :MonoBehaviour {
  [SerializeField] Camera mainCamera = null;
  [SerializeField] GameObject unitPrefab = null;


  public float spawnRadius = 30f;
  public int maxAttempts = 20;

  private Dictionary<int, EnemyUnit> alliveUnits = new Dictionary<int, EnemyUnit>() { };
  public void Initialize() {
    EventManager.Subscribe<SpawnData>("enemyPop", SpawnEnemy);
  }

  private void SpawnEnemy(SpawnData data) {
    if(data == null) return;
    Vector3 spawnPos;
    if(data.position_x != 0 || data.position_y != 0) spawnPos = new Vector3(data.position_x, data.position_y);
    else if(!TryGetValidSpawnPosition(out spawnPos)) return;
    EnemyUnit spawnUnit = Object.Instantiate(unitPrefab, spawnPos, Quaternion.identity).GetComponent<EnemyUnit>();
    spawnUnit.transform.SetParent(this.transform);
    spawnUnit.Initialize(data.m_unit_id, data.lv, alliveUnits.Count);
    spawnUnit.transform.position = spawnPos;
    alliveUnits.Add(alliveUnits.Count + 1, spawnUnit);
  }

  private bool TryGetValidSpawnPosition(out Vector3 result) {
    for(int i = 0; i < maxAttempts; i++) {
      Vector3 randomPos = GetRandomWorldPosition();

      // カメラ画面外か？
      Vector3 viewportPos = mainCamera.WorldToViewportPoint(randomPos);
      bool isOffScreen = viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;

      //// SpriteRenderer の描画範囲外か？
      //bool isOutsideSprite = !bg.bounds.Contains(randomPos);

      if(isOffScreen) {
        result = randomPos;
        return true;
      }
    }

    result = Vector3.zero;
    return false;
  }

  Vector3 GetRandomWorldPosition() {
    Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
    return new Vector3(randomCircle.x, randomCircle.y, 0f);
  }
}
