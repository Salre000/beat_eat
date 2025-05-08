using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] Material tapareaMaterial;
    [SerializeField] Material tapareaMaterial2;
    [SerializeField] Material flashMaterial;

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
        _tapArea = new CreateTapArea();
        _lineFlash = new CreateLineFlash();
        _line = new Createline();

        _tapArea.SetMaterial(tapareaMaterial, tapareaMaterial2);
        _tapArea.CreateMesh(_divisionCount);
        _lineFlash.SetMaterial(flashMaterial);
        _lineFlash.SetFlashLine(_divisionCount);
        _line.SetLine(_divisionCount,areaObject,tapareaMaterial);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) _tapArea.GetClickPoint(Input.mousePosition, _lineFlash.AddAlpha);

        _lineFlash.SbuAlpha();
        _tapArea.CheckTime();

    }
}
