using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject _ballBox;             // StageManager���� �ڵ��Ҵ�
    private BallBox _ballBoxComponent;      // _ballBox�� �̹� �ִ� ������Ʈ �ڵ��Ҵ�

    private RaycastHit _rayHit;
    public float _initX = 0;               // �ʱ�x�� ����
    public float _initRayX = 0;            // ray�� �ʱ� x��
    private bool _isClicking = false;

    private Animator _animator = null;            // _ballBox�� �̹� �ִ� ������Ʈ �ڵ��Ҵ�
    private StageManager _stageManager = null;    // �±׸� ������ �ڵ��Ҵ�

    void Start()
    {
        Init();
    }

    void Update()
    {
        //��� ���� ������
        if (Input.GetKey(KeyCode.Mouse0)
            && !_ballBoxComponent._opened)
        {
            Drag();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Drop();
        }
    }
    
    public void Init()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        _animator = _ballBox.gameObject.GetComponentInParent<Animator>();
        _ballBoxComponent = _ballBox.GetComponent<BallBox>();
        _initX = _ballBox.gameObject.transform.position.x;
        _initRayX = 0;            // ray�� �ʱ� x��
        _isClicking = false;
    }

    //���콺�巡��(������ ���� OnMouseDrag������)
    private void Drag()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_ray.origin, _ray.direction * 1000, Color.green);

        if (Physics.Raycast(_ray, out _rayHit))
        {
            if (_rayHit.transform.gameObject.tag == "DragHelper")
            {
                //����1ȸ�� �� ����
                if (!_isClicking)
                    _initRayX = _rayHit.point.x;

                //ù Ŭ���� ���� �������� ���ڰ� �̵��ϴ� ���� �����ϱ� ���� �� ����
                float _tempX = (_initX + (_rayHit.point.x - _initRayX)) * 20;

                ////���� ���� �ʵ��� ���� ����
                if (_tempX > 1.9f)
                    _tempX = 1.9f;
                else if (_tempX < -1.9f)
                    _tempX = -1.9f;

                //�����̵�
                _ballBox.transform.position
                    = new Vector3(_tempX, _ballBox.transform.position.y, _ballBox.transform.position.z);
                

                //���� �ݴ� �ִϸ��̼� ���
                if (_isClicking)
                    _animator.SetTrigger("Close");

                _isClicking = true;
            }
        }
    }

    private void Drop()
    {
        if (_isClicking)
        {
            //���� ���� �ִϸ��̼� ���
            _animator.SetTrigger("Open");

            //_ballBoxComponent.DropBalls();

            _stageManager._isPaused = false;         // ���������� ����
            _isClicking = false;                     // �巡������
            _ballBoxComponent._opened = true;        // ���ڽ�����
        }
    }
}
