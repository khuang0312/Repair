using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWhenInvisible: MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
       
    }
}
