using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

public class AllayFlowMove : FlowBase {
  private PlayerUnit owner = null;

  // �G���j�b�g��T�����A�U���\�ȏꍇ�͍U���t���[�֑J�ڂ���
  private void UpdateEnemyUnitTarget() {
    target = null;
    float length = float.MaxValue;
    foreach (EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      if (unit.IsDead())
        continue;
      float distance = Vector3.Distance(unit.transform.position , owner.transform.position);
      if (owner.IsAttackRange(distance)) {
        Step((int)AllyUnitAct.Attack);
        return;
      }
      //射程外なら移動
      if (distance < length && owner.IsSearchRange(distance)) {
        length = distance;
        target = unit.gameObject;
      }
    }
  }

  private bool IsTowerTarget() {
    float range = Mathf.Abs(this.owner.transform.position.x - Tower.Instance().TowerPosition(1).x);
    return range < 5;
  }

  private void UpdateTowerTarget() {
    if (target != null)
      return;
    if (Tower.Instance() == null)
      return;
    if (!IsTowerTarget())
      return;
    target = Tower.Instance().SetTarget(1);
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(1) , owner.transform.position);
    if (owner.IsAttackRange(distance)) {
      Step((int)AllyUnitAct.Attack);
      return;
    }
  }

  // ���ۂɃ��j�b�g��ړ������鏈��
  private void Move() {
    Vector3 direction = Vector3.zero;
    if (target != null)
      direction = (target.transform.position - owner.transform.position).normalized;
    else
      direction = new Vector3(1 , 0 , 0);
    owner.transform.position += direction * owner.Status().Move() * Time.deltaTime;
  }

  // ���t���[���Ă΂��B��Ԃ�^�[�Q�b�g�̍X�V�A�ړ�������s��
  private void Update() {
    if (isStop)
      return;
    if (this.flowStatus.Value != (int)AllyUnitAct.CommonMove)
      return;
    if (owner == null) {
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    if (owner.IsDead()) {
      Step((int)AllyUnitAct.Dead);
      return;
    }
    UpdateEnemyUnitTarget(); // �G���j�b�g�̒T���E�U������
    UpdateTowerTarget();     // �^���[�̒T���E�U������
    Move();                  // ���ۂ̈ړ�����
  }
}
