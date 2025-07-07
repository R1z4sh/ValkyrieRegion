using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : SceneBase {

  public Button button;
  [SerializeField] private Button optionButton = null;
  //public DoubleTapButton button;

  public override async Task Initialize(SceneData data = null) {
    BattleData battleData = new BattleData();
    battleData.stageId = 100001;
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game , battleData));
    optionButton.onClick.AddListener(() => ShowPopup());
  }


  public override void Fainalize() { }
  private async void ShowPopup() {
    GameObject prefab = Resources.Load<GameObject>("Prefabs/Menu/Popup/PopupMenuOption");
    bool isRestart = await PopupManager.Instance().ShowPopup<int , bool , PopupMenuOption>(0 , prefab);
  }
}
