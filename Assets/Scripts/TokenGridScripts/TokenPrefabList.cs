using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPrefabList : MonoBehaviour
{

    public Transform black;
    public Transform blue;
    public Transform green;
    public Transform orange;

    public Transform purple;
    public Transform red;
    public Transform white;
    public Transform yellow;

    private void Awake()
    {
        //Take the tokens and add them to dicctionary
        //so they can be created by the TokenGridData

        TokenGridData.Tokens.Add(TokenClass.TokenTypes.Black, this.black);
        TokenGridData.Tokens.Add(TokenClass.TokenTypes.Blue, this.blue);
        TokenGridData.Tokens.Add(TokenClass.TokenTypes.Green, this.green);
        TokenGridData.Tokens.Add(TokenClass.TokenTypes.Orange, this.orange);

        TokenGridData.Tokens.Add(TokenClass.TokenTypes.Purple, this.purple);
        TokenGridData.Tokens.Add(TokenClass.TokenTypes.Red, this.red);
        TokenGridData.Tokens.Add(TokenClass.TokenTypes.White, this.white);
        TokenGridData.Tokens.Add(TokenClass.TokenTypes.Yellow, this.yellow);
    }
}
