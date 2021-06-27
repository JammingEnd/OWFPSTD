using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnAbles = new List<GameObject>();
    private float maxRangeX;
    private float maxRangeZ;
    public float centerRangeX;
    public float centerRangeZ;
    public float spawnRateDecay;
    public int maxSpawned;
    public int currentSpawned;
    public float spawnrate;
    public float currentspawnrate;

   
    /// <summary>
    /// gets the position of the current object
    /// </summary>
    private void Start()
    {
        maxRangeX = this.gameObject.transform.position.x + centerRangeX;
        maxRangeZ = this.gameObject.transform.position.z + centerRangeZ;
        currentspawnrate = spawnrate;
 
       // spawnPos = new Vector3(Random.Range(-maxRangeX, maxRangeX), 1, Random.Range(-maxRangeZ, maxRangeZ));
       
    }
    
    /// <summary>
    /// every spawnrate (in seconds) spawns an enemy at a random location
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public IEnumerator SpawnInterval(GameObject enemy, Vector3 pos)
    { 
        while(currentSpawned < maxSpawned)
        {

           
           
            Instantiate(enemy, pos, transform.rotation);
            currentSpawned++;
            NewData();
            yield return new WaitForSeconds(currentspawnrate);
            yield return enemy;
            yield return pos = new Vector3(Random.Range(-maxRangeX, maxRangeX), 1, Random.Range(-maxRangeZ, maxRangeZ));
            
        }
        
    }

    /// <summary>
    /// gets new position and gameobject from a list
    /// </summary>
    void NewData()
    {
        Vector3 newPos = new Vector3(Random.Range(-maxRangeX, maxRangeX), 1, Random.Range(-maxRangeZ, maxRangeZ));
      
        SpawnInterval(spawnAbles[Random.Range(0, (spawnAbles.Count))], newPos); 
    }

    /// <summary>
    /// starts the coroutine
    /// </summary>
    public void StartSpawning()
    {
        StartCoroutine(SpawnInterval(spawnAbles[Random.Range(0, spawnAbles.Count)], new Vector3(Random.Range(-maxRangeX, maxRangeX), 1, Random.Range(-maxRangeZ, maxRangeZ))));

    }
}
