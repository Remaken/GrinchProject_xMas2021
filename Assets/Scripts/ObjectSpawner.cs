using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour

{
    public GameObject[] spawnables;
    private float _randomspawn=0f;
    public bool canSpawn=true;
    public float spawnTime;
    public float spawnDelay;
    public float delay = 0f;
    public float timer = 0f;
    // Start is called before the first frame update


    public void Start()
    {
        resetDelay();
    }

    public void Update()
    {
//        Debug.Log("Je suis à " + GUI.tempsDepart + "d'un maximum de " +GUI.tempsLimite);
        if (GUI.tempsDepart < GUI.tempsLimite)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= delay)
            {
                SpawnObject();
                resetDelay();
            }
        }
    }
    public void SpawnObject()
    {
        if (canSpawn==true)
        {
            _randomspawn = Random.Range(0f, 3f);
            canSpawn = false;
            if (_randomspawn <=1)
            {
                Instantiate(spawnables[0],this.gameObject.transform);
            }
            else if (_randomspawn <=2)
            {
                Instantiate(spawnables[1],this.gameObject.transform);
            }
            else
            {
                Instantiate(spawnables[2],this.gameObject.transform);
            }
            
        }
    }

    public void resetDelay()
    {
        delay = Random.Range(0f, 4f);
        timer = 0;
    }
}
