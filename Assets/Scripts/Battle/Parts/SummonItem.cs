using System;
using UnityEngine;
using UnityEngine.UI;

public class SummonItem : MonoBehaviour
{
    [SerializeField]private Button summonButton;


    private UnitData unit;
    public void Initialize(UnitData unit){
        this.unit = unit;
        summonButton.onClick.AddListener(onClickSummonButton);
    }

    private void  onClickSummonButton(){
        PlayerUnitController.Instance().Summon(unit.id);
    }
}
