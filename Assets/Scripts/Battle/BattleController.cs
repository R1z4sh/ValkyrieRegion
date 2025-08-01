using UnityEngine;
using UnityEngine.UI;

public class BattleController :MonoBehaviour {
  [SerializeField] private UiBattle uiBattle = null;
  [SerializeField] private SpriteRenderer background = null;
  [SerializeField] private PlayerUnitController playerUnitController = null;
  [SerializeField] private EnemyUnitController enemyUnitController = null;
  [SerializeField] private Leader leader = null;
  [SerializeField] private Button pauseButton = null;
  [SerializeField] private Camera minimapCamera = null;
  public int stageId;

  public void Initialize(int stageId) {
    this.stageId = stageId;
    CreateState();
    playerUnitController.Initialize(this);
    enemyUnitController.Initialize(this);
    uiBattle.Initialize(this);
    pauseButton.onClick.AddListener(ShowPopup);
    leader.Initialize(uiBattle.GetJoyStick(), OnChangeLeaderDirection);
    EventManager.Subscribe<bool>("gameClear", GameClear);
    EventManager.Subscribe<bool>("gameOver", GameOver);
  }

  private void CreateState() {
    StageData stageData = MasterManager.stageMaster.GetStageData(stageId);
    leader.gameObject.transform.position = new Vector3(stageData.PlayerPosX(), stageData.PlayerPosY(), 0);
    leader.SetMoveRange(new Vector2(stageData.ScaleX(), stageData.ScaleY()));
    Tower.Instance().InitializeToewrPosition(
      new Vector3(stageData.PlayerPosX(), stageData.PlayerTowerPosY(), 0),
      new Vector3(stageData.EnemyTowerPosX(), stageData.EnemyTowerPosY(), 0)
    );
    background.size = new Vector2(stageData.ScaleX(), stageData.ScaleY());
    background.transform.position = new Vector3(stageData.ScaleX() / 2, 0, 0);
    minimapCamera.transform.position = new Vector3(stageData.ScaleX() / 2, 0, -10);
    minimapCamera.orthographicSize = stageData.ScaleX() / 2;
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
    await PopupManager.Instance().ShowPopup<int, bool, PopupGameClear>(PopupName.PopupGameClear, 0);
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }

  private async void GameOver(bool nooe) {
    EventManager.Trigger<bool>("gameStop", true);
    await PopupManager.Instance().ShowPopup<int, bool, PopupGameOver>(PopupName.PopupGameOver, 0);
    GameSceneManager.Instance().ChangeScene(SceneName.Menu);
  }

  private async void ShowPopup() {
    EventManager.Trigger<bool>("gameStop", true);
    bool isRestart = await PopupManager.Instance().ShowPopup<int, bool, PopupOption>(PopupName.PopupBattelOption, 0);

    if(!isRestart)
      return;
    EventManager.Trigger<bool>("gameStop", false);
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
