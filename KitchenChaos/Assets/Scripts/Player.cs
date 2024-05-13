using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    private bool isWalking;

    private void Update()
    {
        Vector2 moveInput = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            moveInput.y = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveInput.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveInput.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveInput.x = +1;
        }

        
        moveInput = moveInput.normalized;
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        
        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 15f);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}

