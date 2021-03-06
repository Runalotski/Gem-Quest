﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPos {

    public int x;
    public int y;

    public GridPos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(System.Object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            GridPos gp = (GridPos)obj;
            return (x == gp.x) && (y == gp.y);
        }
    }

    public override int GetHashCode()
    {
        return (x << 2) ^ y;
    }

    public override string ToString()
    {
        return "Grid Pos (" + x + "," + y + ")";
    }
}
