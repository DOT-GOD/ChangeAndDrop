using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintColor : MonoBehaviour
{
    public bool _isBlue = false;                            // ���� ���� (false:�Ķ�,true:��Ȳ)

    public bool _toggle = false;                            // Ŭ���� ���� ��Ŭ(���� true)

    private MeshRenderer[] _renderers = null;               // �̹� �ִ� ������Ʈ �ڵ��Ҵ�

    private StageManager _stageManager = null;              // �±׸� ������ �ڵ��Ҵ�

    void Start()
    {
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();

        //��������
        _renderers = this.GetComponentsInChildren<MeshRenderer>();
        if (_isBlue) //�Ķ�
            Set(_renderers, 0, 0, 1f);
        else  //��Ȳ
            Set(_renderers, 1f, 0.5f, 0);
    }

    void Update()
    {
        // ������� �ʿ�� ����
        if (!_toggle)
            return;

        //�������� �÷��� ���� ���� ���
        if(_isBlue != _stageManager._isBlue)
        {
            _isBlue = _stageManager._isBlue;

            if (_isBlue) //�Ķ�
                Set(_renderers, 0, 0, 1f);
            else  //��Ȳ
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
