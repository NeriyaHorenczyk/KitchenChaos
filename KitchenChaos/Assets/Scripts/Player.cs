using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private Vector3 lastInteractedDir;
    private ClearCounter selectedCounter;

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
            selectedCounter.Interact();
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }


    public bool IsWalking()
    {
        return isWalking;
    }

    //This method will handle the player's interaction with the environment
    private void HandleInteraction()
    {
        Vector2 moveInput = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);

        if (moveDir != Vector3.zero)
            lastInteractedDir = moveDir;

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractedDir, out RaycastHit raycastHit, interactDistance)) { 
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                if (clearCounter != selectedCounter) {
                    selectedCounter = clearCounter;
                }

            }
            else
            {
                selectedCounter = null;
            }
        }
        else
        {
            selectedCounter = null;
        }
        Debug.Log(selectedCounter);
    }

    // This method is responsible for moving the player
    private void HandleMovement()
    {
        // Get the movement vector from the GameInput class
        Vector2 moveInput = gameInput.GetMovementVectorNormalized();


        Vector3 moveDir = new Vector3(moveInput.x, 0f, moveInput.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        // Check if the player can move in the desired direction
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // If the player can't move in the desired direction, check if the player can move in the x or z direction
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
                moveDir = moveDirX;
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                    moveDir = moveDirZ;
            }
        }

        if (canMove)
            transform.position += moveDir * moveDistance;

        isWalking = moveDir != Vector3.zero;

        // Rotate the player to face the direction it is moving
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 20f);
    }
}

