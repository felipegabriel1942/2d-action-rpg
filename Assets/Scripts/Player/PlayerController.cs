using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        PlayerInput();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
    }

    private void FixedUpdate() {
        Move();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
