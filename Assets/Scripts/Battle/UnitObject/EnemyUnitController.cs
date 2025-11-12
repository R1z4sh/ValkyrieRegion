using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class EnemyUnitController : MonoBehaviour {
  [SerializeField] Camera mainCamera = null;
  [SerializeField] GameObject unitPrefab = null;

  private BattleController battleController = null;
  public float spawnRadius = 30f;
  public int maxAttempts = 20;

  private readonly List<EnemyUnit> alliveUnits = new() { };
  public void Initialize(BattleController controller) {
    this.battleController = controller;
    EventManager.Subscribe<SpawnData>("enemyPop" , SpawnEnemy);
    EventManager.Subscribe<EnemyUnit>("enemyUnitDead" , onDeadEnemyUnit);
  }

  public List<EnemyUnit> AlliveUnits() {
    return alliveUnits;
  }
  private void SpawnEnemy(SpawnData data) {
    if (data == null)
      return;
    Vector3 spawnPos;
    if (data.position_x != 0 || data.position_y != 0)
      spawnPos = new Vector3(data.position_x , data.position_y);
    else if (!TryGetValidSpawnPosition(out spawnPos))
      return;
    Debug.Log("PopupExecute");
    EnemyUnit spawnUnit = GameObject.Instantiate(unitPrefab , spawnPos , Quaternion.identity).GetComponent<EnemyUnit>();
    spawnUnit.transform.SetParent(this.transform);
    spawnUnit.Initialize(battleController.GetPlayerController() , this , data.m_unit_id , data.lv);
    spawnUnit.transform.position = spawnPos;
    alliveUnits.Add(spawnUnit);
  }

  private bool TryGetValidSpawnPosition(out Vector3 result) {
    for (int i = 0; i < maxAttempts; i++) {
      Vector3 randomPos = GetRandomWorldPosition();

      // ƒJƒƒ‰‰æ–ÊŠO‚©H
      Vector3 viewportPos = mainCamera.WorldToViewportPoint(randomPos);
      bool isOffScreen = viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;

      if (isOffScreen) {
        result = randomPos;
        return true;
      }
    }

    result = Vector3.zero;
    return false;
  }

  Vector3 GetRandomWorldPosition() {
    Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
    return (Vector3)randomCircle;
  }

  private void onDeadEnemyUnit(EnemyUnit unit) {
    if (alliveUnits.Count <= 0 || !alliveUnits.Contains(unit))
      return;
    EnemyUnit deadUnit = alliveUnits[alliveUnits.IndexOf(unit)];
    if (deadUnit == null)
      return;
    alliveUnits.Remove(unit);
  }
  private void OnDestroy() {
    EventManager.Unsubscribe<SpawnData>("enemyPop" , SpawnEnemy);
    EventManager.Unsubscribe<EnemyUnit>("enemyUnitDead" , onDeadEnemyUnit);
    foreach (var unit in alliveUnits) {
      if (unit != null) {
        Destroy(unit.gameObject);
      }
    }
    alliveUnits.Clear();
  }
}
