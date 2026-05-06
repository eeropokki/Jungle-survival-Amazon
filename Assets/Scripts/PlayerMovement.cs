using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;

    [SerializeField] private InputActionReference inputActionReference;


    private Rigidbody2D rb;

    private Vector2 moveInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput.normalized * playerSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        inputActionReference.action.Enable();
        inputActionReference.action.performed += OnMovePerformed;
        inputActionReference.action.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        inputActionReference.action.performed -= OnMovePerformed;
        inputActionReference.action.canceled -= OnMoveCanceled;
        inputActionReference.action.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
