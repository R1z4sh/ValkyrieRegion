using System;
using UnityEngine;
using UnityEngine.UI;

public class SummonItem :MonoBehaviour {
  [SerializeField] private Button summonButton;
  [SerializeField] private Image unitImage;
  [SerializeField] private Text cost;
  [SerializeField] private Text unitName;

  private bool isStop = false;
  private BattleController battleController = null;
  private UnitData unit;
  public void Initialize(BattleController controller, UnitData unit) {
    this.unit = unit;
    this.battleController = controller;
    string unitIdPath = string.Format("{0:D4}", unit.m_unit_id);
    unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
    cost.text = unit.cost.ToString();
    unitName.text = unit.unit_name;
    summonButton.onClick.AddListener(onClickSummonButton);
    EventManager.Subscribe<bool>("gameStop", Stop);
  }

  private void Stop(bool flag) {
    isStop = flag;
  }

  private void onClickSummonButton() {
    if(isStop) return;
    battleController.GetPlayerController().Summon(unit.m_unit_id, unit.cost);
  }

  private void Update() {
    summonButton.enabled = !isStop;
  }
}
