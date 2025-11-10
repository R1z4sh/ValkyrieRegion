using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tower : MonoBehaviour {
  [SerializeField] AllyTower allyTower = null;
  [SerializeField] EnemyTower enemyTower = null;
  private static Tower instance = null;

  protected void Awake() {
    instance = this;
    if (allyTower == null || enemyTower == null) {
      Debug.LogError("AllayTower or EnemyTower is not assigned in the inspector.");
      return;
    }
  }


  public GameObject SetTarget(int team) {
    if (team == 0)
      return allyTower.gameObject;
    if (team == 1)
      return enemyTower.gameObject;
    return null;
  }

  public Vector3 TowerPosition(int team) {
    return team == 0 ? allyTower.gameObject.transform.position : enemyTower.gameObject.transform.position;
  }
  public void InitializeToewrPosition(Vector3 allay , Vector3 enemy) {
    allyTower.gameObject.transform.position = allay;
    enemyTower.gameObject.transform.position = enemy;
  }

  public static Tower Instance() {
    return instance;
  }

}

