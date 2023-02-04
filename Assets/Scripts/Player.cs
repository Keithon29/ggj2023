using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    float speed = 10.0f;

    [SerializeField] Rigidbody playerRig;

    [SerializeField] PlayerInput m_Input = null;

    InputAction m_Move;

    private void Start()
    {
        m_Move = m_Input.currentActionMap.FindAction("Move");
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 vector = m_Move.ReadValue<Vector2>() * speed;

        if (vector.sqrMagnitude > 0)
        {
            playerRig.velocity = new Vector3(vector.x, 0, vector.y);
        }
        else
        {
            playerRig.velocity = Vector3.zero;
        }
    }
}
