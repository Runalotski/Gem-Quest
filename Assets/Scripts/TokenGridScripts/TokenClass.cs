using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenClass
{ 
    public enum TokenTypes { EMPTY, Black, Blue, Green, Orange, Purple, Red, White, Yellow };
    public Transform transform;
    public TokenTypes type;
    public GridPos position;

    public TokenClass(Transform transform, TokenTypes type, GridPos position)
    {
        this.transform = transform;
        this.type = type;
        this.position = position;
    }
}
