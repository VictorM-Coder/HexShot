using UnityEngine;

public class EnemyDied : MonoBehaviour
{
    [SerializeField]
    private GameObject _blood;

    public void Dead()
    {
        Instantiate(_blood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
