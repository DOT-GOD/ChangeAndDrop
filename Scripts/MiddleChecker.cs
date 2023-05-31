using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleChecker : MonoBehaviour
{
    public GameObject _middleBox = null;           // 지정필요 : 중간박스
    StageManager _stageManager = null;             // 태그를 참조해 자동할당

    private DragAndDrop _dnd = null;               // 태그를 참조해 자동할당

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        _dnd = GameObject.FindGameObjectWithTag("DragHelper").GetComponent<DragAndDrop>();
    }

    void Update()
    {
        if(_stageManager._currnetBallNum <= 0
            && _middleBox.GetComponent<BallBox>()._ballNum > 0)
        {
            // 중간박스의 애니메이터 트리거 작동
            Animator _tempAni = _middleBox.GetComponent<BallBox>().GetComponent<Animator>();
            _tempAni.SetTrigger("Close");

            // 중간 공 박스 할당
            _dnd._ballBox = _middleBox;
            _dnd.Init();

            // 체커 비활성화
            this.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // 공일 경우에만 실행
        if (other.gameObject.tag != "Ball")
            return;

        Destroy(other.gameObject);
        _stageManager._currnetBallNum--;
        _middleBox.GetComponent<BallBox>()._ballNum++;

        _stageManager._isPaused = true;
        _stageManager._isStarted = false;
    }
}
