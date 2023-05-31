using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    public int _level = 0;                          //n-1번째 계산(스테이지 매니저에서 자동할당)

}
