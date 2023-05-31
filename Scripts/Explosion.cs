using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool _trigger = false;

    public GameObject _bomb;                // �����ʿ� : ����Ʈ�� �ڽ�

    public GameObject _confetti;            // �����ʿ� : �����̰���

    [SerializeField]
    [Range(0, 20f)]
    public float _bombRadius = 3f;

    [SerializeField]
    [Range(0, 10000f)]
    public float _bombForce = 2500f;

    void Start()
    {
    }

    void Update()
    {
        if (_trigger)
        {
            explosive(_bomb.transform.position, _bombRadius, _bombForce);
            _confetti.SetActive(true);
            _trigger = false;
        }

    }

    public void explosive(Vector3 _pos, float _radius, float _force)
    {
        // �����̳� ������ 0�̸� ����
        if (_force <= 0.0f || _radius <= 0.0f)
            return;

        // ��� �ö��̴� Ž��
        Collider[] objects = UnityEngine.Physics.OverlapSphere(_pos, _radius);

        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(_force, _pos, _radius);
            }
        }
    }


}
