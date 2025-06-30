using UniRx;
using UnityEngine;

public class FlowBase :MonoBehaviour {
  protected PlayerUnitController playerUnitcontoller = null;
  protected EnemyUnitController enemyUnitController = null;
  protected ReactiveProperty<int> flowStatus = null;
  public virtual void Initialize(
    ReactiveProperty<int> flowStatus,
    PlayerUnitController playerUnitcontoller,
    EnemyUnitController enemyUnitController) {
    this.flowStatus = flowStatus;
    this.playerUnitcontoller = playerUnitcontoller;
    this.enemyUnitController = enemyUnitController;
    EventManager.Subscribe<bool>("gameStop", Stop);
  }

  private void Stop(bool flag) {
    isStop = flag;
  }

  protected bool isStop = false;

  public void Remove() {
    Destroy(this.gameObject);
  }

  public virtual void Step(int act) { this.flowStatus.Value = act; }


  private void Update() {

  }

  private void OnDestroy() {
    EventManager.Unsubscribe<bool>("gameStop", Stop);
  }
}
