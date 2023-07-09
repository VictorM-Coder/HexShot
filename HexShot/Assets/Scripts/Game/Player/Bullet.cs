using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private float _damageAmount;
    [SerializeField]
    private string _name;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool _isPiercing;
    [SerializeField]
    private int _cost;

    private void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyWhenOffScreen();
    }

    public float getSpeed() {
        return speed;
    }

    public int getCost() {
        return _cost;
    }

    public string getName() {
        return _name;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyMovement>() || collision.gameObject.GetComponent<HealthController>())
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(_damageAmount);
        } else if (collision.CompareTag("Shotgun") || collision.CompareTag("BulletStorm")) {
            return;
        }
        if(!_isPiercing) Destroy(gameObject);
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
