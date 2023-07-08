using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private float _damageAmount;

    private void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyWhenOffScreen();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyMovement>())
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(_damageAmount);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }


    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if(screenPosition.x < 0 || 
           screenPosition.x > _camera.pixelWidth ||
           screenPosition.y < 0 ||
           screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);
        }
    }

}
