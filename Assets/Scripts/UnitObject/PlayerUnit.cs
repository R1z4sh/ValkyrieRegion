using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{
    //TODO�@�L�����N�^�[�̃A�Z�b�g�ɒu��������܂ł̎��ʗp
    [SerializeField] private Image unitImage;

    public override void Initialize(int id,int lv)
    {
        base.Initialize(id, lv);
        string unitIdPath = string.Format("{0:D2}", id);
        unitImage.sprite = Resources.Load<Sprite>("Sprites/Battle/Unit/Unit" + unitIdPath);
    }
}
