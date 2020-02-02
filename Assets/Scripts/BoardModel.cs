using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour
{
    //some variables we need throughout
    Camera mainCamera;
    
    //this sets up the board
    static int rows = 20;
    static int columns = 24;
    BlockTypes[,] board = new BlockTypes[rows, columns];

    //this might come in handy...
    enum BlockTypes
    {
        empty,
        preGenBlock,
        frozenBlock,
        faller,
    }

    //this affects the pre-generated blocks
    public int maxNumberOfBlocks;

    //this will be from lowestRow to highestRow (inclusive)...
    public int lowestRow;
    public int highestRow;

    //this is where we stick the block we want to randomly generate...
    public GameObject preGenerationBlock;
    

    void Start()
    {
        //initializing the cells
        InitializeBoard();

        //placing the pre-generated blocks randomly within a certain range of rows...
        InitialGeneration(maxNumberOfBlocks);
    }

    //this is for initializing every cell in the board to empty
    //empty represents that there is nothing currently occupying it
    void InitializeBoard()
    {
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                board[i, j] = BlockTypes.empty;
            }
        }
    }

    //this places the intial blocks...
    void InitialGeneration(int maxNumberOfBlocks)
    {
        int row = Random.Range(lowestRow, highestRow + 1);
        int column = Random.Range(0, columns);

        //there will only be maxNumberOfBlocks generated
        //they will generate within the whole width and select rows of the board...
        for (int i = 0; i < maxNumberOfBlocks; ++i)
        {
            //pick a cell in the board
            Vector2 cell = pickCell();

            //convert that cell to a world coordinate
            Vector3 approxWorldPos = convertCellToWorld(cell);

            //instantiate
            Instantiate(preGenerationBlock, approxWorldPos, Quaternion.identity);

            //flag cell to show that it's been occupied.
            board[row, column] = BlockTypes.preGenBlock;
        }
    }

    Vector2 pickCell()
    {
        int y = Random.Range(lowestRow, highestRow + 1);
        int x = Random.Range(0, columns);
        
        while (board[y, x] != BlockTypes.empty)
        {
            y = Random.Range(lowestRow, highestRow + 1);
            x = Random.Range(0, columns);
        }

        return new Vector2(x, y);
    }   


    Vector3 convertCellToWorld(Vector2 cell)
    {
        mainCamera = Camera.main;
        
        float cameraX = (cell.x / columns) * mainCamera.scaledPixelWidth;
        float cameraY = (cell.y / rows) * mainCamera.scaledPixelHeight;

        int worldX = Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(new Vector3(cameraX, 0)).x);
        int worldY = Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(new Vector3(0, cameraY)).y);

        return new Vector3(worldX, worldY);
    }


}
