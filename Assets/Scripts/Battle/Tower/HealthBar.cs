using UnityEngine;
using UnityEngine.UI;

public class HealthBar :MonoBehaviour {
  [SerializeField] private RectTransform rateGauge;
  [SerializeField] private float gaugeWidth;
  public void SetRate(float rate) {
    rateGauge.sizeDelta = new Vector2(gaugeWidth * rate, rateGauge.sizeDelta.y);
  }
}
