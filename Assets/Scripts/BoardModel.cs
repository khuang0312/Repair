using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour
{
    
    //this sets up the board
    static int rows = 20;
    static int columns = 24;
    int[,] board = new int[rows, columns];

    //this might come in handy...
    enum BlockTypes
    {
        noBlock,
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
        
    }

    //this is for initializing every cell in the board to 0
    //0 represents that there is nothing currently occupying it
    void InitializeBoard()
    {
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                board[i, j] = 0;
            }
        }
    }

    //this places the intial blocks...
    void InitialGeneration(int maxNumberOfBlocks)
    {
        //there will only be maxNumberOfBlocks generated
        //they will generate within the whole width and select rows of the board...
        for (int i = 0; i < maxNumberOfBlocks; ++i)
        {
            int row = Random.Range(lowestRow, highestRow + 1);
            int column = Random.Range(0, columns);

            //if the index is occupied by some other number, leave it
            while (board[row, column] != 0)
            {
                row = Random.Range(lowestRow, highestRow + 1);
                column = Random.Range(0, columns);
            }

           Instantiate(preGenerationBlock, new Vector3(row, column), Quaternion.identity);

           //mark the board to show that something has spawned here...
           board[row, column] = 1;
        }
    }
    

}
