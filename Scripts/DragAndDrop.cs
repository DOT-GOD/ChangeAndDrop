using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject _ballBox;             // StageManager에서 자동할당
    private BallBox _ballBoxComponent;      // _ballBox에 이미 있는 컴포넌트 자동할당

    private RaycastHit _rayHit;
    public float _initX = 0;               // 초기x값 저장
    public float _initRayX = 0;            // ray의 초기 x값
    private bool _isClicking = false;

    private Animator _animator = null;            // _ballBox에 이미 있는 컴포넌트 자동할당
    private StageManager _stageManager = null;    // 태그를 참조해 자동할당

    void Start()
    {
        Init();
    }

    void Update()
    {
        //드롭 이후 미적용
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
        _initRayX = 0;            // ray의 초기 x값
        _isClicking = false;
    }

    //마우스드래그(투명벽에 막혀 OnMouseDrag사용안함)
    private void Drag()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_ray.origin, _ray.direction * 1000, Color.green);

        if (Physics.Raycast(_ray, out _rayHit))
        {
            if (_rayHit.transform.gameObject.tag == "DragHelper")
            {
                //최초1회만 값 저장
                if (!_isClicking)
                    _initRayX = _rayHit.point.x;

                //첫 클릭시 레이 지점으로 상자가 이동하는 것을 방지하기 위해 값 조정
                float _tempX = (_initX + (_rayHit.point.x - _initRayX)) * 20;

                ////벽을 넘지 않도록 범위 조정
                if (_tempX > 1.9f)
                    _tempX = 1.9f;
                else if (_tempX < -1.9f)
                    _tempX = -1.9f;

                //상자이동
                _ballBox.transform.position
                    = new Vector3(_tempX, _ballBox.transform.position.y, _ballBox.transform.position.z);
                

                //상자 닫는 애니메이션 출력
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
            //상자 여는 애니메이션 출력
            _animator.SetTrigger("Open");

            //_ballBoxComponent.DropBalls();

            _stageManager._isPaused = false;         // 색변경정지 해제
            _isClicking = false;                     // 드래그해제
            _ballBoxComponent._opened = true;        // 공박스열림
        }
    }
}
