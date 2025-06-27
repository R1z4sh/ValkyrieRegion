using UnityEngine;

public class Rader :MonoBehaviour {
  [SerializeField] private Leader leader = null;
  [SerializeField] private PlayerUnitController playerUnitController = null;
  [SerializeField] private EnemyUnitController enemyUnitController = null;

  [SerializeField] private GameObject playerUnit = null;
  [SerializeField] private GameObject enemyUnit = null;

  [SerializeField] private GameObject tower = null;
  [SerializeField] private GameObject playerUnitRoot = null;
  [SerializeField] private GameObject enemyUnitRoot = null;


  private void UpdatePlayerUnit() {
    if(playerUnitRoot.transform.childCount > playerUnitController.AlliveUnits().Count) {
      for(int i = playerUnitRoot.transform.childCount - playerUnitController.AlliveUnits().Count; i >= 0; --i) {
        Destroy(playerUnitRoot.transform.GetChild(i - 1).gameObject);
      }
    } else if(playerUnitRoot.transform.childCount < playerUnitController.AlliveUnits().Count) {
      for(int i = 0; i < playerUnitController.AlliveUnits().Count; ++i) {
        GameObject newPlayerUnit = Instantiate(playerUnit, Vector3.zero, Quaternion.identity);
        newPlayerUnit.transform.SetParent(playerUnitRoot.transform, false);
      }
    }

    for(int i = 0; i < playerUnitRoot.transform.childCount; ++i) {
      PlayerUnit player = playerUnitController.AlliveUnits()[i];
      float posX = (player.transform.position.x - leader.transform.position.x) * 2;
      float posY = (player.transform.position.y - leader.transform.position.y) * 2;
      playerUnitRoot.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
    }
  }


  private void UpdateEnemyUnit() {
    if(enemyUnitRoot.transform.childCount > enemyUnitController.AlliveUnits().Count) {
      for(int i = enemyUnitRoot.transform.childCount - enemyUnitController.AlliveUnits().Count; i >= 0; --i) {
        Destroy(enemyUnitRoot.transform.GetChild(i - 1).gameObject);
      }
    } else if(enemyUnitRoot.transform.childCount < enemyUnitController.AlliveUnits().Count) {
      for(int i = 0; i < enemyUnitController.AlliveUnits().Count; ++i) {
        GameObject newEnemyUnit = Instantiate(enemyUnit, Vector3.zero, Quaternion.identity);
        newEnemyUnit.transform.SetParent(enemyUnitRoot.transform, false);
      }
    }

    for(int i = 0; i < enemyUnitRoot.transform.childCount; ++i) {
      EnemyUnit enemy = enemyUnitController.AlliveUnits()[i];
      float posX = (enemy.transform.position.x - leader.transform.position.x) * 2;
      float posY = (enemy.transform.position.y - leader.transform.position.y) * 2;
      enemyUnitRoot.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
    }
  }

  private void UpdateTower() {
    float posX = (Tower.Instance().transform.position.x - leader.transform.position.x) * 2;
    float posY = (Tower.Instance().transform.position.y - leader.transform.position.y) * 2;
    tower.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
  }

  private void Update() {
    UpdatePlayerUnit();
    UpdateEnemyUnit();
    UpdateTower();
  }
}
