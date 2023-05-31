using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBox : MonoBehaviour
{
    public GameObject _ballPrefab;      // 지정필요 : 공 프리팹
    public int _ballNum = 0;            // 지정필요 : 가진 공 수량
    public int _madeBallNum = 0;        // 지정필요 : 만들어진 공 수량
    public bool _opened = false;        // 이미 열린 상자인지 판정
    public bool _drop = false;          // 애니메이션에서 조정

    private float _countTime = 0;


    StageManager _stageManager = null;             // 태그를 참조해 자동할당

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();


    }

    void Update()
    {
        // 공이 겹쳐서 팅겨져 나가는 현상 방지를 위해 딜레이 부여
        if (_drop && _madeBallNum < _ballNum)
        {
            _countTime += Time.deltaTime;
        }

        if(_countTime > 0.3f)
        {
            DropBalls();
            _countTime = 0;
        }
    }

    // 공 3개씩 투하
    public void DropBalls()
    {
        for (int i = 0; i < 3; i++)
        {
            // 최대 수만큼 만들어진 경우 리턴
            if (_madeBallNum >= _ballNum)
                return;

            GameObject _tempBall;
            Vector3 _tempPosition = new Vector3(this.transform.position.x - 0.5f + (i*0.5f), this.transform.position.y, this.transform.position.z);
            _tempBall = GameObject.Instantiate(_ballPrefab.gameObject, _tempPosition, Quaternion.identity);
            _madeBallNum++;

            // 현재 공 수 증가
            _stageManager._currnetBallNum++;

            // 시작신호
            _stageManager._isStarted = true;
        }
    }
}
