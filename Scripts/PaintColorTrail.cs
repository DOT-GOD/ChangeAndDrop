using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintColorTrail : MonoBehaviour
{
    public bool _isBlue = false;                    // 현재 색상 (false:파랑,true:주황)

    public Material _mtBlue = null;                 // 지정필요 : 머티리얼(파랑)
    public Material _mtOrange = null;               // 지정필요 : 머티리얼(주황)
    private TrailRenderer[] _renderers = null;      // 이미 있는 컴포넌트 자동할당

    StageManager _stageManager = null;             // 태그를 참조해 자동할당

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

        //색상적용
        _renderers = this.GetComponentsInChildren<TrailRenderer>();
        if (_isBlue) //파랑
            Set(_renderers, _mtBlue);
        else  //주황
            Set(_renderers, _mtOrange);
    }

    void Update()
    {
        //스테이지 컬러에 맞춰 색상 토글
        if (_isBlue != _stageManager._isBlue)
        {
            _isBlue = _stageManager._isBlue;

            if (_isBlue) //파랑
                Set(_renderers, _mtBlue);
            else  //주황
                Set(_renderers, _mtOrange);
        }
    }
    public void Set(TrailRenderer[] _renderers, Material _mt)
    {
        foreach (TrailRenderer render in _renderers)
        {
            render.material = _mt;
        }

    }
}
