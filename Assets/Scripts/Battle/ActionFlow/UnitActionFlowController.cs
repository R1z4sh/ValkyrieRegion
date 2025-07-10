using UniRx;
using UnityEngine;

public class UnitActionFlowController {
  private ReactiveProperty<int> flowStatus = new ReactiveProperty<int>(-1);

  private UnitBase unit = null;
  private FlowBase current = null;
  private PlayerUnitController playerUnitController = null;
  private EnemyUnitController enemyUnitController = null;


  public UnitActionFlowController(UnitBase unit, PlayerUnitController playerUnitController, EnemyUnitController enemyUnitController) {
    this.unit = unit;
    this.playerUnitController = playerUnitController;
    this.enemyUnitController = enemyUnitController;
    this.flowStatus.Subscribe(x => {
      this.ChangeFlowStatus();
    });
  }

  private void ChangeFlowStatus() {
    if(flowStatus.Value < 0) return;
    GameObject target = null;
    if(current != null) {
      if(current.target != null) target = current.target;
      current.Remove();
    }
    GameObject prefab = GameObject.Instantiate(Resources.Load<GameObject>(UnitActionFlowDefines.GetActionFlowPath((int)flowStatus.Value)));
    prefab.transform.SetParent(unit.transform, false);
    current = prefab.GetComponent<FlowBase>();
    current.Initialize(flowStatus, playerUnitController, enemyUnitController, target);
  }

  public void To(int status) {
    flowStatus.Value = status;
  }
}
