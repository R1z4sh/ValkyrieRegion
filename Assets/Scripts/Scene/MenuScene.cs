using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene :SceneBase {

  public Button button;
  [SerializeField] private Button optionButton = null;


  public override async Task Initialize(SceneData data = null) {
    BattleData battleData = new BattleData();
    battleData.stageId = 100001;
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game, battleData));
    optionButton.onClick.AddListener(() => ShowPopup());
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game, battleData));
  }

  public override void Fainalize() { }
  private async void ShowPopup() {
    bool isRestart = await PopupManager.Instance().ShowPopup<int, bool, PopupMenuOption>(PopupName.PopupMemuOption, 0);

    if(!isRestart) return;

  }
}
