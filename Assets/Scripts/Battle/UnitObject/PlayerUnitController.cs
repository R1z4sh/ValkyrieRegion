using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : MonoBehaviour {
  [SerializeField] GameObject unitPrefab = null;
  private Dictionary<int , PlayerUnit> alliveUnits = new Dictionary<int , PlayerUnit>() { };
  private BattleController battleController = null;
  private Vector3 direction = new Vector3(0f , -1f , 0);
  private Vector3 leaderPos;

  public void LeaderData(Vector3 position,Vector3 direction) {
    this.leaderPos = position;
    this.direction = direction;
  }

  public void Initialize(BattleController controller) {
    battleController = controller;
    EventManager.Subscribe<int>("onDeadUnit" , onDeadPlayerUnit);
  }

  public void Summon(int id , int cost) {
    if (cost > battleController.GetSummonPoint())
      return;
        Vector3 summonPos = leaderPos + direction.normalized * 3;
        PlayerUnit summonUnit = GameObject.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerUnit>();
        summonUnit.transform.SetParent(this.transform);
        summonUnit.Initialize(id, 10, alliveUnits.Count);
        summonUnit.transform.position = summonPos;
        alliveUnits.Add(alliveUnits.Count + 1, summonUnit);

        EventManager.Trigger<int>("UseCost" , cost);
  }

  private void onDeadPlayerUnit(int index) {
    PlayerUnit deadUnit = alliveUnits[index];
    if (deadUnit == null)
      return;
    alliveUnits.Remove(index);
    Destroy(deadUnit);
  }
}
