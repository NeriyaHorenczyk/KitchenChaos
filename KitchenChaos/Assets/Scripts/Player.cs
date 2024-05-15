using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update()
    {
        Vector2 moveInput = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        
        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 20f);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}

