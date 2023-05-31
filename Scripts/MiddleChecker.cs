using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleChecker : MonoBehaviour
{
    public GameObject _middleBox = null;           // �����ʿ� : �߰��ڽ�
    StageManager _stageManager = null;             // �±׸� ������ �ڵ��Ҵ�

    private DragAndDrop _dnd = null;               // �±׸� ������ �ڵ��Ҵ�

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
            // �߰��ڽ��� �ִϸ����� Ʈ���� �۵�
            Animator _tempAni = _middleBox.GetComponent<BallBox>().GetComponent<Animator>();
            _tempAni.SetTrigger("Close");

            // �߰� �� �ڽ� �Ҵ�
            _dnd._ballBox = _middleBox;
            _dnd.Init();

            // üĿ ��Ȱ��ȭ
            this.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // ���� ��쿡�� ����
        if (other.gameObject.tag != "Ball")
            return;

        Destroy(other.gameObject);
        _stageManager._currnetBallNum--;
        _middleBox.GetComponent<BallBox>()._ballNum++;

        _stageManager._isPaused = true;
        _stageManager._isStarted = false;
    }
}
