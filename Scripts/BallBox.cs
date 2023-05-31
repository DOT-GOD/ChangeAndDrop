using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBox : MonoBehaviour
{
    public GameObject _ballPrefab;      // �����ʿ� : �� ������
    public int _ballNum = 0;            // �����ʿ� : ���� �� ����
    public int _madeBallNum = 0;        // �����ʿ� : ������� �� ����
    public bool _opened = false;        // �̹� ���� �������� ����
    public bool _drop = false;          // �ִϸ��̼ǿ��� ����

    private float _countTime = 0;


    StageManager _stageManager = null;             // �±׸� ������ �ڵ��Ҵ�

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();


    }

    void Update()
    {
        // ���� ���ļ� �ð��� ������ ���� ������ ���� ������ �ο�
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

    // �� 3���� ����
    public void DropBalls()
    {
        for (int i = 0; i < 3; i++)
        {
            // �ִ� ����ŭ ������� ��� ����
            if (_madeBallNum >= _ballNum)
                return;

            GameObject _tempBall;
            Vector3 _tempPosition = new Vector3(this.transform.position.x - 0.5f + (i*0.5f), this.transform.position.y, this.transform.position.z);
            _tempBall = GameObject.Instantiate(_ballPrefab.gameObject, _tempPosition, Quaternion.identity);
            _madeBallNum++;

            // ���� �� �� ����
            _stageManager._currnetBallNum++;

            // ���۽�ȣ
            _stageManager._isStarted = true;
        }
    }
}
