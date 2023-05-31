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
        // ���� ��ġ���� -2.5��ŭ �Ʒ��� �̵�
        // �ִϸ��̼����� �̵���Ű�� ���� �����Ǵ� ���󶧹��� ��ũ��Ʈ�� ���
        if(_trigger)
        {
            if(this.transform.position.y > _initY - 2.5f)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.05f,this.transform.position.z);
            }
        }
    }
}
