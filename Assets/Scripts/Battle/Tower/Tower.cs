using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tower : MonoBehaviour {
  [SerializeField] AllayTower allayTower = null;
  [SerializeField] EnemyTower enemyTower = null;
  private static Tower instance = null;

  protected void Awake() {
    instance = this;
    if (allayTower == null || enemyTower == null) {
      Debug.LogError("AllayTower or EnemyTower is not assigned in the inspector.");
      return;
    }
  }


  public GameObject SetTarget(int team) {
    if (team == 0)
      return allayTower.gameObject;
    if (team == 1)
      return enemyTower.gameObject;
    return null;
  }

  public Vector3 TowerPosition(int team) {
    return team == 0 ? allayTower.gameObject.transform.position : enemyTower.gameObject.transform.position;
  }
  public void InitializeToewrPosition(Vector3 allay , Vector3 enemy) {
    allayTower.gameObject.transform.position = allay;
    enemyTower.gameObject.transform.position = enemy;
  }

  public static Tower Instance() {
    return instance;
  }

  public void OnDamage(int team , int damage) {
    int Allay = 0;
    int Enemy = 1;
    if (team == Allay && allayTower.OnDamage(damage))
      EventManager.Trigger<bool>("gameOver" , true);
    if (team == Enemy && enemyTower.OnDamage(damage))
      EventManager.Trigger<bool>("gameClear" , true);

  }
}

