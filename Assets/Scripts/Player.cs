using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Number of points the player has earned from pulling grass
    private int grassPoints = 0;
    // Speed of the player's movement
    private float speed = 10.0f;

    [SerializeField] Rigidbody playerRig; // Reference to the player's rigidbody component
    [SerializeField] PlayerInput m_Input = null; // Reference to the PlayerInput component

    // Input actions for movement and pulling
    InputAction m_Move;
    InputAction m_Pull;

    private void Start()
    {
        // Get references to the "Move" and "Pull" actions
        m_Move = m_Input.currentActionMap.FindAction("Move");
        m_Pull = m_Input.currentActionMap.FindAction("Pull");
    }

    private void Update()
    {
        // Call the Move method each frame
        Move();

        // Subscribe to the performed event for the "Pull" action
        m_Pull.performed += context => buttonActionB();
    }

    // Method for moving the player
    void Move()
    {
        Vector2 vector = m_Move.ReadValue<Vector2>() * speed;

        // If the input vector is not equal to zero
        if (vector.sqrMagnitude > 0)
        {
            // Update the player's velocity and forward direction
            playerRig.velocity = new Vector3(vector.x, 0, vector.y);
            transform.forward = new Vector3(vector.x, 0, vector.y);
        }
        else
        {
            // If the input vector is equal to zero, set the player's velocity to zero
            playerRig.velocity = Vector3.zero;
        }
    }

    // Method for pulling grass
    void PullGrass(Grass grass)
    {
        // Increase the player's grass points and destroy the pulled grass object
        grassPoints += grass.GetPoints();
        Destroy(grass.gameObject);
        Debug.Log(grassPoints);
    }

    // Method called when the "Pull" action is performed
    void buttonActionB()
    {
        RaycastHit raycastHitObject;
        // If a grass object is within the range of the raycast
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out raycastHitObject, 2f))
        {
            Grass grass = raycastHitObject.transform.GetComponent<Grass>();
            // If the raycast hit object has a Grass component
            if (grass != null)
            {
                // Call the PullGrass method
                PullGrass(grass);
            }
        }
    }
}
