using UnityEngine;

public class HealthCollectableController : MonoBehaviour
{
    [SerializeField]
    private float _lifeAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.AddHealth(_lifeAmount);
            Destroy(gameObject);
        }
    }
}
