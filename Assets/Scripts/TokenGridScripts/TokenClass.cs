using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenClass
{ 
    public enum TokenTypes { EMPTY, Black, Blue, Green, Orange, Purple, Red, White, Yellow };
    public Transform transform;
    public TokenTypes type;
    public bool updated;

    public TokenClass(Transform transform, TokenTypes type, bool updated = false)
    {
        this.transform = transform;
        this.type = type;
        this.updated = updated;
    }
}
