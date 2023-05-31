using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    public int _clearCount = 0;                             // �����ʿ� : Ŭ�����ϱ� ���� �ʿ��� �� ����

    public GameObject _box = null;                          // �����ʿ� : �� �ڽ�
    public GameObject _explosion = null;                    // �����ʿ� : ��ź

    private int _ballCount = 0;                             // ����� �� ����

    private Obstacle _obstacle = null;                      // �̹� �ִ� ������Ʈ �ڵ��Ҵ�
    private Animator _animator = null;                      // �̹� �ִ� ������Ʈ �ڵ��Ҵ�
    private StageManager _stageManager = null;              // �±׸� ������ �ڵ��Ҵ�

    private float _currentTime;

    private bool _alreadyCalled = false;                      // �̹� ���� ���������� ȣ���ߴ���

    void Start()
    {
        _obstacle = this.gameObject.GetComponentInParent<Obstacle>();
        _animator = _box.gameObject.GetComponentInParent<Animator>();
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
    }

    void Update()
    {
        //���� 50�� �̻� ��ǥ���� ���޽� �б� �ִϸ��̼� ���
        if (_ballCount > 50)
        {
            _animator.SetTrigger("Push");

            // ��� �� ����� 5���� ������������ ȣ��
            if (_stageManager._escapedBallNum >= _stageManager._currnetBallNum
                && !_alreadyCalled)
            {
                Invoke("Call", 5f);
                _alreadyCalled = true;
            }
        }
        //���� 100�� �̻� ��ǥ���� ���޽� ���� �ִϸ��̼� ���
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
        //���� ��� ��츸 ����
        if (other.gameObject.tag != "Ball")
            return;

        //�ߺ� �����ϱ� ���� ���Ƚ�� �߰�
        Ball _tempBall = other.gameObject.GetComponent<Ball>();
        if (_tempBall._currentLevel > _obstacle._level)
            return;
        _tempBall._currentLevel += 1;

        //�ϳ��� ���� ���� ���� ����
        _stageManager._isPaused = true;

        //����� �� ���� ����
        _ballCount++;
    }

    // ������������ ȣ��(����ȣ���� ���� ���� �Լ� ����)
    private void Call()
    {
        _stageManager.CallNextStage();
    }
}
