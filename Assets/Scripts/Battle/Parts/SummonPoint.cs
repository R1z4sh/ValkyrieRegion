using UnityEngine;
using UnityEngine.UI;

public class SummonPoint :MonoBehaviour {
  [SerializeField] Text pointText;
  private float summonPoint = 30;
  private bool isStop = false;
  public void Initialize() {
    PointReset();
    EventManager.Subscribe<int>("UseCost", setPoint);
    EventManager.Subscribe<bool>("gameStop", Stop);
    EventManager.Subscribe<EnemyUnit>("enemyUnitDead", AddSummonPoint);
  }

  private void Stop(bool flag) {
    isStop = flag;
  }

  public void PointReset() { this.summonPoint = 30; }

  public void setPoint(int point) { summonPoint -= point; }
  public int GetPoint() { return (int)summonPoint; }

  private void AddSummonPoint(EnemyUnit data) {
    summonPoint += 10;
  }

  void Update() {
    if(isStop) return;
    if(summonPoint >= 100) return;
    summonPoint += 3 * Time.deltaTime;
    pointText.text = "   " + GetPoint().ToString() + "\n" + "          /100";
  }
}
