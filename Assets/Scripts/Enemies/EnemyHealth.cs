using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 3;

    private int currentHealth;

    private Animator myAnimator;

    void Start(){
        currentHealth = startingHealth;
        myAnimator = GetComponent<Animator>();
    }


    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth > 0) {
            myAnimator.SetTrigger("Hit");
        } else {
            myAnimator.SetTrigger("Death");
            StartCoroutine(DestroyRoutine());
        }
    }

    private IEnumerator DestroyRoutine() {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
