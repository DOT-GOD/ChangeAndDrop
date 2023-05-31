using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculater : MonoBehaviour
{
    [SerializeField]
    [Range(1,20)]
    public int _num = 1;                           // �����ʿ� : �� ������ ���� ��

    public GameObject _pop;                        // �����ʿ� : ȿ����
    public GameObject _particle;                   // �����ʿ� : ��ƼŬ
    public bool _isSecret = false;                 // �����ʿ� : ǥ�� ���ڰ� ???���� ����

    public bool _haveSpike = false;                // �� �ı� ����

    StageManager _stageManager = null;             // �±׸� ������ �ڵ��Ҵ�
    Obstacle _obstacle = null;                     // �̹� �ִ� ������Ʈ �ڵ��Ҵ�
    PaintColor _paint = null;                      // �̹� �ִ� ������Ʈ �ڵ��Ҵ�

    void Start()
    {

        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

        _obstacle = this.gameObject.GetComponent<Obstacle>();

        // �� �ʱ���� = ��Ȳ
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

        //�ؽ�Ʈ �޽� ����
        var _tempTxt = this.gameObject.GetComponentInChildren<TextMesh>();
        if(_isSecret)
            _tempTxt.text = "???";
        else
            _tempTxt.text = "X" + _num;
    }

    void Update()
    {

        // �������� ����� �ٸ��� �������
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

        //���� ��� ��츸 ����
        if (other.gameObject.tag != "Ball")
            return;
        //Debug.Log("����");


        //���� ���� ���� �����Ǵ� ���� �����ϱ� ���� ���Ƚ�� �߰�
        Ball _tempBall = other.gameObject.GetComponent<Ball>();
        if (_tempBall._currentLevel > _obstacle._level)
            return;
        _tempBall._currentLevel += 1;


        if (_haveSpike)  //��������
        {
            //Debug.Log("���ı�");
            _stageManager._currnetBallNum--;
            Destroy(other.gameObject);
        }
        else // ���þ���
        {
            //Debug.Log("�����");

            //???�� ���ڷ� ����
            if (_isSecret)
            {
                var _tempTxt = this.gameObject.GetComponentInChildren<TextMesh>();
                _tempTxt.text = "X" + _num;
            }

            //�� ����
            for (int i = 0; i < _num - 1; i++)
            {
                GameObject tempBall;
                tempBall = GameObject.Instantiate(other.gameObject, other.transform.position, Quaternion.identity);

                tempBall.transform.position =
                    new Vector3(other.transform.position.x,
                    other.transform.position.y + 0.5f,
                    other.transform.position.z);

                _stageManager._currnetBallNum++;


                // ȿ����
                GameObject _tempPop;
                _tempPop = GameObject.Instantiate(_pop, this.transform.position, Quaternion.identity);
                Destroy(_tempPop, 0.5f);


                //Debug.Log("������");
            }
        }

    }
}
