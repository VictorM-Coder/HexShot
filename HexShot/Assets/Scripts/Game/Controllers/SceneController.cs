using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetNumberOfObjectsInScene();
    }

    void GetNumberOfObjectsInScene()
    {
        EnemyMovement[] allObjects = UnityEngine.Object.FindObjectsOfType<EnemyMovement>();
        int numberOfObjects = allObjects.Length;

        Debug.Log("Quantidade de objetos na cena: " + numberOfObjects);
    }
}
