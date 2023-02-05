using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Private integer variable to keep track of grass points collected by the player
    private int grassPoints = 0;

    //public int TreePoints = 0;

    private int selectedTreeLevel = 1;

    // Private list to store the trees the player has planted
    private List< TreeObject> trees = new List<TreeObject>();

    public List<TreeObject> GetTrees()
    {
        return trees;
    }

    // Private float to store the speed of the player
    private float speed = 10.0f;

    [SerializeField] Score score;

    // Reference to the tree object prefab
    [SerializeField] GameObject treeObject;

    // Reference to the Rigidbody component of the player
    [SerializeField] Rigidbody playerRig;

    // Reference to the PlayerInput component for input management
    [SerializeField] PlayerInput m_Input = null;

    // Variables to store the input actions for moving, planting, and pulling
    InputAction m_Move;
    InputAction m_Plant;
    InputAction m_Pull;
    InputAction m_Decrease_selected_tree_level;
    InputAction m_Increase_selected_tree_level;

    int PlayerIndex;

    private void Start()
    {
        // Find the actions "Move", "Plant", and "Pull" in the current action map
        m_Move = m_Input.currentActionMap.FindAction("Move");
        m_Plant = m_Input.currentActionMap.FindAction("Plant");
        m_Pull = m_Input.currentActionMap.FindAction("Pull");

        PlayerIndex = m_Input.playerIndex + 1;
        var scoreObj = GameObject.Find("Score" + PlayerIndex.ToString());
        score = scoreObj.GetComponent<Score>();
        score.SetScore(0, PlayerIndex);

        m_Decrease_selected_tree_level = m_Input.currentActionMap.FindAction("Decrease selected tree level");
        m_Increase_selected_tree_level = m_Input.currentActionMap.FindAction("Increase selected tree level");

    }

    private void Update()
    {
        // Call the Move function
        Move();

        // Add the buttonActionA method as a delegate to the performed event of the Plant action
        m_Plant.performed += context => buttonActionA();

        // Add the buttonActionB method as a delegate to the performed event of the Pull action
        m_Pull.performed += context => buttonActionB();

        // Add the buttonActionL method as a delegate to the performed event of the Pull action
        m_Decrease_selected_tree_level.performed += context => buttonActionL();

        // Add the buttonActionR method as a delegate to the performed event of the Pull action
        m_Increase_selected_tree_level.performed += context => buttonActionR();
    }

    // Function to handle player movement
    void Move()
    {
        // Read the value of the move action and multiply it by the speed
        Vector2 vector = m_Move.ReadValue<Vector2>() * speed;
        
        // If the magnitude of the vector is greater than 0, move the player and face the forward direction
        if (vector.sqrMagnitude > 1)
        {
            playerRig.velocity = new Vector3(vector.x, 0, vector.y);
            transform.forward = new Vector3(vector.x, 0, vector.y);
        }
        // If the magnitude of the vector is 0, stop the player
        else
        {
            playerRig.velocity = Vector3.zero;
        }
    }

    // Function to plant a tree
    void plantTree()
    {
        if (grassPoints >= 1)
        {
            int plantCost = 1;
            switch (selectedTreeLevel)
            {
                case 1:
                    plantCost = 1;
                    break;
                case 2:
                    plantCost = 4;
                    break;
                case 3:
                    plantCost = 9;
                    break;
                case 4:
                    plantCost = 16;
                    break;
                case 5:
                    plantCost = 25;
                    break;
                case 6:
                    plantCost = 36;
                    break;
                case 7:
                    plantCost = 49;
                    break;
            }
            if (grassPoints >= plantCost)
            {
                grassPoints -= plantCost;
                score.SetScore(grassPoints, PlayerIndex);
                // Instantiate a tree object in front of the player and add it to the trees list
                GameObject treeGO = Instantiate(treeObject, transform.position + transform.forward, Quaternion.identity);
                TreeObject tree = treeGO.GetComponent<TreeObject>();
                //TreePoints += plantCost;
                tree.SetLevel(selectedTreeLevel);
                trees.Add(tree);
            }
        }
    }

    // Function to handle the action performed when the A button is pressed
    void buttonActionA()
    {
        // Check if there is no obstacle in front of the player
        if (!Physics.Raycast(gameObject.transform.position, transform.forward, 2f))
        {
            // Call the plantTree function
            plantTree();
        }
    }

    // Function to handle the action of pulling grass
    private void PullGrass(Grass grass)
    {
        // Increase the grass points when pulling the grass
        grassPoints += grass.GetPoints();
        
        score.SetScore(grassPoints, PlayerIndex);

        // Destroying the grass game object
        Destroy(grass.gameObject);
    }

    // Function to handle the action performed when the B button is pressed
    void buttonActionB()
    {
        RaycastHit raycastHitObject;
        // Raycast to check if the player is facing any object with 2 units distance
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out raycastHitObject, 2f))
        {
            // Check if the object has a Grass component
            Grass grass = raycastHitObject.transform.GetComponent<Grass>();
            if (grass != null)
            {
                // Call the PullGrass function to handle pulling the grass
                PullGrass(grass);
            }
        }
    }

    void buttonActionL()
    {
         if(selectedTreeLevel >= 2 && selectedTreeLevel <= 7)
        {
            selectedTreeLevel -= 1;
        }
        Debug.Log(selectedTreeLevel);
    }

    void buttonActionR()
    {
        if(selectedTreeLevel >= 1 && selectedTreeLevel <= 6)
        {
            selectedTreeLevel += 1;
        }

        Debug.Log(selectedTreeLevel);
    }
}