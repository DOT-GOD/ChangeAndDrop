using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCheck : MonoBehaviour
{
    public bool _isSecret = false;                 // 지정필요 : 표시 숫자가 ???인지 여부

    [SerializeField]
    [Range(0,100)]
    public int _breakCount = 0;          // 지정필요 : 파괴하기 위해 필요한 공 개수

    private int _ballCount = 0;          // 통과한 공 개수

    private Obstacle _obstacle = null;      // 이미 있는 컴포넌트 자동할당
    private Animator _animator = null;      // 이미 있는 컴포넌트 자동할당

    private float _currentTime;

    void Start()
    {
        _obstacle = this.gameObject.GetComponentInParent<Obstacle>();
        _animator = this.gameObject.GetComponentInParent<Animator>();

        //텍스트 메쉬 변경
        var _tempTxt = this.gameObject.transform.parent.GetComponentInChildren<TextMesh>();
        if (_isSecret)
            _tempTxt.text = "???";
        else
            _tempTxt.text = ""+_breakCount;
    }

    void Update()
    {
        //파괴 카운트에 도달시 파괴 애니메이션 재생
        if (_ballCount > _breakCount)   
        {
            _animator.SetTrigger("break");

            //_currentTime += Time.deltaTime;
        }

        //파괴 카운트에 도달시 n초후 파괴(애니메이션으로 대체)
        //if(_currentTime > 3)
        //{
        //    Destroy(this.transform.parent.gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //공이 닿는 경우만 판정
        if (other.gameObject.tag != "Ball")
            return;

        //새로 생긴 공이 복제되는 것을 방지하기 위해 계산횟수 추가
        Ball _tempBall = other.gameObject.GetComponent<Ball>();
        if (_tempBall._currentLevel > _obstacle._level)
            return;
        _tempBall._currentLevel += 1;

        //통과한 공 개수 증가
        _ballCount++;


    }
}

