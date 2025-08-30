using UniRx;
using UnityEngine;
using UnityEngine.Rendering;

// �������j�b�g�̈ړ��t���[�𐧌䂷��N���X
public class AllayFlowMove :FlowBase {
  // ���̃t���[�̑ΏۂƂȂ閡�����j�b�g
  private PlayerUnit owner = null;

  // �G���j�b�g��T�����A�U���\�ȏꍇ�͍U���t���[�֑J�ڂ���
  private void UpdateEnemyUnitTarget() {
    target = null;
    float length = float.MaxValue;
    foreach(EnemyUnit unit in enemyUnitController.AlliveUnits()) {
      if(unit.IsDead()) continue; // ���S���Ă���G�͖���
      float distance = Vector3.Distance(unit.transform.position, owner.transform.position);
      if(owner.IsAttackRange(distance)) {
        // �U���͈͓�Ȃ�U���t���[��
        Step((int)AllyUnitAct.Attack);
        return;
      }
      // �U���͈͊O�������G�͈͓�Ȃ�ł�߂��G��^�[�Q�b�g�ɂ���
      if(distance < length && owner.IsSearchRange(distance)) {
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
    if(target != null) return;
    if(Tower.Instance() == null) return;
    if(!IsTowerTarget()) return;
    target = Tower.Instance().SetTaret(1);
    float distance = Vector3.Distance(Tower.Instance().TowerPosition(1), owner.transform.position);
    if(owner.IsAttackRange(distance)) {
      // �^���[���U���͈͓�Ȃ�U���t���[��
      Step((int)AllyUnitAct.Attack);
      return;
    }
  }

  // ���ۂɃ��j�b�g��ړ������鏈��
  private void Move() {
    Vector3 direction = Vector3.zero;
    if(target != null) direction = (target.transform.position - owner.transform.position).normalized; // �^�[�Q�b�g�����ֈړ�
    else direction = new Vector3(1, 0, 0); // �^�[�Q�b�g�����Ȃ���ΉE�����֒��i
    owner.transform.position += direction * owner.Status().Move() * Time.deltaTime;
  }

  // ���t���[���Ă΂��B��Ԃ�^�[�Q�b�g�̍X�V�A�ړ�������s��
  private void Update() {
    if(isStop) return; // ��~�t���O�������Ă���Ή�����Ȃ�
    if(this.flowStatus.Value != (int)AllyUnitAct.CommonMove) return; // �ړ���ԂłȂ���Ή�����Ȃ�
    if(owner == null) {
      // ����̂ݐe�I�u�W�F�N�g����PlayerUnit��擾
      owner = transform.parent.GetComponent<PlayerUnit>();
      return;
    }
    if(owner.IsDead()) {
      // ���j�b�g�����S���Ă���Ύ��S�t���[��
      Step((int)AllyUnitAct.Dead);
      return;
    }
    UpdateEnemyUnitTarget(); // �G���j�b�g�̒T���E�U������
    UpdateTowerTarget();     // �^���[�̒T���E�U������
    Move();                  // ���ۂ̈ړ�����
  }
}
