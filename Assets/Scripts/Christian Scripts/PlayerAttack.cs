using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeBtwAttack;

    public Transform attackPos;
    public LayerMask enemyOne;
    public LayerMask enemyTwo;
    public LayerMask bossEnemy;
    public Animator playerAnimation;
    public float attackRange;
    public int damage;

    private bool attackReady;

    private void Start()
    {
        attackReady = true;
    }
    public void Attack()
    {
        StartCoroutine(MeleeAttack(timeBtwAttack));
    }

    public IEnumerator MeleeAttack(float coolDown)
    {
        if (attackReady)
        {
            playerAnimation.ResetTrigger("Attack1");

            playerAnimation.SetTrigger("Attack1");

            attackReady = false;

            Collider2D[] enemyOneToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyOne);

            Collider2D[] enemyTwoToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyTwo);

            Collider2D[] bossEnemyToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, bossEnemy);

            for (int i = 0; i < enemyOneToDamage.Length; i++)
            {
                enemyOneToDamage[i].GetComponent<EnemyBehaviour>().Hurt(damage);
            }

            for (int i = 0; i < enemyTwoToDamage.Length; i++)
            {
                enemyTwoToDamage[i].GetComponent<RedEnemyBehaviour>().Hurt(damage);
            }

            for (int i = 0; i < bossEnemyToDamage.Length; i++)
            {
                bossEnemyToDamage[i].GetComponent<BossBehaviour>().Hurt(damage);
            }

            GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(coolDown);

            attackReady = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}