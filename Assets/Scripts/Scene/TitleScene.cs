using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

//シーン引継ぎ用インターフェイス

public class TitleScene :SceneBase {
  [SerializeField] Animation logoAnimation = null;
  [SerializeField] Animation warningAnimation = null;
  [SerializeField] GameObject gameStartItems = null;
  [SerializeField] Button gameStartButton = null;
  [SerializeField] AudioSource audio = null;

  public override async Task Initialize(SceneData data = null) {
    StartCoroutine(LogoAnimation());
    gameStartButton.onClick.AddListener(GameStart);
    gameStartItems.SetActive(false);
  }
  public override void Fainalize() { }

  private IEnumerator LogoAnimation() {
    logoAnimation.Play();
    yield return null;
    yield return new WaitForSeconds(logoAnimation.clip.length);
    StartCoroutine(WarningAnimation());
  }

  private IEnumerator WarningAnimation() {
    warningAnimation.Play();
    yield return null;
    yield return new WaitForSeconds(warningAnimation.clip.length);
    gameStartItems.SetActive(true);
    audio.Play();
  }

  public void GameStart() {
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }
}
