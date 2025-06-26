using System.Collections;
using UnityEngine;

public class Tower :MonoBehaviour {

  private static Tower instance = null;
  private int hp = 1000;

  private void Start() {
    instance = this;
  }

  public static Tower Instance() {
    return instance;
  }

  private IEnumerator Heal(
    ) {
    yield return new WaitForSeconds(5f);
    hp += 25;
  }

  public void OnDamage(int damage) {
    this.hp -= damage;
    if(hp <= 0) OnDead();
  }
  private void OnDead() {

  }
}

