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
    private SpriteRenderer mySpriteRenderer;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack() {
        print("Attacked!!!!");
        myAnimator.SetTrigger("Attack");
    }

    void Update() {
        PlayerInput();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void FixedUpdate() {
        Move();
        AdjustPlayerFacingDirection();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Move() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection() {
        if (movement.x < 0) {
            mySpriteRenderer.flipX = true; 
        } else if (movement.x > 0) {
            mySpriteRenderer.flipX = false;
        }
    }
}
