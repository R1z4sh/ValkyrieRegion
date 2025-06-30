using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower :MonoBehaviour {
  [SerializeField] HealthBar healthGauge = null;
  private static Tower instance = null;
  private int hp = 1000;

  private void Start() {
    instance = this;
    healthGauge.SetRate(1f);
    StartCoroutine(Heal());
  }

  public static Tower Instance() {
    return instance;
  }

  private IEnumerator Heal(
    ) {
    yield return new WaitForSeconds(5f);
    hp += 25;
    StartCoroutine(Heal());
  }

  public void OnDamage(int damage) {
    this.hp -= damage;
    healthGauge.SetRate((float)hp / 1000);
    if(hp <= 0) OnDead();
  }
  private void OnDead() {
    EventManager.Trigger<bool>("gameOver", true);
  }
}

