using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TokenGridData
{

    public const int GRID_SIZE = 8;

    public static TokenClass[,] Grid = new TokenClass[GRID_SIZE ,GRID_SIZE];

    //array index is the column, value is number of tokens destoyred in it.
    public static int[] destroyedTokens = new int[GRID_SIZE];

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

    public static void SwapToken(GridPos tp1, GridPos tp2)
    {
        TokenClass tk1v = TokenGridData.Grid[tp1.x, tp1.y];
        TokenClass tk2v = TokenGridData.Grid[tp2.x, tp2.y];

        tk1v.position = new GridPos(tp2.x, tp2.y);
        tk2v.position = new GridPos(tp1.x, tp1.y);

        TokenGridData.Grid[tp1.x, tp1.y] = tk2v;
        TokenGridData.Grid[tp2.x, tp2.y] = tk1v;

        TokenGridRenderer.AddToAnimationQueue(tk1v, tk1v.transform.position);
        TokenGridRenderer.AddToAnimationQueue(tk2v, tk2v.transform.position);

        TokenGridManager.animating = true;
    }

    public static bool FindMatch(TokenClass token)
    {
        bool matchFound = false;

        GridPos tp = token.position;

        //Check X axis for match

        //O - token
        //X - check tokens for match


        //XX0
        if(tp.x >= 2 && Grid[tp.x - 1, tp.y].type == token.type
                     && Grid[tp.x - 2, tp.y].type == token.type)
        {
            Debug.Log("Match X 1");
            return true;
        }

        //XOX
        if (tp.x >= 1 && tp.x < GRID_SIZE - 1 && Grid[tp.x - 1, tp.y].type == token.type
                                              && Grid[tp.x + 1, tp.y].type == token.type)
        {
            Debug.Log("Match X 2");
            return true;
        }

        //OXX
        if (tp.x < GRID_SIZE - 2 && Grid[tp.x + 1, tp.y].type == token.type
                                 && Grid[tp.x + 2, tp.y].type == token.type)
        {
            Debug.Log("Match X 3");
            return true;
        }


        //Check Y For match

        //O
        //X
        //X
        if (tp.y >= 2 && Grid[tp.x, tp.y - 1].type == token.type
                      && Grid[tp.x, tp.y - 2].type == token.type)
        {
            Debug.Log("Match Y 1");
            return true;
        }

        //X
        //O
        //X
        if (tp.y >= 1 && tp.y < GRID_SIZE - 1 && Grid[tp.x, tp.y - 1].type == token.type
                                              && Grid[tp.x, tp.y + 1].type == token.type)
        {
            Debug.Log("Match Y 2");
            return true;
        }

        //X
        //X
        //O
        if (tp.y < GRID_SIZE - 2 && Grid[tp.x, tp.y + 1].type == token.type
                                 && Grid[tp.x, tp.y + 2].type == token.type)
        {
            Debug.Log("Match Y 3");
            return true;
        }

        return false;
    }

    /// <summary>
    /// Flood fill from token to find all matching types
    /// </summary>
    /// <param name="token">token to search from</param>
    /// <returns>A List of matching connected tokens</returns>
    public static List<TokenClass> FindLinkedTypes(TokenClass token)
    {
        List<TokenClass> tokensToSearch = new List<TokenClass>() { token };
        List<TokenClass> tokensExplored = new List<TokenClass>();
        

        while (tokensToSearch.Count > 0)
        {
            List<TokenClass> newTokenstoSearch = new List<TokenClass>();

            foreach (TokenClass tc in tokensToSearch)
            {
                tokensExplored.Add(tc);

                GridPos tPos = tc.position;


                GridPos leftPos = new GridPos(tPos.x - 1, tPos.y);

                if (InBounds(leftPos) && !tokensExplored.Contains(GetToken(leftPos)) && GetTokenType(leftPos) == tc.type)
                    newTokenstoSearch.Add(GetToken(leftPos));


                GridPos rightPos = new GridPos(tPos.x + 1, tPos.y);

                if (InBounds(rightPos) && !tokensExplored.Contains(GetToken(rightPos)) && GetTokenType(rightPos) == tc.type)
                    newTokenstoSearch.Add(GetToken(rightPos));


                GridPos downPos = new GridPos(tPos.x, tPos.y - 1);

                if (InBounds(downPos) && !tokensExplored.Contains(GetToken(downPos)) && GetTokenType(downPos) == tc.type)
                    newTokenstoSearch.Add(GetToken(downPos));


                GridPos upPos = new GridPos(tPos.x, tPos.y + 1);

                if (InBounds(upPos) && !tokensExplored.Contains(GetToken(upPos)) && GetTokenType(upPos) == tc.type)
                    newTokenstoSearch.Add(GetToken(upPos));
            }

            tokensToSearch.Clear();
            tokensToSearch.AddRange(newTokenstoSearch);

            newTokenstoSearch.Clear();
        }

        return tokensExplored;
    }

    static TokenClass.TokenTypes GetTokenType(GridPos gp)
    {
        return Grid[gp.x, gp.y].type;
    }

    static TokenClass GetToken(GridPos gp)
    {
        return Grid[gp.x, gp.y];
    }

    static bool InBounds(GridPos position)
    {
        return InBounds(position.x, position.y);
    }

    static bool InBounds(int x, int y)
    {
        if (   x >= 0 && x < GRID_SIZE
            && y >= 0 && y < GRID_SIZE)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Checks if two tokens are adacent and within Grid bounds
    /// </summary>
    /// <param name="token1"></param>
    /// <param name="Token2"></param>
    /// <returns>False if out of bounds or not adjacent</returns>
    public static bool TokensAreAdjacent(GridPos token1, GridPos Token2)
    {
        if (token1 == null || Token2 == null)
        {
            Debug.LogError("One or Both of the tokens are null cannot swap (Token1 = " + token1 + ") (Token2 = " + Token2 + ")");
            return false;
        }

        //Is mouse position adajcent to the selected token to swap?
        if ((Token2.x == token1.x + 1 || Token2.x == token1.x - 1) && Token2.y == token1.y)
            return true;

        if ((Token2.y == token1.y + 1 || Token2.y == token1.y - 1) && Token2.x == token1.x)
            return true;

        //No Match found return false
        return false;

    }

}
