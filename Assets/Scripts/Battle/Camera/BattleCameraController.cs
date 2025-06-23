using UnityEngine;

public class BattleCameraController : MonoBehaviour {
  public Transform target;

  void LateUpdate() {
    if (target == null)
      return;
    Vector3 desiredPosition = target.position;

    // カメラの位置と向き更新
    transform.position = new Vector3(desiredPosition.x , desiredPosition.y , -10);
    transform.LookAt(target.position);
  }
}
