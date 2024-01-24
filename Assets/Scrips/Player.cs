using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    [SerializeField] private float movSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private void Update(){
        HandleMovement();
        
    }

    private void HandleInteractions(){
         
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = movSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerheight = 2f;
        bool canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDir, moveDistance);

        if (!canmove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirX, moveDistance);

            if (canmove)
            {
                moveDir = moveDirX;

            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

                canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerheight, playerRadius, moveDirZ, moveDistance);

                if (canmove)
                {
                    moveDir = moveDirZ;

                }
            }
        }
        if (canmove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotSpeed);
    }
    public bool IsWalking(){
        return isWalking;
    }
}
