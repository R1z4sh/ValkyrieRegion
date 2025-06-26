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
    pauseButton.onClick.AddListener(GameStop);
    leader.Initialize(uiBattle.GetJoyStick(), OnChangeLeaderDirection);
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

  private void GameStop() {
    isStop = !isStop;
    EventManager.Trigger<bool>("gameStop", isStop);
  }

  public void OnChangeLeaderDirection(Vector3 position, Vector3 direction) {
    playerUnitController.LeaderData(position, direction);
  }
}
