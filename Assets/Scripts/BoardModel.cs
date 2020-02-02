using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour
{
    // Initialise camera for later
    Camera mainCamera;

    // Sets up the board
    static int rows = 21;
    static int columns = 24;
    BlockTypes[,] board = new BlockTypes[rows, columns];

    // Enumerator for each of the states of a block
    enum BlockTypes
    {
        empty,
        preGenBlock,
        frozenBlock,
        faller,
    }

    // Affects the pre-generated blocks
    public int numBlocks;

    // Number lowest and highest row (inclusive)
    public int lowestRow;
    public int highestRow;

    // Object to hold pre-generared blocks
    public GameObject preGenerationBlock;


    void Start()
    {
        // Initializing cells
        InitializeBoard();

        // Placing the pre-generated blocks within the predefined rows
        InitialGeneration(numBlocks);
    }

    void Update()
    {
        // Evaluate win condition
        // evaluateWinCondition();
    }

    // Initialises every cell in the board to "empty"
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

    // Places starting blocks
    void InitialGeneration(int numBlocks)
    {
        // there will only be maxNumberOfBlocks generated
        // they will generate within the whole width and select rows of the board...
        for (int i = 0; i < numBlocks; ++i)
        {
            // Pick a defined cell in the board
            Vector2 cell = pickCell();

            PlaceBlock(cell);
        }
    }

    void PlaceBlock(Vector2 cell)
    {
        Instantiate(preGenerationBlock, cell, Quaternion.identity);

        // Flag cell to show that it is occupied
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
            // Find the first row of a bridge
            if (board[i, 0] != BlockTypes.empty)
            {
                y = i;
                break;
            }
        }

        bool loopCompletedSuccessfully = true;

        while (x != columns - 1)
        {
            // Check the vector (+1, -1)
            if (withinBounds(y - 1, x + 1))
            {
                if (board[y - 1, x + 1] != BlockTypes.empty)
                {
                    x += 1;
                    y -= 1;
                    continue;
                }
            }

            // Check the vector (+1, 0)
            if (withinBounds(y, x + 1))
            {
                if (board[y, x + 1] != BlockTypes.empty)
                {
                    x += 1;
                    continue;
                }
            }

            // Check the vector (+1, +1)
            if (withinBounds(y + 1, x))
            {
                if (board[y + 1, x + 1] != BlockTypes.empty)
                {
                    x += 1;
                    y += 1;
                    continue;
                }
            }

            loopCompletedSuccessfully = false;
            break;
        }

        if (loopCompletedSuccessfully)
        {
            // You Win
            print("You Win!");
        }
    }
}
