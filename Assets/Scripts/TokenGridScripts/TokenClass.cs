using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenClass
{ 
    public enum TokenTypes { EMPTY, Black, Blue, Green, Orange, Purple, Red, White, Yellow, COUNT };
    public Transform transform;
    public TokenTypes type;
    public GridPos position;

    public TokenClass(Transform transform, TokenTypes type, GridPos position)
    {
        this.transform = transform;
        this.type = type;
        this.position = position;
    }

    public void Randomise()
    {
        int rnd = Random.Range(1, (int)TokenTypes.COUNT);

        type = (TokenTypes)rnd;

        transform = TokenGridRenderer.CreateNewTokenTransform(position);

        //Debug.Log("=============================================== Spawned " + position + " Is Null? " + (TokenGridData.Grid[position.x, position.y].transform == null).ToString());

    }

    /*
    public override bool Equals(System.Object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            TokenClass tc = (TokenClass)obj;
            return (position.x == tc.position.x) && (position.y == tc.position.y);
        }
    }

    public override int GetHashCode()
    {
        return (position.x << 2) ^ position.y;
    }
    */
    public override string ToString()
    {
        return "Token Class (" + position.x + "," + position.y + ") Type : " + type;
    }
}
