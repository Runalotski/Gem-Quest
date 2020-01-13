using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TokenGridData
{

    public const int GRID_SIZE = 8;

    public static TokenClass[,] Grid = new TokenClass[GRID_SIZE ,GRID_SIZE];


    public static Dictionary<TokenClass.TokenTypes, Transform> Tokens = new Dictionary<TokenClass.TokenTypes, Transform>();

    public static void CreateNewGrid(TokenClass.TokenTypes[] possiblTokens)
    {
        //iterate over the whole grid and assign a random token from a select list.
        for(int y = 0; y < GRID_SIZE; y++)
        {
            for(int x = 0; x < GRID_SIZE; x++)
            {
                int rnd = Random.Range(0, possiblTokens.Length);

                TokenClass.TokenTypes SelectedToken = possiblTokens[rnd];

                Grid[x, y] = ReturnNonmatchingToken(possiblTokens, SelectedToken, x, y);

            }
        }

    }

    /// <summary>
    /// Avoid creating matches in initial grid
    /// </summary>
    /// <param name="possiblTokens"></param>
    /// <param name="selected"></param>
    /// <param name="x">x Position of Selected</param>
    /// <param name="y">y Position of Selected</param>
    static TokenClass ReturnNonmatchingToken(TokenClass.TokenTypes[] possiblTokens, TokenClass.TokenTypes selected, int x, int y)
    {
        //we want to avoid matches when the drid is first created.
        //This function will check in both direction for 2 squares for mataching

        //Initial grid scans left to right bottom to top, we only need to look back

        bool matchFound = false;

        //-x
        if(!matchFound && x >= 2)
        {
            if(Grid[x-2, y].type == selected && Grid[x-1, y].type == selected)
            {
                matchFound = true;
            }
        }

        //-y
        if (!matchFound && y >= 2)
        {
            if (Grid[x, y - 2].type == selected && Grid[x, y - 1].type == selected)
            {
                matchFound = true;
            }
        }

        if(matchFound)
        {
            //remove the current selection from the possiblities and select a new one

            TokenClass.TokenTypes[] newPossibleList = new TokenClass.TokenTypes[possiblTokens.Length - 1];
            int addedCount = 0;

            for (int i = 0; i < possiblTokens.Length; i++)
            {
               if(possiblTokens[i] != selected)
               {
                    newPossibleList[addedCount] = possiblTokens[i];
                    addedCount++;
               }
            }

            TokenClass.TokenTypes newSelected = newPossibleList[Random.Range(0, newPossibleList.Length)];

            return ReturnNonmatchingToken(newPossibleList, newSelected, x, y);
        }
        else
        {
            return new TokenClass(null, selected, new GridPos(x,y));
        }

    }

}
