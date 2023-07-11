using UnityEngine;

public class DropCollectableController : MonoBehaviour
{
    [SerializeField]
    private int _chancePercentage;
    [SerializeField]
    private GameObject _prefabCollectable;
    
    public void DropCollectable()
    {
        if(Random.Range(0, 100) < _chancePercentage)
        {
            Instantiate(_prefabCollectable, transform.position + transform.right * 0.5f, Quaternion.identity);
        }
    }
}
