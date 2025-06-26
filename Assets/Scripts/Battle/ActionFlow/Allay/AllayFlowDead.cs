using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class AllayFlowDead :FlowBase {
  private PlayerUnit owner = null;


  private void Update() {
    if(isStop) return;
    if(owner == null) {
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    Destroy(owner.gameObject);
    owner = null;
  }
}
