using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenGridRenderer : MonoBehaviour
{
 
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
