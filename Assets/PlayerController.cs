using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    
    public GameObject player;

    public Camera playerCamera;

    public InputAction moveInput;
    public InputAction lookInput;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        moveInput.Enable();
        lookInput.Enable();
    }
    
    
    
}
