using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] private Transform player;
    public float chaseDistance = 5f;
    public float moveSpeed = 3f;
    private Vector3 originalPosition;
    private bool isChasing = false;


    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance) {
            isChasing = true;
            ChasePlayer();
        } else {
            isChasing = false;
            ReturnToOriginalPosition();
        }
    }

    private void ChasePlayer() {
        if (isChasing) {
            Vector3 directionToPLayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPLayer * moveSpeed * Time.deltaTime);
        }
    }

    private void ReturnToOriginalPosition() {
        if (!isChasing) {
            Vector3 directionToOriginalPosition = (originalPosition - transform.position).normalized;
            transform.Translate(directionToOriginalPosition * moveSpeed * Time.deltaTime);
        }
    }
}
