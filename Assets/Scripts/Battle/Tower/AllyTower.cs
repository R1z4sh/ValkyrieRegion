using System.Collections;
using UnityEngine;

public class AllyTower : MonoBehaviour {
  [SerializeField] private HealthBar healthGauge = null;
  private int hp = 1000;

  private void Start() {
    healthGauge.SetRate(1f);
    StartCoroutine(Heal());
  }

  private IEnumerator Heal(
  ) {
    yield return new WaitForSeconds(5f);
    hp += 25;
    StartCoroutine(Heal());
  }

  public bool OnDamage(int damage) {
    hp -= damage;
    if (hp <= 0) {
      healthGauge.SetRate(0f);
      EventManager.Trigger<bool>("gameOver" , true);
      return true;
    }
    healthGauge.SetRate((float)hp / 1000f);
    return false;
  }
}
