using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] Material tapareaMaterial;
    [SerializeField] Material tapareaMaterial2;
    [SerializeField] Material flashMaterial;

    //���݃^�b�v�\�ȃm�[�c�̔z��
    [SerializeField] List<NotesBase> activeObject=new List<NotesBase>();

    //�^�b�v������ʒu�𐶐�����N���X
    private CreateTapArea _tapArea;

    //���[�����点��N���X
    private CreateLineFlash _lineFlash;

    //���[������؂���𐶐�����N���X
    private Createline _line;

    [SerializeField] GameObject areaObject;

    [SerializeField, Header("���C���̕�����")] int _divisionCount = 12;



    private void Awake()
    {
        LineUtility.gameManager = this;

        InGameStatus inGame =new InGameStatus();

        _tapArea = new CreateTapArea();
        _lineFlash = new CreateLineFlash();
        _line = new Createline();

        _tapArea.SetMaterial(tapareaMaterial, tapareaMaterial2);
        _tapArea.CreateMesh(_divisionCount);
        _lineFlash.SetMaterial(flashMaterial);
        _lineFlash.SetFlashLine(_divisionCount);
        _line.SetLine(_divisionCount,areaObject,tapareaMaterial);

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) _tapArea.GetClickPoint(Input.mousePosition, _lineFlash.AddAlpha);

        _lineFlash.SbuAlpha();
        _tapArea.CheckTime();


    }

    public float RangeToDecision(Vector3 position) { return -6.25f-position.z; }


    public void AddActiveObject(NotesBase gameObject) { activeObject.Add(gameObject); }
    public void SbuActiveObject(NotesBase gameObject) { activeObject.Remove(gameObject); }
}
