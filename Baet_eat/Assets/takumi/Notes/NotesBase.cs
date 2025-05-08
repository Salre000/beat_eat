using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesBase : MonoBehaviour
{
    private List<int> laneIndex = new List<int>();
    private const int BaseSpeed = 1;
    protected Vector3 Vec = new Vector3(0,0,(BaseSpeed * InGameStatus.GetSpeed())/50.0f);
    //����^�C�v
    protected enum JudgmentType
    {
        DC,
        Delicious,
        Yammy,
        Good,
        Miss

    }

    public void OnEnable()
    {
        LineUtility.AddActiveObject(this);
    }
    //�m�[�c�ɐG�ꂽ�Ƃ��ɓ����֐�
    public void Hit()
    {
        //����̉��Z������֐�
        SetJudgment();

        //���g��active����Ȃ���ԂɕύX
        LineUtility.SbuActiveObject(this);
       
        //�����������Ȃ�����
        this.gameObject.SetActive(false);


    }

    public void FixedUpdate()
    {
        this.transform.position -= Vec;
    }

    private void SetJudgment()
    {
        int renge = (int)LineUtility.RangeToDecision(this.transform.position);
        if (renge >= 0) InGameStatus.SetJudgments(renge, 0);
        else InGameStatus.SetJudgments(renge, 1);
        float rete = 1;

        switch ((JudgmentType)renge)
        {
            case JudgmentType.DC:
                break;
            case JudgmentType.Delicious:
                rete = 0.8f;
                break;
            case JudgmentType.Yammy:
                rete = 0.5f;
                break;
            case JudgmentType.Good:
                rete = 0.2f;
                break;
            case JudgmentType.Miss:
                break;
        }

        InGameStatus.AddScore(rete);
    }

}
