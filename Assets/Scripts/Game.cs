using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private float grassTime = 0; // A variable to track the time since the last grass was placed
    [SerializeField] float leftTime = 10.0f;
    [SerializeField] float GrassInterval = 0.5f; // The interval between each grass placement
    [SerializeField] GameObject GrassObject; // The prefab for the grass

    private List<Grass> grassList = new();
     

    // Function to place a grass object at a random position
    void PlaceGrass()
    {
        GameObject go = Instantiate(GrassObject, new Vector3(Random.Range(-29, 30), 0, Random.Range(-19, 20)), Quaternion.identity);
        Grass grass = go.GetComponent<Grass>();
        grassList.Add(grass);
    }

    // Place grass objects at the start of the game
    void Start()
    {
        for (int i = 1; i <= 40; i++)
        {
            PlaceGrass();
        }
    }

    void DecreaseLeftTime(float deltaTime)
    {
        leftTime -= deltaTime;
    }
    // Update is called once per frame
    void Update()
    {

        if (leftTime >= 0)
        {
            DecreaseLeftTime(Time.deltaTime);
            // Increase the grassTime by the deltaTime each frame
            grassTime += Time.deltaTime;
            // If the grassTime is greater than or equal to the GrassInterval, place a grass object and reset grassTime
            if (grassTime >= GrassInterval)
            {
                PlaceGrass();
                grassTime = 0;
            }
        }
        else
        {
            List<Player> allPlayers = new List<Player>(FindObjectsOfType<Player>());
            foreach (Player player in allPlayers)
            {
                Debug.Log(player.GetTrees().Count);
            }
        }
    }
}
 