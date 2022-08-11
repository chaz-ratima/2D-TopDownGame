using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackVertical : MonoBehaviour
{
    Vector2 attackOffSet;

    public bool attacking;
    public float damage = 3;
    public Collider2D swordColliderVertical;

    public SwordAttackHorizontal attackingAlready;
    // Start is called before the first frame update
    void Start()
    {
        attackOffSet = transform.position;
        swordColliderVertical = GetComponent<Collider2D>();
    }

    public void AttackUp()
    {
        if (attackingAlready.attacking == false)
        {
            attacking = true;
            swordColliderVertical.transform.localPosition = new Vector3(attackOffSet.x, attackOffSet.y * -1);
            swordColliderVertical.enabled = true;
        }
    }

    public void AttackDown()
    {
        if (attackingAlready.attacking == false)
        {
            attacking = true;
            transform.localPosition = attackOffSet;
            swordColliderVertical.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Deal damage to the enemy
            EnemyAiController enemy = collision.GetComponent<EnemyAiController>();

            if (enemy != null)
            {
                enemy.Health -= damage;
                Debug.Log(enemy.Health);
            }
        }
    }
}
