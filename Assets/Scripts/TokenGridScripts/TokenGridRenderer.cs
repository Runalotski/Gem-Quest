using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenGridRenderer : MonoBehaviour
{
    static List<TokenClass> tokensToAnimate = new List<TokenClass>();
    static List<Vector3> tokenStartPositions = new List<Vector3>();

    public Transform TokenParent;
    public static Transform TOKEN_PARENT;

    static float animateTime = 0;

    public void Awake()
    {
        TOKEN_PARENT = TokenParent;
    }

    public static void AnimateGrid()
    {

        animateTime += (Time.deltaTime * 4);

        for (int i = 0; i < tokensToAnimate.Count; i++)
        {
            Transform TokenTransform = tokensToAnimate[i].transform;
            Vector3 StartPos = tokenStartPositions[i];
            Vector3 TokenDest = new Vector3(tokensToAnimate[i].position.x, tokensToAnimate[i].position.y, 0);

            tokensToAnimate[i].transform.position = Vector3.Lerp(tokenStartPositions[i], TokenDest, animateTime);
        }

        if (animateTime >= 1f)
        {
            Debug.Log("======================  Animation Complete  ====================== ");
            foreach (TokenClass token in tokensToAnimate)
            {
                if (TokenGridData.FindMatch(token))
                {
                    List<TokenClass> tkl = TokenGridData.FindLinkedTypes(token);

                    foreach (TokenClass tc in tkl)
                        Debug.Log(tc + " !null? " + (tc.transform != null).ToString() );

                    Debug.Log("======================  Destroy  ======================");

                    foreach (TokenClass tc in tkl)
                    {
                        if (tc.transform != null)
                        {
                            Destroy(tc.transform.gameObject);

                            QuitButton.score += ((int)tc.type) * 1000000 + Random.Range(1,999999);

                            tc.transform = null;
                            TokenGridData.destroyedTokens[tc.position.x] += 1;
                        }
                    }

                    tkl.Clear();
                }
            }

            tokensToAnimate.Clear();
            tokenStartPositions.Clear();
            animateTime = 0;

            TokenGridData.SettleAndSpawnTokens();

            if (tokensToAnimate.Count == 0)
                TokenGridManager.animating = false;

        }
  
    }

    public static void AddToAnimationQueue(TokenClass Token)
    {
        tokensToAnimate.Add(Token);
        tokenStartPositions.Add(Token.transform.position);
    }

    public static void RemoveFromAnimateQueue(List<int> index)
    {
        for(int i = 0; i < index.Count; i++)
        {
            tokensToAnimate.RemoveAt(index[i]);
            tokenStartPositions.RemoveAt(index[i]);
        }
    }

    public void RenderGrid()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        for(int y = 0; y < TokenGridData.GRID_SIZE; y++)
        {
            for (int x = 0; x < TokenGridData.GRID_SIZE; x++)
            {
                TokenGridData.Grid[x, y].transform = CreateNewTokenTransform(new GridPos(x, y));
            }
        }
    }

    public static Transform CreateNewTokenTransform(GridPos gPos)
    {
        return Instantiate(TokenGridData.Tokens[TokenGridData.Grid[gPos.x, gPos.y].type], new Vector3(gPos.x, gPos.y, 0), Quaternion.identity, TOKEN_PARENT) as Transform;
    }

}
