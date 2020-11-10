using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private LayerMask opponentLayer;                           // A mask determining what is opponent
    [SerializeField] private Animator enemyAnimator;                            // Animator for enemy
    [SerializeField] private float jumpKillForce = 100f;                        // Force added to opponent for successful kill
    [SerializeField] private float attackFactor = 1f;                           // Applied to move opponents on failed attacks
    [Range(0, 1)] [SerializeField] private float jumpKillDirection = 0.5f;      // Tolerance on jump kill direction using Vector2 y
    [SerializeField] private Combat opponentCombat;                             // Combat controller of opponent

    private bool isDead = false;               // If enemy is dead

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collision's layer is within opponentLayer layermask
        if (!isDead && (opponentLayer == (opponentLayer | (1 << collision.gameObject.layer))))
        {
            Vector2 collisionDirection = (collision.rigidbody.position - collision.otherCollider.attachedRigidbody.position).normalized;
            if (collisionDirection.y >= jumpKillDirection && opponentCombat.canAttack)
            {
                collision.rigidbody.AddForce(new Vector2(0f, jumpKillForce)); // Add a jump force to opponent
                isDead = true;
                enemyAnimator.SetBool("IsDead", true);
            }
            else
            {
                Vector2 counterAttack = collisionDirection * attackFactor;
                collision.rigidbody.MovePosition(collision.rigidbody.position + counterAttack);
                opponentCombat.damageTaken();
            }
        }
    }
}