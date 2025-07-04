using System.Threading.Tasks;
using UnityEngine.UI;

public class MenuScene :SceneBase {

  public Button button;
  //public DoubleTapButton button;

  public override async Task Initialize(SceneData data = null) {
    BattleData battleData = new BattleData();
    battleData.stageId = 100001;
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game, battleData));
    //button.SetOnClickEvent(() => ShowPopup());
  }


  public override void Fainalize() { }

  private void Update() {

  }
}
