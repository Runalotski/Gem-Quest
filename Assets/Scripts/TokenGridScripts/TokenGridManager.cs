using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenGridManager : MonoBehaviour
{
    public static bool animating = false;

    public static TokenClass.TokenTypes[] Level1 = { TokenClass.TokenTypes.Red,
                                                     TokenClass.TokenTypes.Blue,
                                                     TokenClass.TokenTypes.Green,
                                                     TokenClass.TokenTypes.White,
                                                     TokenClass.TokenTypes.Yellow,
                                                     TokenClass.TokenTypes.Purple};

    // Start is called before the first frame update
    void Start()
    {


        TokenGridData.CreateNewGrid(Level1);

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
