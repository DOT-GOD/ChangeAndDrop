using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject _top = null;              // �����ʿ� : õ�� ������Ʈ(ù�ڽ�)
    public GameObject _bottom = null;           // �����ʿ� : �ٴ� ������Ʈ
    public GameObject[] _ballBoxs = null;       // �����ʿ� : �� �ڽ���
    //public GameObject[] _obstacles = null;    // �����ʿ� : ��ֹ� ������Ʈ��

    void Start()
    {
        //LevelSet();
    }

    void Update()
    {

    }

    // ��ֹ� ���� ����(���� ���� ������ ���� �ִ� ��찡 �־ ���)
    //private void LevelSet()
    //{
    //    for (int i = 0; i < _obstacles.Length; i++)
    //    {
    //        _obstacles[i].GetComponent<Obstacle>()._level = i;
    //    }
    //}
}
