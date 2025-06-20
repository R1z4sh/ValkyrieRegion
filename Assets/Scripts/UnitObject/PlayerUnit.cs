using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{
    //TODO　キャラクターのアセットに置き換えるまでの識別用
    [SerializeField] private Image unitImage;

    public override void Initialize(int id,int lv)
    {
        base.Initialize(id, lv);
        string unitIdPath = string.Format("{0:D2}", id);
        unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
    }
}
