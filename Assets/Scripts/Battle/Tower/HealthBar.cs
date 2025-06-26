using UnityEngine;

public class HealthBar :MonoBehaviour {
  [SerializeField] private Transform rateTransform;

  public void SetRate(float rate) {
    rate = Mathf.Clamp01(rate);
    Vector3 scale = rateTransform.localScale;
    scale.x = rate;
    rateTransform.localScale = scale;
    float fullWidth = 1f;
    Vector3 pos = rateTransform.localPosition;
    pos.x = -(1 - rate) * fullWidth * 0.5f;
    rateTransform.localPosition = pos;
  }
}
