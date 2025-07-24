using UnityEngine;

// 敵ユニットの移動フローを制御するクラス
public class EnemyFlowMove :FlowBase {
  // このフローの対象となる敵ユニット
  private EnemyUnit owner = null;
  // 攻撃対象となるプレイヤーユニット
  private PlayerUnit target = null;

  // プレイヤーユニットを探索し、攻撃可能な場合は攻撃フローへ遷移する
  private void UpdatePlayerUnitTarget() {
    foreach(PlayerUnit unit in playerUnitcontoller.AlliveUnits()) {
      if(unit.IsDead()) continue; // 死亡している味方は無視
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance)) {
        // 攻撃範囲内なら攻撃フローへ
        Step((int)EnemyUnitAct.Attack);
        return;
      }
    }
  }

  // タワーをターゲットにする処理。タワーが攻撃範囲内なら攻撃フローへ
  private void UpdateTowerTarget() {
    if(Tower.Instance() == null) return; // タワーが存在しなければ何もしない
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(0), owner.transform.position);
    if(owner.IsAttackRange(distance)) {
      // タワーが攻撃範囲内なら攻撃フローへ
      Step((int)EnemyUnitAct.Attack);
      return;
    }
  }

  // 実際にユニットを移動させる処理
  private void Move() {
    if(Tower.Instance() == null) return; // タワーが存在しなければ何もしない
    Vector3 direction = (Tower.Instance().TowerPosition(0) - owner.transform.position).normalized; // タワー方向へ移動
    owner.transform.position += direction * owner.Status().Move() * Time.deltaTime;
  }

  // 毎フレーム呼ばれる。状態やターゲットの更新、移動処理を行う
  private void Update() {
    if(isStop) return; // 停止フラグが立っていれば何もしない
    if(this.flowStatus.Value != (int)EnemyUnitAct.CommonMove) return; // 移動状態でなければ何もしない
    if(owner == null) {
      // 初回のみ親オブジェクトからEnemyUnitを取得
      owner = transform.parent.GetComponent<EnemyUnit>();
      return;
    }
    if(owner.IsDead()) {
      // ユニットが死亡していれば死亡フローへ
      Step((int)EnemyUnitAct.Dead);
      return;
    }
    UpdateTowerTarget();        // タワーの探索・攻撃判定
    UpdatePlayerUnitTarget();   // プレイヤーユニットの探索・攻撃判定
    Move();                    // 実際の移動処理
  }
}
