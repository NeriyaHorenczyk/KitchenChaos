using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        
        Vector2 moveInput = playerInputActions.Player.Move.ReadValue<Vector2>(); ;


        moveInput = moveInput.normalized;
        return moveInput;
    }

}
