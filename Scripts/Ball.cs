using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int _currentLevel = 0;   // n-1번째 계산

    public bool _isEscaped = false;        // 공 탈출여부

    private float _maxV = 8;


    private StageManager _stageManager = null;             // 태그를 참조해 자동할당
    private Rigidbody _rigid = null;                       // 이미 있는 컴포넌트 자동할당

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        _rigid = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //최저지점 갱신
        //공의 위치가 최저점보다 낮고 바닥보다 높아야 함
        if(_stageManager._lowest > this.gameObject.transform.position.y &&
            this.gameObject.transform.position.y > _stageManager._currentStage._bottom.transform.position.y)
        {
            _stageManager._lowest = this.gameObject.transform.position.y;
        }

        LimitVelocity();

        // 공이 바닥보다 낮아지면 탈출판정
        if(this.gameObject.transform.position.y < _stageManager._currentStage._bottom.gameObject.transform.position.y
            && !_isEscaped)
        {
            _stageManager._escapedBallNum++;
            _isEscaped = true;
        }

    }

    
    // 리지드바디 속도제한
    private void LimitVelocity()
    {
        if (Mathf.Abs(_rigid.velocity.x) > _maxV)
        {
            if (_rigid.velocity.x > 0)
                _rigid.velocity = new Vector3(_maxV, _rigid.velocity.y, _rigid.velocity.z);
            else
                _rigid.velocity = new Vector3(-_maxV, _rigid.velocity.y, _rigid.velocity.z);
        }
        if (Mathf.Abs(_rigid.velocity.y) > _maxV)
        {
            if (_rigid.velocity.y > 0)
                _rigid.velocity = new Vector3(_rigid.velocity.x, _maxV, _rigid.velocity.z);
            else
                _rigid.velocity = new Vector3(_rigid.velocity.x, -_maxV, _rigid.velocity.z);
        }
        if (Mathf.Abs(_rigid.velocity.z) > _maxV)
        {
            if (_rigid.velocity.z > 0)
                _rigid.velocity = new Vector3(_rigid.velocity.x, _rigid.velocity.y, _maxV);
            else
                _rigid.velocity = new Vector3(_rigid.velocity.x, _rigid.velocity.y, -_maxV);
        }
    }
}
