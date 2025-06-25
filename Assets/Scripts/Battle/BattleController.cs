using UnityEngine;

public class BattleController :MonoBehaviour {
  [SerializeField] private UiBattle uiBattle = null;
  [SerializeField] private PlayerUnitController playerUnitController = null;
  [SerializeField] private EnemyUnitController enemyUnitController = null;
  [SerializeField] private Leader leader = null;

  public void Initialize() {
    playerUnitController.Initialize(this);
    enemyUnitController.Initialize(this);
    uiBattle.Initialize(this);
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

  public void OnChangeLeaderDirection(Vector3 position, Vector3 direction) {
    playerUnitController.LeaderData(position, direction);
  }
}
