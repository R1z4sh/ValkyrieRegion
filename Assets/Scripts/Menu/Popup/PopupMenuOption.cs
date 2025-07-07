using UnityEngine;
using UnityEngine.UI;

public class PopupMenuOption : PopupBase<int , bool> {
  [SerializeField] private Button closeButton = null;
  [SerializeField] private Button debugMenuButton = null;
  public override void Initialize(int none , System.Threading.Tasks.TaskCompletionSource<bool> tcs) {
    base.Initialize(none , tcs);
    closeButton.onClick.AddListener(() => CloseWithResult(true));
    debugMenuButton.onClick.AddListener(() => ShowDebugMenu());
  }

  private async void DataLoad() {
    await MasterManager.LoadSeverData();
  }


  private async void ShowDebugMenu() {
    GameObject prefab = Resources.Load<GameObject>("Prefabs/Debug/PopupDebug");
    await PopupManager.Instance().ShowPopup<bool , bool , PopupDebug>(false , prefab);
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }

}
