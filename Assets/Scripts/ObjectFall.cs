using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ObjectFall : MonoBehaviour
{
    public Player playerManager;
    // private float speed = 2f;
    public ObjectSpawner spawnManager;


    public void Start()
    {
        spawnManager = GetComponentInParent<ObjectSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D fall)
    {
        if (fall.gameObject.CompareTag("Terrain"))
        {
            if (this.gameObject.CompareTag("Gift"))
            {
                playerManager.life--;
            }
            Destroy(this.gameObject);
            spawnManager.canSpawn = true;
        }
    }

 
}




