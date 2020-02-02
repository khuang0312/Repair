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

    void Update()
    {
        //evaluate win condition
        evaluateWinCondition();
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

        while (true)
        {
            if (withinBounds(y, x))
            {
                if (board[y, x] == BlockTypes.empty)
                {
                    break;
                }
            }

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

    // we do it y first, then x because our arrays are [row][column]
    bool withinBounds(int y, int x)
    {
        bool xIsValid = (0 <= x && x < columns);
        bool yIsValid = (0 <= y && y < rows);
        return ( xIsValid && yIsValid );
    }

    void evaluateWinCondition()
    {
        int y = 0;
        int x = 0;

        for (int i = 0; i < rows; ++i)
        {
            //find the first row of a bridge...
            if (board[i, 0] != BlockTypes.empty)
            {
                y = i;
                break;
            }
        }

        bool loopCompletedSuccessfully = true;

        while (x != columns - 1)
        {
            //figure out if you can go down
            if (withinBounds(y + 1, x))
            {
                if (board[y + 1, x] != BlockTypes.empty)
                {
                    y += 1;
                    continue;
                }
            }

            //figure out if you can go right
            if (withinBounds(y, x + 1))
            {
                if (board[y, x + 1] != BlockTypes.empty)
                { 
                    x += 1;
                    continue;
                }
            }

            //figure out if you can go up
            if (withinBounds(y + 1, x))
            {
                if (board[y + 1, x] != BlockTypes.empty)
                {
                    x += 1;
                    continue;
                }
            }

            loopCompletedSuccessfully = false;
            break;
        }

        if (loopCompletedSuccessfully)
        {
            //this is for debugging purposes
            print("You Win!");
        }
    }
}
