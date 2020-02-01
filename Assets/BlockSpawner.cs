using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    
    public int blocksAvailable = 18;
    public List<GameObject> blocksArray = new List<GameObject>();

    void spawnBlock()
    {
        int index = Random.Range(0, blocksAvailable);

        //Quaternion.identity means no rotation
        GameObject.Instantiate(blocksArray[index], generateVertex(), Quaternion.identity);
    }

    Vector3 generateVertex()
    {
        //create a vector that is below the frustum
        int x = Random.Range(0, 5);
        return new Vector3(x, 0);
    }
}
