using UnityEngine;
using UnityEngine.UI;

public class PopupMenuOption : PopupBase<int , bool> {
  [SerializeField] private Button closeButton = null;
  [SerializeField] private Button menuButton = null;

  public override void Initialize(int none , System.Threading.Tasks.TaskCompletionSource<bool> tcs) {
    base.Initialize(none , tcs);
    closeButton.onClick.AddListener(() => CloseWithResult(true));
    menuButton.onClick.AddListener(() => DataLoad());
  }

  private async void DataLoad() {
    await MasterManager.LoadSeverData();
  }
}
