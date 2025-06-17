using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour {
  void Start() {
    GameSceneManager.Instance().ChangeSceneAsync(SceneName.Title);
  }
}
