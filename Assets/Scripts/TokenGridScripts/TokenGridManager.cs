using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenGridManager : MonoBehaviour
{
    public static bool animating = false;

    // Start is called before the first frame update
    void Start()
    {
        TokenGridData.CreateNewGrid(new TokenClass.TokenTypes[]{ TokenClass.TokenTypes.Red,
                                                                     TokenClass.TokenTypes.Blue,
                                                                     TokenClass.TokenTypes.Green,
                                                                     TokenClass.TokenTypes.White});

        this.transform.GetComponent<TokenGridRenderer>().RenderGrid();
    }

    void Update()
    {
        if(animating)
        {
            TokenGridRenderer.AnimateGrid();
        }

    }

}
