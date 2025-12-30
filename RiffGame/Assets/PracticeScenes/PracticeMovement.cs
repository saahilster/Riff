using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PracticeMovement : MonoBehaviour
{
    InputSystem_Actions controls; 
    InputAction movement;
    Vector2 direction = Vector2.zero;
    Rigidbody rb;
    [SerializeField] float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        controls = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        movement = controls.Player.Move;
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        direction = movement.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.y * speed);
    }
}
