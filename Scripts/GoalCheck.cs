using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    public int _clearCount = 0;                             // 지정필요 : 클리어하기 위해 필요한 공 개수

    public GameObject _box = null;                          // 지정필요 : 골 박스
    public GameObject _explosion = null;                    // 지정필요 : 폭탄

    private int _ballCount = 0;                             // 통과한 공 개수

    private Obstacle _obstacle = null;                      // 이미 있는 컴포넌트 자동할당
    private Animator _animator = null;                      // 이미 있는 컴포넌트 자동할당
    private StageManager _stageManager = null;              // 태그를 참조해 자동할당

    private float _currentTime;

    private bool _alreadyCalled = false;                      // 이미 다음 스테이지를 호출했는지

    void Start()
    {
        _obstacle = this.gameObject.GetComponentInParent<Obstacle>();
        _animator = _box.gameObject.GetComponentInParent<Animator>();
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        //공이 50개 이상 목표지에 도달시 밀기 애니메이션 재생
        if (_ballCount > 50)
        {
            _animator.SetTrigger("Push");

            // 모든 공 통과시 5초후 다음스테이지 호출
            if (_stageManager._escapedBallNum >= _stageManager._currnetBallNum
                && !_alreadyCalled)
            {
                Invoke("Call", 5f);
                _alreadyCalled = true;
            }
        }
        //공이 100개 이상 목표지에 도달시 낙하 애니메이션 재생
        if (_ballCount > 100)
        {
            _animator.SetTrigger("Down");

            if (_stageManager._escapedBallNum >= _stageManager._currnetBallNum
                && _stageManager._currnetBallNum != 0)
            {
                _explosion.GetComponent<Explosion>()._trigger = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //공이 닿는 경우만 판정
        if (other.gameObject.tag != "Ball")
            return;

        //중복 방지하기 위해 계산횟수 추가
        Ball _tempBall = other.gameObject.GetComponent<Ball>();
        if (_tempBall._currentLevel > _obstacle._level)
            return;
        _tempBall._currentLevel += 1;

        //하나라도 공이 들어가면 조작 중지
        _stageManager._isPaused = true;

        //통과한 공 개수 증가
        _ballCount++;
    }

    // 다음스테이지 호출(지연호출을 위해 따로 함수 생성)
    private void Call()
    {
        _stageManager.CallNextStage();
    }
}
