using UnityEngine;

public class EnemyFragmentadoAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _fragmentadoCloneManager;

    public int _level { get; set; } = 2;
    // Start is called before the first frame update

    public void CreateNewFragmentados()
    {
        var fragmentadoCloneManager = Instantiate(_fragmentadoCloneManager);
        if (_level > 0)
        {
            CreateFragmentado();
            CreateFragmentado();
        }

        Destroy(fragmentadoCloneManager);
    }

    private void CreateFragmentado()
    {
        var FragmentadoSplit = _fragmentadoCloneManager.GetComponent<FragmentadoSplit>();
        GameObject newFragmentado = FragmentadoSplit.CreateClone(
            _level - 1, 
            (gameObject.transform.localScale * 0.7f), 
            _level  + 1 * 9,
            transform.position, 
            Quaternion.identity
        );
    }
}
