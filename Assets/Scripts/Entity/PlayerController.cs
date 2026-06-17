using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private VirtualJoystick moveJoystick;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
private void FixedUpdate()
    {
        Vector2 input = Vector2 .zero;
        if (moveJoystick != null)
        {
            input = moveJoystick.Direction;
        }
        Vector2 keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (keyboardInput.sqrMagnitude > 0.1f)
        {
            input = keyboardInput.normalized;
        }
        rb.velocity = input * moveSpeed;
    }
        
    
}
