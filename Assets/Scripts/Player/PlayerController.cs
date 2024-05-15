using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private bool isAttacking = false;

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
        print("Ola");
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        isAttacking = true;
    }

    void Update() {
        PlayerInput();
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        SetAnimatorMoveParams(isAttacking ? null : movement);
    }

    private void SetAnimatorMoveParams(Vector2? movement) {
        myAnimator.SetFloat("moveX", movement.HasValue ? movement.Value.x : 0);
        myAnimator.SetFloat("moveY", movement.HasValue ? movement.Value.y : 0);
    }

    private void FixedUpdate() {
        Move();
        AdjustPlayerFacingDirection();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void Move() {
        if (isAttacking) return;

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection() {
        if (movement.x < 0) {
            mySpriteRenderer.flipX = true; 
        } else if (movement.x > 0) {
            mySpriteRenderer.flipX = false;
        }
    }

    public void DoneAttackingAnimEvent() {
        isAttacking = false;
        weaponCollider.gameObject.SetActive(false);
    }
}
