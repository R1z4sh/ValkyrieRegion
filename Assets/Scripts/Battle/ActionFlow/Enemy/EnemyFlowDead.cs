using UnityEngine;
using UnityEngine.Rendering;

public class EnemyFlowDeads :FlowBase {
  private EnemyUnit owner = null;


  private void Update() {
    if(isStop) return;
    if(owner == null) {
      owner = transform.parent.GetComponent<EnemyUnit>();
      return;
    }
    Destroy(owner.gameObject);
    owner = null;
  }
}
