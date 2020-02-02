using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Spawns the next faller if the block has either disappered or landed


    // The vector generator is slightly different...
    // It generates only vectors at the bottom of the screen and below...

    // Positive values force the blocks down...
    public int yOffset = 0;
    public int blocksAvailable = 12;
    public List<GameObject> blocksArray = new List<GameObject>();
    Camera mainCamera;

    private void Start()
    {
        // Spawn the first block
        spawnBlock();
    }

    public void spawnBlock()
    {
        // Pick a random block from the array
        int index = Random.Range(0, blocksAvailable);

        // Quaternion.identity means no rotation
        GameObject.Instantiate(blocksArray[index], generateVector(), Quaternion.identity);
    }

    Vector3 generateVector()
    {
        mainCamera = Camera.main;

        // The left edge of the screen, and the bottom of the screen
        int leftCorner = Mathf.CeilToInt( mainCamera.ScreenToWorldPoint(Vector3.zero).x ) + 1;
        int screenBottom = Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(Vector3.zero).y);
        
        // The right edge of the screen
        int rightCorner = Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0)).x);

        // Create a vector that is below the frustum
        int x = Random.Range(leftCorner, rightCorner);
        return new Vector3(x, screenBottom - yOffset);
    }
}
