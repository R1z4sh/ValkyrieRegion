using UniRx;
using UnityEngine;

public class FlowBase : MonoBehaviour {
  protected PlayerUnitController playerUnitcontoller = null;
  protected EnemyUnitController enemyUnitController = null;
  protected ReactiveProperty<int> flowStatus = null;
  public GameObject target = null;
  public virtual void Initialize(
    ReactiveProperty<int> flowStatus ,
    PlayerUnitController playerUnitcontoller ,
    EnemyUnitController enemyUnitController ,
    GameObject target = null) {
    if (target != null)
      this.target = target;
    this.flowStatus = flowStatus;
    this.playerUnitcontoller = playerUnitcontoller;
    this.enemyUnitController = enemyUnitController;
    EventManager.Subscribe<bool>("gameStop" , Stop);
  }


  private void Stop(bool flag) {
    isStop = flag;
  }

  protected bool isStop = false;

  public void Remove() {
    Destroy(this.gameObject);
  }

  public virtual void Detach() {

  }

  public virtual void Step(int act) { this.flowStatus.Value = act; }

  private void OnDestroy() {
    EventManager.Unsubscribe<bool>("gameStop" , Stop);
  }
}
