using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : UnitBase {
  public override void Initialize(int id , int lv , int index) {
    base.Initialize(id , lv , index);
    string unitIdPath = string.Format("{0:D4}" , id);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
  }
}
