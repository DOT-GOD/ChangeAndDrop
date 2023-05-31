using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableCheck : MonoBehaviour
{
    public bool _isSecret = false;                 // �����ʿ� : ǥ�� ���ڰ� ???���� ����

    [SerializeField]
    [Range(0,100)]
    public int _breakCount = 0;          // �����ʿ� : �ı��ϱ� ���� �ʿ��� �� ����

    private int _ballCount = 0;          // ����� �� ����

    private Obstacle _obstacle = null;      // �̹� �ִ� ������Ʈ �ڵ��Ҵ�
    private Animator _animator = null;      // �̹� �ִ� ������Ʈ �ڵ��Ҵ�

    private float _currentTime;

    void Start()
    {
        _obstacle = this.gameObject.GetComponentInParent<Obstacle>();
        _animator = this.gameObject.GetComponentInParent<Animator>();

        //�ؽ�Ʈ �޽� ����
        var _tempTxt = this.gameObject.transform.parent.GetComponentInChildren<TextMesh>();
        if (_isSecret)
            _tempTxt.text = "???";
        else
            _tempTxt.text = ""+_breakCount;
    }

    void Update()
    {
        //�ı� ī��Ʈ�� ���޽� �ı� �ִϸ��̼� ���
        if (_ballCount > _breakCount)   
        {
            _animator.SetTrigger("break");

            //_currentTime += Time.deltaTime;
        }

        //�ı� ī��Ʈ�� ���޽� n���� �ı�(�ִϸ��̼����� ��ü)
        //if(_currentTime > 3)
        //{
        //    Destroy(this.transform.parent.gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //���� ��� ��츸 ����
        if (other.gameObject.tag != "Ball")
            return;

        //���� ���� ���� �����Ǵ� ���� �����ϱ� ���� ���Ƚ�� �߰�
        Ball _tempBall = other.gameObject.GetComponent<Ball>();
        if (_tempBall._currentLevel > _obstacle._level)
            return;
        _tempBall._currentLevel += 1;

        //����� �� ���� ����
        _ballCount++;


    }
}

