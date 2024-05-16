using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionInterval = 2f;
    private float timer = 0f;
    private Vector2 randomDirection;

    private void Start() {
        randomDirection = GetRandomDirection();
    }

    private void Update() {
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        
        if (timer >= changeDirectionInterval) {
            randomDirection = GetRandomDirection();
            timer = 0f;
        }
    }

    private Vector2 GetRandomDirection() {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        return new Vector2(randomX, randomY).normalized;
    }
}
