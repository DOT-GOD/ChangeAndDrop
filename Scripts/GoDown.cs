using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDown : MonoBehaviour
{
    public bool _trigger = false;

    private float _initY = 0;

    void Start()
    {
        _initY = this.transform.position.y;    
    }

    void Update()
    {
        // 현재 위치에서 -2.5만큼 아래로 이동
        // 애니메이션으로 이동시키면 이후 고정되는 현상때문에 스크립트로 사용
        if(_trigger)
        {
            if(this.transform.position.y > _initY - 2.5f)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.05f,this.transform.position.z);
            }
        }
    }
}
