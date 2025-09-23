using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum MenuSceneBtnType 
{
  Battle,
  Shop,
  Option,
  Mission

}

public class MenuScene :SceneBase {

  public Button button;
  [SerializeField]
  private List<Button> btnList;
  [SerializeField] private Button optionButton = null;
  [SerializeField] private Button shopButton = null;


  public override async Task Initialize(SceneData data = null) {
    BattleData battleData = new BattleData();
    battleData.stageId = 100001;
    btnList[(int)MenuSceneBtnType.Battle].onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game, battleData));
    btnList[(int)MenuSceneBtnType.Option].onClick.AddListener(() => ShowPopup());
    btnList[(int)MenuSceneBtnType.Shop].onClick.AddListener(()=> ShowShop());
    btnList[(int)MenuSceneBtnType.Mission].onClick.AddListener(() => ShowMission());
  }

  public override void Fainalize() { }
  private async void ShowPopup() {
    bool isRestart = await PopupManager.Instance().ShowPopup<int, bool, PopupMenuOption>(PopupName.PopupMemuOption, 0);

    if(!isRestart) return;

  }

  /// <summary>
  /// ショップリスト表示
  /// </summary>
  private async void ShowShop() 
  {
    List<PopupShopItemModel> list = new List<PopupShopItemModel>();
    //テストデータ
    for(int i = 0; i < 30; i++) 
    {
      var item = new PopupShopItemModel();
      item.InjectData(i+1,$"item_{i+1}");
      list.Add(item);
    }

    await PopupManager.Instance().ShowPopup<List<PopupShopItemModel>, bool, PopupShopView>(PopupName.PopupShop, list);
  }

  /// <summary>
  /// ミッションリスト表示
  /// </summary>
  private async void ShowMission()
  {
    List<PopupMissionItemModel> list = new List<PopupMissionItemModel>();
    //テストデータ
    for (int i = 0; i < 30; i++)
    {
      var item = new PopupMissionItemModel();
      item.InjectData(i + 1, $"item_{i + 1}");
      list.Add(item);
    }

    await PopupManager.Instance().ShowPopup<List<PopupMissionItemModel>, bool, PopupMissionView>(PopupName.PopupMission, list);
  }

}
