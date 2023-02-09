using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tile
{
    Ground,
    Grass,
    Tree
}

public class Map
{
    private Tile[] tiles;

    public Map(int size)
    {
        tiles = new Tile[size];
    }

    public Tile this[int index]
    {
        get
        {
            return tiles[index];
        }
        set
        {
            tiles[index] = value;
        }
    }
}
