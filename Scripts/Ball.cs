using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int _currentLevel = 0;   // n-1��° ���

    public bool _isEscaped = false;        // �� Ż�⿩��

    private float _maxV = 8;


    private StageManager _stageManager = null;             // �±׸� ������ �ڵ��Ҵ�
    private Rigidbody _rigid = null;                       // �̹� �ִ� ������Ʈ �ڵ��Ҵ�

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        _rigid = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //�������� ����
        //���� ��ġ�� ���������� ���� �ٴں��� ���ƾ� ��
        if(_stageManager._lowest > this.gameObject.transform.position.y &&
            this.gameObject.transform.position.y > _stageManager._currentStage._bottom.transform.position.y)
        {
            _stageManager._lowest = this.gameObject.transform.position.y;
        }

        LimitVelocity();

        // ���� �ٴں��� �������� Ż������
        if(this.gameObject.transform.position.y < _stageManager._currentStage._bottom.gameObject.transform.position.y
            && !_isEscaped)
        {
            _stageManager._escapedBallNum++;
            _isEscaped = true;
        }

    }

    
    // ������ٵ� �ӵ�����
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
