using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiController : MonoBehaviour
{
    /*public float maxStamina = 5f;
    public float stamina;
    public float maxMana = 5f;
    public float mana;*/

    public float Health
    {
        set {
            health = value;
            if(health <= 0)
            {
                Defeated();
            }
        }
        get {
            return health; 
        }
    }

    public float health = 10;

    void Defeated()
    {
        Debug.Log("Enemy Died");
    }

}
