using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWhenInvisible: MonoBehaviour
{
    public GameObject spawner;

    void OnBecameInvisible()
    {
        Destroy(gameObject);

        //spawn a new block
        spawner = GameObject.Find("BlockSpawner");
        spawner.GetComponent<BlockSpawner>().spawnBlock();
    }
}
