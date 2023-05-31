using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintColor : MonoBehaviour
{
    public bool _isBlue = false;                            // 현재 색상 (false:파랑,true:주황)

    public bool _toggle = false;                            // 클릭시 색상 토클(공은 true)

    private MeshRenderer[] _renderers = null;               // 이미 있는 컴포넌트 자동할당

    private StageManager _stageManager = null;              // 태그를 참조해 자동할당

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

        //색상적용
        _renderers = this.GetComponentsInChildren<MeshRenderer>();
        if (_isBlue) //파랑
            Set(_renderers, 0, 0, 1f);
        else  //주황
            Set(_renderers, 1f, 0.5f, 0);
    }

    void Update()
    {
        // 색상토글 필요시 적용
        if (!_toggle)
            return;

        //스테이지 컬러에 맞춰 색상 토글
        if(_isBlue != _stageManager._isBlue)
        {
            _isBlue = _stageManager._isBlue;

            if (_isBlue) //파랑
                Set(_renderers, 0, 0, 1f);
            else  //주황
                Set(_renderers, 1f, 0.5f, 0);
        }
    }
    public void Set(MeshRenderer[] _renderers, float _r,float _g, float _b)
    {
        foreach (MeshRenderer render in _renderers)
        {
            render.material.color = new Color(_r, _g, _b);
        }
        
    }
}
