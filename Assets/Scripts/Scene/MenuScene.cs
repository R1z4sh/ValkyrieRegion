using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuScene :SceneBase {

  public Button button;
  [SerializeField] private Button optionButton = null;
  [SerializeField] private Button shopButton = null;


  public override async Task Initialize(SceneData data = null) {
    BattleData battleData = new BattleData();
    battleData.stageId = 100001;
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game, battleData));
    optionButton.onClick.AddListener(() => ShowPopup());
    button.onClick.AddListener(() => GameSceneManager.Instance().ChangeScene(SceneName.Game, battleData));
    shopButton.onClick.AddListener(()=> ShowShop());
  }

  public override void Fainalize() { }
  private async void ShowPopup() {
    bool isRestart = await PopupManager.Instance().ShowPopup<int, bool, PopupMenuOption>(PopupName.PopupMemuOption, 0);

    if(!isRestart) return;

  }

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
}
