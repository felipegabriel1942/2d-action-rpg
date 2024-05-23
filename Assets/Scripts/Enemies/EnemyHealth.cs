using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int startingHealth = 3;

    private int currentHealth;

    private Animator myAnimator;

    public bool isTakingDamage = false;

    private float damageTime = 1f;

    private float deathTime = 1f;

    private Flash flash;

    void Start(){
        currentHealth = startingHealth;
        myAnimator = GetComponent<Animator>();
        flash = GetComponent<Flash>();
    }


    public void TakeDamage(int damage) {
        if (isTakingDamage) {
            return;
        }

        currentHealth -= damage;
        isTakingDamage = true;

        StartCoroutine(DamageRoutine());

        if (currentHealth > 0) {
            myAnimator.SetTrigger("Hit");
            StartCoroutine(flash.FlashRoutine());
        } else {
            myAnimator.SetTrigger("Death");
            StartCoroutine(DestroyRoutine());
        }
    }

    private IEnumerator DestroyRoutine() {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    private IEnumerator DamageRoutine() {
        yield return new WaitForSeconds(damageTime);
        isTakingDamage = false;
    }
}
