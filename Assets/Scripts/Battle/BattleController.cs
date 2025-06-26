using UnityEngine;
using UnityEngine.UI;

public class BattleController :MonoBehaviour {
  [SerializeField] private UiBattle uiBattle = null;
  [SerializeField] private PlayerUnitController playerUnitController = null;
  [SerializeField] private EnemyUnitController enemyUnitController = null;
  [SerializeField] private Leader leader = null;
  [SerializeField] private Button pauseButton = null;

  private bool isStop = false;

  public void Initialize() {
    playerUnitController.Initialize(this);
    enemyUnitController.Initialize(this);
    uiBattle.Initialize(this);
    pauseButton.onClick.AddListener(ShowPopup);
    leader.Initialize(uiBattle.GetJoyStick(), OnChangeLeaderDirection);
    EventManager.Subscribe<bool>("gameClear", GameClear);
    EventManager.Subscribe<bool>("gameOver", GameOver);
  }

  public PlayerUnitController GetPlayerController() {
    return playerUnitController;
  }

  public EnemyUnitController GetEnemyCountoller() {
    return enemyUnitController;
  }

  public int GetSummonPoint() {
    return uiBattle.GetSummonPoint();
  }

  public void OnChangeLeaderDirection(Vector3 position, Vector3 direction) {
    playerUnitController.LeaderData(position, direction);
  }

  private async void GameClear(bool none) {
    EventManager.Trigger<bool>("gameStop", true);
    GameObject prefab = Resources.Load<GameObject>("Prefabs/Battle/Popup/PopupGameClear");
    await PopupManager.Instance().ShowPopup<int, bool, PopupGameClear>(0, prefab);
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }

  private async void GameOver(bool nooe) {
    EventManager.Trigger<bool>("gameStop", true);
    GameObject prefab = Resources.Load<GameObject>("Prefabs/Battle/Popup/PopupGameOver");
    await PopupManager.Instance().ShowPopup<int, bool, PopupGameOver>(0, prefab);
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);

  }

  private async void ShowPopup() {
    EventManager.Trigger<bool>("gameStop", true);
    GameObject prefab = Resources.Load<GameObject>("Prefabs/Battle/Popup/PopupOption");
    await PopupManager.Instance().ShowPopup<int, bool, PopupOption>(0, prefab);
  }




  private void OnDestroy() {
    Destroy(uiBattle.gameObject);
    Destroy(leader.gameObject);
    Destroy(playerUnitController);
    Destroy(enemyUnitController);
    EventManager.Unsubscribe<bool>("gameClear", GameClear);
    EventManager.Unsubscribe<bool>("gameOver", GameOver);
  }
}
