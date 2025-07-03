using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tower :MonoBehaviour {
  [SerializeField] AllayTower allayTower = null;
  [SerializeField] EnemyTower enemyTower = null;
  private static Tower instance = null;

  private void Start() {
    instance = this;
  }

  public static Tower Instance() {
    return instance;
  }

  public void OnDamage(int team, int damage) {
    int Allay = 0;
    int Enemy = 1;
    if(team == Allay && allayTower.OnDamage(damage)) EventManager.Trigger<bool>("gameOver", true);
    if(team == Enemy && enemyTower.OnDamage(damage)) EventManager.Trigger<bool>("gameClear", true);

  }
}

