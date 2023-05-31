using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculater : MonoBehaviour
{
    [SerializeField]
    [Range(1,20)]
    public int _num = 1;                           // 지정필요 : 공 갯수에 곱할 수

    public GameObject _pop;                        // 지정필요 : 효과음
    public GameObject _particle;                   // 지정필요 : 파티클
    public bool _isSecret = false;                 // 지정필요 : 표시 숫자가 ???인지 여부

    public bool _haveSpike = false;                // 공 파괴 여부

    StageManager _stageManager = null;             // 태그를 참조해 자동할당
    Obstacle _obstacle = null;                     // 이미 있는 컴포넌트 자동할당
    PaintColor _paint = null;                      // 이미 있는 컴포넌트 자동할당

    void Start()
    {

        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

        _obstacle = this.gameObject.GetComponent<Obstacle>();

        // 공 초기색깔 = 주황
        _paint = this.gameObject.GetComponent<PaintColor>();
        if (_paint._isBlue)
        {
            _particle.SetActive(true);
            _haveSpike = true;
        }
        else
        {
            _particle.SetActive(false);
            _haveSpike = false;
        }

        //텍스트 메쉬 변경
        var _tempTxt = this.gameObject.GetComponentInChildren<TextMesh>();
        if(_isSecret)
            _tempTxt.text = "???";
        else
            _tempTxt.text = "X" + _num;
    }

    void Update()
    {

        // 스테이지 색상과 다르면 가시토글
        if (!_haveSpike && _paint._isBlue != _stageManager._isBlue)
        {
            _particle.SetActive(true);
            _haveSpike = true;
        }
        else if((_haveSpike && _paint._isBlue == _stageManager._isBlue))
        {
            _particle.SetActive(false);
            _haveSpike = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //공이 닿는 경우만 판정
        if (other.gameObject.tag != "Ball")
            return;
        //Debug.Log("닿음");


        //새로 생긴 공이 복제되는 것을 방지하기 위해 계산횟수 추가
        Ball _tempBall = other.gameObject.GetComponent<Ball>();
        if (_tempBall._currentLevel > _obstacle._level)
            return;
        _tempBall._currentLevel += 1;


        if (_haveSpike)  //가시있음
        {
            //Debug.Log("공파괴");
            _stageManager._currnetBallNum--;
            Destroy(other.gameObject);
        }
        else // 가시없음
        {
            //Debug.Log("공통과");

            //???를 숫자로 변경
            if (_isSecret)
            {
                var _tempTxt = this.gameObject.GetComponentInChildren<TextMesh>();
                _tempTxt.text = "X" + _num;
            }

            //공 복제
            for (int i = 0; i < _num - 1; i++)
            {
                GameObject tempBall;
                tempBall = GameObject.Instantiate(other.gameObject, other.transform.position, Quaternion.identity);

                tempBall.transform.position =
                    new Vector3(other.transform.position.x,
                    other.transform.position.y + 0.5f,
                    other.transform.position.z);

                _stageManager._currnetBallNum++;


                // 효과음
                GameObject _tempPop;
                _tempPop = GameObject.Instantiate(_pop, this.transform.position, Quaternion.identity);
                Destroy(_tempPop, 0.5f);


                //Debug.Log("공복제");
            }
        }

    }
}
