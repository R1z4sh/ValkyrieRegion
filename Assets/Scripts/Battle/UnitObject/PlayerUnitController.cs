using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController :MonoBehaviour {
  [SerializeField] GameObject unitPrefab = null;
  private List<PlayerUnit> alliveUnits = new List<PlayerUnit>() { };
  private BattleController battleController = null;
  private Vector3 direction = new Vector3(0f, -1f, 0);
  private Vector3 leaderPos;

  public List<PlayerUnit> AlliveUnits() {
    return alliveUnits;
  }

  public void LeaderData(Vector3 position, Vector3 direction) {
    this.leaderPos = position;
    this.direction = direction;
  }

  public void Initialize(BattleController controller) {
    battleController = controller;
    EventManager.Subscribe<PlayerUnit>("PlayerUnitDead", onDeadPlayerUnit);
  }

  public void Summon(int id, int cost) {
    if(cost > battleController.GetSummonPoint())
      return;
    Vector3 summonPos = leaderPos + direction.normalized * 3;
    PlayerUnit summonUnit = GameObject.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerUnit>();
    summonUnit.transform.SetParent(this.transform);
    summonUnit.Initialize(battleController.GetPlayerController(), battleController.GetEnemyCountoller(), id, 10);
    summonUnit.transform.position = summonPos;
    alliveUnits.Add(summonUnit);
    EventManager.Trigger<int>("UseCost", cost);
  }

  private void onDeadPlayerUnit(PlayerUnit unit) {
    if(alliveUnits.Count <= 0 || !alliveUnits.Contains(unit)) return;
    PlayerUnit deadUnit = alliveUnits[alliveUnits.IndexOf(unit)];
    if(deadUnit == null)
      return;
    alliveUnits.Remove(unit);
  }

  private void OnDestroy() {
    EventManager.Unsubscribe<PlayerUnit>("PlayerUnitDead", onDeadPlayerUnit);
    foreach(var unit in alliveUnits) {
      if(unit != null) {
        Destroy(unit.gameObject);
      }
    }
    alliveUnits.Clear();
  }
}
