using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public bool _isStarted = false;      // �������� ���ۿ���

    public bool _isPaused = true;           // ���۰��ɻ���

    //���� ����
    //false:�Ķ�,true:��Ȳ
    public bool _isBlue = false;

    public int _currentStageNum = 0;        // ���� ����������ȣ �ڵ��Ҵ�
    public Stage _currentStage = null;      // ���� ���������� ������Ʈ �ڵ��Ҵ�
    private DragAndDrop _dnd = null;        // ī�޶��� �巡�� �� ��� �Ҵ�

    public int _currnetBallNum = 0;         // ���� �� ����
    public int _escapedBallNum = 0;         // Ż���� �� ����

    public GameObject _camera = null;       // �����ʿ� : ī�޶� ������Ʈ
    public GameObject[] _stages = null;     // �����ʿ� : ��ֹ� ������Ʈ��

    public float _lowest;
    private float _lastLowest = 0;

    void Start()
    {
        Init();
    }

    void Update()
    {
        // ���콺 �Է½� �������
        if (!_isPaused && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isBlue = !_isBlue;
        }

        //������ ����
        if (_lowest != _lastLowest)
        {
            _camera.transform.position
                = new Vector3(_camera.transform.position.x,
                _lowest + 2.6f,
                _camera.transform.position.z);
            _lastLowest = _lowest;
        }

        // ����� 3���� �����
        if(_isStarted && _currnetBallNum == 0)
        {
            _isStarted = false;
            _isPaused = true;
            Invoke("Restart", 3);
        }
    }

    private void Init()
    {
        _currnetBallNum = 0;
        _escapedBallNum = 0;

        //������������
        GameObject _tempStage;
        _tempStage = GameObject.Instantiate(_stages[_currentStageNum], this.transform.position, Quaternion.identity);

        //���� �������� ������Ʈ �Ҵ�
        _currentStage = _stages[_currentStageNum].gameObject.GetComponent<Stage>();

        //�ʱ� ������ = õ�����
        _lowest = _currentStage._top.transform.position.y;

        //���� ���������� �� �ڽ� �Ҵ�
        _dnd = _camera.gameObject.GetComponentInChildren<DragAndDrop>();
        _dnd._ballBox = _tempStage.gameObject.GetComponent<Stage>()._ballBoxs[0];
        _dnd.Init();

        _isStarted = false;
        _isPaused = true;
        _isBlue = false;
    }

    private void CleanStage()
    {
        // ���� �������� ����
        GameObject _tempStage = GameObject.FindGameObjectWithTag("Stage");
        if(_tempStage != null)
            Destroy(_tempStage);

        // ���� �� ����
        GameObject[] _tempballs = GameObject.FindGameObjectsWithTag("Ball");
        for(int i = 0; i<_tempballs.Length;i++)
        {
            Destroy(_tempballs[i]);
        }
    }

    private void Restart()
    {
        //�ʵ� û��
        CleanStage();
        //�ʱ�ȭ
        Init();
    }

    // ���� �������� �ҷ�����
    // GoalCheck ��ũ��Ʈ���� ���
    public void CallNextStage()
    {
        //������ ������������ ������
        if (_currentStageNum + 1 >= _stages.Length)
            return;

        //�ʵ� û��
        CleanStage();
        //�������� ��ȣ����
        _currentStageNum++;
        //�ʱ�ȭ
        Init();
    }
    
}
