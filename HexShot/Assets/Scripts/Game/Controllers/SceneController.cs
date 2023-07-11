using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{

    public TextMeshProUGUI _quantityEnemiesText;

    public UnityEvent OnFinish;

    private int _quantityEnemies;

    // Update is called once per frame
    void Update()
    {
        GetNumberOfObjectsInScene();

        if(_quantityEnemies == 0)
        {
            OnFinish.Invoke();
        }
    }

    void GetNumberOfObjectsInScene()
    {
        EnemyMovement[] allObjects = UnityEngine.Object.FindObjectsOfType<EnemyMovement>();
        int numberOfObjects = allObjects.Length;

        _quantityEnemies = numberOfObjects;
        _quantityEnemiesText.text = _quantityEnemies.ToString();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
