using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private Text timerCount;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        SetTimrCount();
    }

   private void SetTimrCount()
    {
        int seconds = Mathf.FloorToInt(timer % 60);
        int minutes = Mathf.FloorToInt((timer / 60) % 60);
        timerCount.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
