using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovementButBetter : MonoBehaviour
{
    PlayerController playerController;

    public Vector3 currentVelocity;
    
    public float acceleration;
    public float maxSpeed;
    
    public float rotationSpeed = 10f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        HandleMovement(playerController.moveInput.ReadValue<Vector2>());
        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        
        if (playerController.lookInput.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 lookDirection = playerController.lookInput.ReadValue<Vector2>();
            targetDirection = new Vector3(lookDirection.x, 0, lookDirection.y);
        }
        else
        {
            // if not input from controller use mouse
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = playerController.playerCamera.ScreenPointToRay(mousePosition);
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 worldMousePosition = ray.GetPoint(rayDistance);
                targetDirection = worldMousePosition - transform.position;
                targetDirection.y = 0f;
            }
        }

        
        if (targetDirection.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            playerController.player.transform.rotation = Quaternion.Slerp(playerController.player.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    


    private void HandleMovement(Vector2 input)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y) * maxSpeed;
        
        // no input? stop pls
        if (input == Vector2.zero)
        {
            currentVelocity = Vector3.zero;
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration);
        }

        
        playerController.rb.velocity = new Vector3(currentVelocity.x, playerController.rb.velocity.y, currentVelocity.z);
    }
}
