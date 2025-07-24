using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

// 味方ユニットの移動フローを制御するクラス
public class AllayFlowMove :FlowBase {
  // このフローの対象となる味方ユニット
  private PlayerUnit owner = null;

  // 敵ユニットを探索し、攻撃可能な場合は攻撃フローへ遷移する
  private void UpdateEnemyUnitTarget() {
    target = null;
    float length = float.MaxValue;
    foreach(EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      if(unit.IsDead()) continue; // 死亡している敵は無視
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance)) {
        // 攻撃範囲内なら攻撃フローへ
        Step((int)AllyUnitAct.Attack);
        return;
      }
      // 攻撃範囲外だが索敵範囲内なら最も近い敵をターゲットにする
      if(distance < length && owner.IsSearchRange(distance)) {
        length = distance;
        target = unit.gameObject;
      }
    }
  }

  // タワーをターゲットにする処理。敵ユニットがいない場合やターゲットが未設定の場合に実行
  private void UpdateTowerTarget() {
    if(target != null) return; // 既にターゲットがいれば何もしない
    if(Tower.Instance() == null) return; // タワーが存在しなければ何もしない
    target = Tower.Instance().SetTaret(1); // タワーをターゲットに設定
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(1), owner.transform.position);
    if(owner.IsAttackRange(distance)) {
      // タワーが攻撃範囲内なら攻撃フローへ
      Step((int)AllyUnitAct.Attack);
      return;
    }
  }

  // 実際にユニットを移動させる処理
  private void Move() {
    Vector3 direction = Vector3.zero;
    if(target != null) direction = (target.transform.position - owner.transform.position).normalized; // ターゲット方向へ移動
    else direction = new Vector3(1, 0, 0); // ターゲットがいなければ右方向へ直進
    owner.transform.position += direction * owner.Status().Move() * Time.deltaTime;
  }

  // 毎フレーム呼ばれる。状態やターゲットの更新、移動処理を行う
  private void Update() {
    if(isStop) return; // 停止フラグが立っていれば何もしない
    if(this.flowStatus.Value != (int)AllyUnitAct.CommonMove) return; // 移動状態でなければ何もしない
    if(owner == null) {
      // 初回のみ親オブジェクトからPlayerUnitを取得
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    if(owner.IsDead()) {
      // ユニットが死亡していれば死亡フローへ
      Step((int)AllyUnitAct.Dead);
      return;
    }
    UpdateEnemyUnitTarget(); // 敵ユニットの探索・攻撃判定
    UpdateTowerTarget();     // タワーの探索・攻撃判定
    Move();                  // 実際の移動処理
  }
}
