using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{
    //TODO�@�L�����N�^�[�̃A�Z�b�g�ɒu��������܂ł̎��ʗp
    [SerializeField] private Text unitName;

    public override void Initialize(int id,int lv)
    {
        base.Initialize(id, lv);
        unitName.text = status.UnitName();
    }
}
