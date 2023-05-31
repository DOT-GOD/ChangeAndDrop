using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintColorTrail : MonoBehaviour
{
    public bool _isBlue = false;                    // ���� ���� (false:�Ķ�,true:��Ȳ)

    public Material _mtBlue = null;                 // �����ʿ� : ��Ƽ����(�Ķ�)
    public Material _mtOrange = null;               // �����ʿ� : ��Ƽ����(��Ȳ)
    private TrailRenderer[] _renderers = null;      // �̹� �ִ� ������Ʈ �ڵ��Ҵ�

    StageManager _stageManager = null;             // �±׸� ������ �ڵ��Ҵ�

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

        //��������
        _renderers = this.GetComponentsInChildren<TrailRenderer>();
        if (_isBlue) //�Ķ�
            Set(_renderers, _mtBlue);
        else  //��Ȳ
            Set(_renderers, _mtOrange);
    }

    void Update()
    {
        //�������� �÷��� ���� ���� ���
        if (_isBlue != _stageManager._isBlue)
        {
            _isBlue = _stageManager._isBlue;

            if (_isBlue) //�Ķ�
                Set(_renderers, _mtBlue);
            else  //��Ȳ
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
