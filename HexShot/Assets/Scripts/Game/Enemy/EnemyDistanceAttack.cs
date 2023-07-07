using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAttack : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    private float distanceBetweenPlayer;
    [SerializeField]
    private Transform _gunOffset;
    [SerializeField]
    private GameObject _bulletPrefab;
    private float _lastFireTime;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenPlayer = Vector2.Distance(transform.position, player.transform.position);

        float timeSinceLastFire = Time.time - _lastFireTime;

            if(timeSinceLastFire >= 1f)
            {
                FireBullet();
                _lastFireTime = Time.time;
            }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = 10 * transform.up;
    }
}
