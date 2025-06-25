using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit :UnitBase {
  public override void Initialize(
    PlayerUnitController playerUnitController,
    EnemyUnitController enemyUnitController,
    int unitId, int lv) {
    base.Initialize(playerUnitController, enemyUnitController, unitId, lv);
    string unitIdPath = string.Format("{0:D4}", unitId);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
  }

  protected override void Move() {
    base.Move();

  }
}
