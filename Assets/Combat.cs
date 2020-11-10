using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public bool canAttack = true;

    [SerializeField] private Animator animator;                            // Animator for current unit
    [SerializeField] private float attackCooldown = 1f;                    // Attack cooldown in seconds

    public void damageTaken ()
    {
        if (canAttack)
        {
            canAttack = false;
            animator.SetBool("IsHurt", true);
            StartCoroutine(resetAttackTime(attackCooldown));
        }
    }

    IEnumerator resetAttackTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        canAttack = true;
        animator.SetBool("IsHurt", false);
    }

}
