using UnityEngine;

public class AttackRangeController :MonoBehaviour {
  [SerializeField] GameObject inRange = null;
  [SerializeField] GameObject outRange = null;

  public void Initialize(float min, float max) {
    outRange.transform.localScale = new Vector3(max, max, 1f);
    inRange.transform.localScale = new Vector3(min / max, min / max, 1f);
    SetActive(false);
  }

  public void SetActive(bool flag) {
    this.gameObject.SetActive(flag);
  }
}
