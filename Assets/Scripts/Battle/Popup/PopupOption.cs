using UnityEngine;
using UnityEngine.UI;

public class PopupOption : PopupBase<int , bool> {
  [SerializeField] private Button closeButton = null;
  [SerializeField] private Button menuButton = null;

  public override void Initialize(int score , System.Threading.Tasks.TaskCompletionSource<bool> tcs) {
    base.Initialize(score , tcs);
    closeButton.onClick.AddListener(() => CloseWithResult(true));
    menuButton.onClick.AddListener(() => RemoveGame());
  }

  private void RemoveGame() {
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);
    CloseWithResult(false);
  }
}
