using UnityEngine;

public class FragmentadoSplit : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabEnemyFragmentado;
    // Start is called before the first frame update

    public GameObject CreateClone(int level, Vector3 scale, int life, Vector3 position, Quaternion rotation)
    {
        GameObject newFragmentado = Instantiate(_prefabEnemyFragmentado, position, rotation);
        newFragmentado.GetComponent<EnemyFragmentadoAttack>()._level = level;
        newFragmentado.gameObject.transform.localScale = scale;
        newFragmentado.GetComponent<HealthController>().TakeDamage(life);
    
        return newFragmentado;
    }
}
