using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab = null;
    private Dictionary<int, EnemyUnit> alliveUnits = new Dictionary<int, EnemyUnit>() { };
    public void Initialize()
    {
        EventManager.Subscribe<SpawnData>("enemyPop", SpawnEnemy);
    }

    private void SpawnEnemy(SpawnData data)
    {
        if (data == null) return;
        Vector3 spawnPos = Vector3.zero;
        EnemyUnit spawnUnit = Object.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity).GetComponent<EnemyUnit>();
        spawnUnit.transform.SetParent(this.transform);
        spawnUnit.Initialize(data.unitId, data.lv, alliveUnits.Count);
        spawnUnit.GetComponent<RectTransform>().anchoredPosition = spawnPos;
        alliveUnits.Add(alliveUnits.Count + 1, spawnUnit);
    }
}
