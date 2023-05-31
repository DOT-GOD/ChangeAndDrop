using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool _trigger = false;

    public GameObject _bomb;                // 지정필요 : 디폴트는 자신

    public GameObject _confetti;            // 지정필요 : 색종이가루

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
        // 위력이나 지름이 0이면 리턴
        if (_force <= 0.0f || _radius <= 0.0f)
            return;

        // 모든 컬라이더 탐색
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
