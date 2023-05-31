using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintRGB : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    public float _r = 0;
    [SerializeField]
    [Range(0, 1)]
    public float _g = 0;
    [SerializeField]
    [Range(0, 1)]
    public float _b = 0;

    private MeshRenderer[] _renderers = null;                // �̹� �ִ� ������Ʈ �ڵ��Ҵ�

    void Start()
    {
        //��������
        _renderers = this.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer render in _renderers)
        {
            render.material.color = new Color(_r, _g, _b);
        }
    }

}
