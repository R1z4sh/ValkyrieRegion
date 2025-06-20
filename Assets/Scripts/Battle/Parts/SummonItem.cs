using System;
using UnityEngine;
using UnityEngine.UI;

public class SummonItem : MonoBehaviour
{
    [SerializeField] private Button summonButton;
    [SerializeField] private Image unitImage;
    [SerializeField] private Text cost;
    [SerializeField] private Text unitName;

    private UnitData unit;
    public void Initialize(UnitData unit){
        this.unit = unit;
        string unitIdPath = string.Format("{0:D2}", unit.id);
        unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
        cost.text = unit.cost.ToString();
        unitName.text = unit.unitName;
        summonButton.onClick.AddListener(onClickSummonButton);
    }

    private void  onClickSummonButton(){
        PlayerUnitController.Instance().Summon(unit.id,unit.cost);
    }
}
