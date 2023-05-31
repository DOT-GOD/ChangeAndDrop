using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject _top = null;              // 지정필요 : 천장 오브젝트(첫박스)
    public GameObject _bottom = null;           // 지정필요 : 바닥 오브젝트
    public GameObject[] _ballBoxs = null;       // 지정필요 : 공 박스들
    //public GameObject[] _obstacles = null;    // 지정필요 : 장애물 오브젝트들

    void Start()
    {
        //LevelSet();
    }

    void Update()
    {

    }

    // 장애물 순서 지정(같은 층에 두종류 벽이 있는 경우가 있어서 폐기)
    //private void LevelSet()
    //{
    //    for (int i = 0; i < _obstacles.Length; i++)
    //    {
    //        _obstacles[i].GetComponent<Obstacle>()._level = i;
    //    }
    //}
}
