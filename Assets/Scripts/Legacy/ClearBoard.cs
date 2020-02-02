using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBoard : MonoBehaviour
{
    // Purges anything that is a block from the game...

    void clearBoard()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Blocks");

        foreach (GameObject b in blocks)
        {
            Destroy(b);
        }

    }
}
