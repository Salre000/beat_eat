using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] Material tapareaMaterial;
    [SerializeField] Material tapareaMaterial2;
    [SerializeField] Material flashMaterial;

    //タップをする位置を生成するクラス
    private CreateTapArea _tapArea;

    //レーン光らせるクラス
    private CreateLineFlash _lineFlash;

    //レーンを区切る線を生成するクラス
    private Createline _line;

    [SerializeField] GameObject areaObject;

    [SerializeField, Header("ラインの分割数")] int _divisionCount = 12;


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
