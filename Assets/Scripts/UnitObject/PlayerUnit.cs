using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{
    //TODO　キャラクターのアセットに置き換えるまでの識別用
    [SerializeField] private Text unitName;

    public override void Initialize(int id,int lv)
    {
        base.Initialize(id, lv);
        unitName.text = status.UnitName();
    }
}
