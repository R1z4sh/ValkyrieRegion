using UnityEngine;
using UnityEngine.UI;

public class SummonPoint : MonoBehaviour
{
    [SerializeField] Text pointText;
    private  float summonPoint = 30;
    
    public void PointReset() { this.summonPoint = 30; }

    public void  setPoint(int point) { summonPoint -= point; }
    public int GetPoint() { return (int)summonPoint; }

    void Update()
    {
        summonPoint += Time.deltaTime;
        pointText.text = "   " + GetPoint().ToString() + "\n" + "          /100";
    }
}
