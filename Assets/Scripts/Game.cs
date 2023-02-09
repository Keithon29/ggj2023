using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    private float grassTime = 0; // A variable to track the time since the last grass was placed
    [SerializeField] float leftTime;
    [SerializeField] float GrassInterval; // The interval between each grass placement
    [SerializeField] GameObject GrassObject; // The prefab for the grass
    [SerializeField] int GrassOriginNum;
    [SerializeField] GameObject TileObject;

    public int score;

    static int width = 8 * 6;
    static int depth = 5 * 6;

    private List<int> TileNums = Enumerable.Range(0, width * depth).ToList();

    public int LeftTileNum;

    // Place grass objects at the start of the game
    void Start()
    {
        for (int i = 1; i <= width; i++)
        {
            for (int j = 1; j <= depth; j++)
            {
                Instantiate(TileObject, new Vector3(i - width / 2, 0, j - depth / 2), Quaternion.identity);
            }
        }

        //Map map = new Map(2400);
        //for (int i = 0; i < 10; i++)
        //{
        //    if (map[i] == Tile.Ground)
        //    {
        //        Debug.Log("Ground");
        //    }
        //    else if (map[i] == Tile.Grass)
        //    {
        //        Debug.Log("Grass");
        //    }
        //    else if (map[i] == Tile.Tree)
        //    {
        //        Debug.Log("Tree");
        //    }
        //}
        for (int i = 1; i <= GrassOriginNum; i++)
        {
            PlaceGrass();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increase the grassTime by the deltaTime each frame
        grassTime += Time.deltaTime;

        if (leftTime >= 0)
        {
            leftTime -= Time.deltaTime;

            // If the grassTime is greater than or equal to the GrassInterval, place a grass object and reset grassTime
            if (grassTime >= GrassInterval)
            {
                PlaceGrass();
                grassTime = 0;
            }
        }
        else
        {
            CalculateScore();
            
        }
    }

    // Function to place a grass object at a random position
    void PlaceGrass()
    {
        LeftTileNum = TileNums.Count;
        if (LeftTileNum > 0)
        {
            int p = Random.Range(0, LeftTileNum);
            int TileNum = TileNums[p];
            TileNums.RemoveAt(p);
            Instantiate(GrassObject, new Vector3(TileNum/depth-width/2, 0, TileNum%depth-depth/2), Quaternion.identity);
        }
    }

    void CalculateScore()
    {
        List<Player> allPlayers = new List<Player>(FindObjectsOfType<Player>());
        foreach (Player player in allPlayers)
        {
            foreach (TreeObject tree in player.GetTrees())
            {
                int add = 0;
                switch (tree.GetLevel())
                {
                    case 1:
                        add = 1;
                        break;
                    case 2:
                        add = 3;
                        break;
                    case 3:
                        add = 5;
                        break;
                    case 4:
                        add = 8;
                        break;
                    case 5:
                        add = 11;
                        break;
                    case 6:
                        add = 15;
                        break;
                    case 7:
                        add = 19;
                        break;
                }
                score += add;
            }
            Debug.Log(score);
        }
    }
}
 