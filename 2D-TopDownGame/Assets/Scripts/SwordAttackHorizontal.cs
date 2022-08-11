using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackHorizontal : MonoBehaviour
{
    Vector2 attackOffSet;

    public bool attacking;
    public float damage = 3;

    public Collider2D swordColliderHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        attackOffSet = transform.position;
        swordColliderHorizontal = GetComponent<Collider2D>();
    }

    public void AttackLeft()
    {
        attacking = true;
        swordColliderHorizontal.transform.localPosition = new Vector3(attackOffSet.x * -1, attackOffSet.y);
        swordColliderHorizontal.enabled = true;
    }

    public void AttackRight()
    {
        attacking = true;
        transform.localPosition = attackOffSet;
        swordColliderHorizontal.enabled = true;
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
