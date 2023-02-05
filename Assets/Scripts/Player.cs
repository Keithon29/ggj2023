using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Private integer variable to keep track of grass points collected by the player
    private int grassPoints = 0;

    // Private list to store the trees the player has planted
    private List<GameObject> trees = new List<GameObject>();

    // Private float to store the speed of the player
    private float speed = 10.0f;

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

    private void Start()
    {
        // Find the actions "Move", "Plant", and "Pull" in the current action map
        m_Move = m_Input.currentActionMap.FindAction("Move");
        m_Plant = m_Input.currentActionMap.FindAction("Plant");
        m_Pull = m_Input.currentActionMap.FindAction("Pull");
    }

    private void Update()
    {
        // Call the Move function
        Move();

        // Add the buttonActionA method as a delegate to the performed event of the Plant action
        m_Plant.performed += context => buttonActionA();

        // Add the buttonActionB method as a delegate to the performed event of the Pull action
        m_Pull.performed += context => buttonActionB();
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
        // Instantiate a tree object in front of the player and add it to the trees list
        trees.Add(Instantiate(treeObject, transform.position + transform.forward, Quaternion.identity));
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

    public List<TreeObject> GetTrees()
    {
        List<TreeObject> treeList = new List<TreeObject>();
        foreach (GameObject go in trees)
        {
            TreeObject treeObject = go.GetComponent<TreeObject>();
            if (treeObject != null)
            {
                treeList.Add(treeObject);
            }
        }
        return treeList;

    }
}