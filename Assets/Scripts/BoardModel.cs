using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour
{
    //some variables we need throughout
    Camera mainCamera;

    //this sets up the board
    static int rows = 21;
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
    public int numBlocks;

    //this will be from lowestRow to highestRow (inclusive)...
    public int lowestRow;
    public int highestRow;

    //this is where we stick the block we want to randomly generate...
    public GameObject preGenerationBlock;
    public List<GameObject> blocksArray = new List<GameObject>();


    void Start()
    {
        //initializing the cells
        InitializeBoard();

        //placing the pre-generated blocks randomly within a certain range of rows...
        InitialGeneration(numBlocks);

        // Spawn the first block
        spawnBlock();
    }

    void Update()
    {
        //evaluate win condition
        // evaluateWinCondition();
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
    void InitialGeneration(int numBlocks)
    {
        //there will only be maxNumberOfBlocks generated
        //they will generate within the whole width and select rows of the board...
        for (int i = 0; i < numBlocks; ++i)
        {
            //pick a cell in the board
            Vector2 cell = pickCell();

            placeBlock(cell);
        }
    }

    void spawnBlock()
    {
        // Pick a random block from the array
        // int index = Random.Range(0, 1);

        // Quaternion.identity means no rotation
        GameObject.Instantiate(blocksArray[0], new Vector2(0, 0), Quaternion.identity);
    }

    void placeBlock(Vector2 cell)
    {
        //instantiate
        Instantiate(preGenerationBlock, cell, Quaternion.identity);

        //flag cell to show that it's been occupied.
        board[(int)cell.y, (int)cell.x] = BlockTypes.preGenBlock;
    }

    Vector2 pickCell()
    {
        int y = Random.Range(lowestRow, highestRow);
        int x = Random.Range(0, columns);

        while (!(withinBounds(y, x) && board[y, x] == BlockTypes.empty))
        {
            y = Random.Range(lowestRow, highestRow);
            x = Random.Range(0, columns);
        }

        return new Vector2(x, y);
    }

    // we do it y first, then x because our arrays are [row][column]
    bool withinBounds(int y, int x)
    {
        bool xIsValid = (0 <= x && x < columns);
        bool yIsValid = (0 <= y && y < rows);
        return (xIsValid && yIsValid);
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


