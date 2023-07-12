using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawn(){
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        instatiateEnemy();
        StartCoroutine(spawn());
    }

    private void instatiateEnemy() {
        Instantiate(enemies[Random.Range(0, (enemies.Length-1))], new Vector3(Random.Range(-30, 30), Random.Range(-15, 15)), transform.rotation);
    }
}
