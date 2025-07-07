using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene :SceneBase {

  public Button button;
  [SerializeField] private Button debugMenuButton = null;
  //public DoubleTapButton button;

  public override async Task Initialize(SceneData data = null) {
    BattleData battleData = new BattleData();
    battleData.stageId = 100001;
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game, battleData));
    debugMenuButton.onClick.AddListener(showDebug);
    //button.SetOnClickEvent(() => ShowPopup());
  }



  private async void showDebug() {
    GameObject prefab = Resources.Load<GameObject>("Prefabs/Debug/PopupDebug");
    await PopupManager.Instance().ShowPopup<bool, bool, PopupDebug>(false, prefab);
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }
  public override void Fainalize() { }

  private void Update() {

  }
}
