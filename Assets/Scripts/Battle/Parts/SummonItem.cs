using System;
using UnityEngine;
using UnityEngine.UI;

public class SummonItem : MonoBehaviour
{
    [SerializeField] private Button summonButton;
    [SerializeField] private Image unitImage;
    [SerializeField] private Text cost;
    [SerializeField] private Text unitName;

    private BattleController battleController = null;
    private UnitData unit;
    public void Initialize(BattleController controller,UnitData unit){
        this.unit = unit;
        this.battleController = controller;
        string unitIdPath = string.Format("{0:D4}", unit.id);
        unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
        cost.text = unit.cost.ToString();
        unitName.text = unit.unitName;
        summonButton.onClick.AddListener(onClickSummonButton);
    }

    private void  onClickSummonButton(){
        battleController.GetPlayerController().Summon(unit.id,unit.cost);
    }
}
