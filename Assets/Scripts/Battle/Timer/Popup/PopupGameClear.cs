using UnityEngine;
using UnityEngine.UI;

public class PopupGameClear :PopupBase<int, bool> {
  [SerializeField] private Button closeButton = null;

  public override void Initialize(int score, System.Threading.Tasks.TaskCompletionSource<bool> tcs) {
    base.Initialize(score, tcs);
    closeButton.onClick.AddListener(() => Close());
  }

  private void Close() {
    CloseWithResult(true);
  }
}
