using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] public Transform player;

    public float chaseDistance = 5f;
    public float moveSpeed = 3f;
    private Vector3 originalPosition;
    private bool isChasing = false;
    private Animator myAnimator;
    private EnemyHealth enemyHealth;
    private Camera mainCamera;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        originalPosition = transform.position;
        mainCamera = Camera.main;
    }


    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (enemyHealth.isTakingDamage) {
           StopChasing();
        } else {
            if (distanceToPlayer < chaseDistance) {
                isChasing = true;
                ChasePlayer();
            } else {
                isChasing = false;
                ReturnToOriginalPosition();
            }
        }
    }

    private void StopChasing() {
        isChasing = false;
        myAnimator.SetBool("isMoving", false);
    }


    private void ChasePlayer() {
        if (isChasing) {
            Vector3 directionToPLayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPLayer * moveSpeed * Time.deltaTime);
            myAnimator.SetBool("isMoving", true);
        }
    }

    private void ReturnToOriginalPosition() {
        if (!isChasing) {
            Vector3 directionToOriginalPosition = (originalPosition - transform.position).normalized;
            transform.Translate(directionToOriginalPosition * moveSpeed * Time.deltaTime);
            myAnimator.SetBool("isMoving", false);
        }
    }
}
