using UnityEngine;

public class JacketDishChange : MonoBehaviour
{
    // �Ȃ��؂�ւ������M���Ɖ����ăW���P�b�g��؂�ւ���

    [SerializeField] private Transform _jacketDish;
    private int _nowMusicCard = -1;
    private int _currentMusicCard = -1;

    // �O���̖ړI�n
    [SerializeField] private Transform _targetOffScreen;
    // �����̖ړI�n
    private Vector3 _targetOnScreen = new Vector3();
    private Vector3 _lerpPosition = new Vector3();
    private bool _isMove = false;
    private float _speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _currentMusicCard = MusicManager.instance.GetSelectMusicNumber();
        _nowMusicCard = _currentMusicCard;
        _targetOnScreen = _jacketDish.transform.position;
        _isMove = false;
        _lerpPosition = _targetOffScreen.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetMusicNumber();
        OnScreenDishMove();
    }

    /// <summary>
    /// �Ȃ��؂�ւ��ΎM�̈ʒu����ʊO�ɔz�u����
    /// </summary>
    private void SetMusicNumber()
    {
        _currentMusicCard = MusicManager.instance.GetSelectMusicNumber();
        if (_nowMusicCard == _currentMusicCard) return;
        _lerpPosition = _targetOffScreen.position;
        _nowMusicCard = _currentMusicCard;
        _isMove = true;
    }

    /// <summary>
    /// ��ʊO���犊�炩�ɖړI�n�ւƈړ�������
    /// </summary>
    private void OnScreenDishMove()
    {
        {
            if (!_isMove) return;
            _lerpPosition = Vector3.Lerp(_lerpPosition, _targetOnScreen, _speed * Time.deltaTime);
            _jacketDish.position = _lerpPosition;
            if ((_lerpPosition - _targetOnScreen).sqrMagnitude < 0.01f)
                _isMove = false;
        }
    }
}
