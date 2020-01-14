using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenGridRenderer : MonoBehaviour
{
    static List<TokenClass> tokensToAnimate = new List<TokenClass>();
    static List<Vector3> tokenStartPositions = new List<Vector3>();

    static float animateTime = 0;

    public static void AnimateGrid()
    {
        animateTime += (Time.deltaTime * 4);

        for (int i = 0; i < tokensToAnimate.Count; i++)
        {
            Transform TokenTransform = tokensToAnimate[i].transform;
            Vector3 StartPos = tokenStartPositions[i];
            Vector3 TokenDest = new Vector3(tokensToAnimate[i].position.x, tokensToAnimate[i].position.y, TokenTransform.position.z);

            tokensToAnimate[i].transform.position = Vector3.Lerp(tokenStartPositions[i], TokenDest, animateTime);
        }

        if (animateTime >= 1f)
        {
            TokenGridManager.animating = false;
            animateTime = 0;

            foreach (TokenClass token in tokensToAnimate)
            {
                if (TokenGridData.FindMatch(token))
                {
                    List<TokenClass> tkl = TokenGridData.FindLinkedTypes(token);

                    foreach (TokenClass tc in tkl)
                    {
                        Destroy(tc.transform.gameObject);
                        Debug.Log("Spawn a new Trasform aboce level here");
                    }
                }
            }

            tokensToAnimate.Clear();
            tokenStartPositions.Clear();
        }
    }

    public static void AddToAnimationQueue(TokenClass Token, Vector3 StartLocation)
    {
        tokensToAnimate.Add(Token);
        tokenStartPositions.Add(StartLocation);
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
                TokenGridData.Grid[x, y].transform = Instantiate(TokenGridData.Tokens[TokenGridData.Grid[x, y].type], new Vector3(x,y,0), Quaternion.identity, this.transform) as Transform;
            }
        }
    }

}
