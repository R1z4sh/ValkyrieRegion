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
    leader.gameObject.transform.position = new Vector3(stageData.player_pos_x, stageData.player_pos_y, 0);
    leader.SetMoveRange(new Vector2(stageData.scale_x, stageData.scale_y));
    Tower.Instance().InitializeToewrPosition(
      new Vector3(stageData.player_tower_pos_x, stageData.player_tower_pos_y, 0),
      new Vector3(stageData.enemy_tower_pos_x, stageData.enemy_tower_pos_y, 0)
    );
    background.size = new Vector2(stageData.scale_x, stageData.scale_y);
    background.transform.position = new Vector3(stageData.scale_x / 2, 0, 0);
    minimapCamera.transform.position = new Vector3(stageData.scale_x / 2, 0, -10);
    minimapCamera.orthographicSize = stageData.scale_x / 2;
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
