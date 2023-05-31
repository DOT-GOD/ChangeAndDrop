using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public bool _isStarted = false;      // 스테이지 시작여부

    public bool _isPaused = true;           // 조작가능상태

    //현재 색상
    //false:파랑,true:주황
    public bool _isBlue = false;

    public int _currentStageNum = 0;        // 현재 스테이지번호 자동할당
    public Stage _currentStage = null;      // 현재 스테이지의 컴포넌트 자동할당
    private DragAndDrop _dnd = null;        // 카메라의 드래그 앤 드롭 할당

    public int _currnetBallNum = 0;         // 현재 공 개수
    public int _escapedBallNum = 0;         // 탈출한 공 개수

    public GameObject _camera = null;       // 지정필요 : 카메라 오브젝트
    public GameObject[] _stages = null;     // 지정필요 : 장애물 오브젝트들

    public float _lowest;
    private float _lastLowest = 0;

    void Start()
    {
        Init();
    }

    void Update()
    {
        // 마우스 입력시 색상토글
        if (!_isPaused && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isBlue = !_isBlue;
        }

        //최저점 갱신
        if (_lowest != _lastLowest)
        {
            _camera.transform.position
                = new Vector3(_camera.transform.position.x,
                _lowest + 2.6f,
                _camera.transform.position.z);
            _lastLowest = _lowest;
        }

        // 사망시 3초후 재시작
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

        //스테이지생성
        GameObject _tempStage;
        _tempStage = GameObject.Instantiate(_stages[_currentStageNum], this.transform.position, Quaternion.identity);

        //현재 스테이지 컴포넌트 할당
        _currentStage = _stages[_currentStageNum].gameObject.GetComponent<Stage>();

        //초기 최저점 = 천장높이
        _lowest = _currentStage._top.transform.position.y;

        //현재 스테이지의 공 박스 할당
        _dnd = _camera.gameObject.GetComponentInChildren<DragAndDrop>();
        _dnd._ballBox = _tempStage.gameObject.GetComponent<Stage>()._ballBoxs[0];
        _dnd.Init();

        _isStarted = false;
        _isPaused = true;
        _isBlue = false;
    }

    private void CleanStage()
    {
        // 기존 스테이지 제거
        GameObject _tempStage = GameObject.FindGameObjectWithTag("Stage");
        if(_tempStage != null)
            Destroy(_tempStage);

        // 기존 공 제거
        GameObject[] _tempballs = GameObject.FindGameObjectsWithTag("Ball");
        for(int i = 0; i<_tempballs.Length;i++)
        {
            Destroy(_tempballs[i]);
        }
    }

    private void Restart()
    {
        //필드 청소
        CleanStage();
        //초기화
        Init();
    }

    // 다음 스테이지 불러오기
    // GoalCheck 스크립트에서 사용
    public void CallNextStage()
    {
        //마지막 스테이지에선 미적용
        if (_currentStageNum + 1 >= _stages.Length)
            return;

        //필드 청소
        CleanStage();
        //스테이지 번호갱신
        _currentStageNum++;
        //초기화
        Init();
    }
    
}
