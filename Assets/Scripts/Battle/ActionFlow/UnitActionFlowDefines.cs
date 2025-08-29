using System.Collections.Generic;
using UnityEngine;
enum AllyUnitAct {
  None = 0,
  CommonMove,
  Attack,
  Dead,
  TowerMove,
}

enum EnemyUnitAct {
  None = 10,
  CommonMove,
  Attack,
  Dead,
  TowerMove,
}

class UnitActionFlowDefines {
  private static Dictionary<int, string> actFilePath = new Dictionary<int, string> {
    { (int)AllyUnitAct.CommonMove, "Prefabs/Battle/Flow/Allay/AllayMove"},
    { (int)AllyUnitAct.Attack, "Prefabs/Battle/Flow/Allay/AllayAttack"},
    { (int)AllyUnitAct.Dead, "Prefabs/Battle/Flow/Allay/AllayDead"},
    { (int)AllyUnitAct.TowerMove, "Prefabs/Battle/Flow/Allay/AllayTowerMove"},
    { (int)EnemyUnitAct.CommonMove, "Prefabs/Battle/Flow/Enemy/EnemyMove"},
    { (int)EnemyUnitAct.Attack, "Prefabs/Battle/Flow/Enemy/EnemyAttack"},
    { (int)EnemyUnitAct.Dead, "Prefabs/Battle/Flow/Enemy/EnemyDead"},
    { (int)EnemyUnitAct.TowerMove, "Prefabs/Battle/Flow/Enemy/EnemyDead"},
  };

  public static string GetActionFlowPath(int act) {
    return actFilePath[act];
  }
}