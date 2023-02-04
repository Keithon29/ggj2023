using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Player : MonoBehaviour
{
    float speed = 10.0f;
    [SerializeField] Rigidbody playerRig;
    void Move()
    {
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }
 
        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= speed * transform.forward * Time.deltaTime;
        }
 
        // Dキー（右移動）
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }
 
        // Aキー（左移動）
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }
    }

    void RigMove()
    {
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerRig.velocity = speed * transform.forward;
        }
        // Sキー（後方移動）
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerRig.velocity = -speed * transform.forward;
        }
        // Dキー（右移動）
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRig.velocity =  speed * transform.right;
        }
        // Aキー（左移動）
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRig.velocity = -speed * transform.right;
        } else {
            playerRig.velocity = Vector3.zero;
        }
    }
    void Update()
    {
        RigMove();
    }
}
