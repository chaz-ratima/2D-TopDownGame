using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth = 5f;
    public float health;
    public float maxStamina = 5f;
    public float stamina;
    public float maxMana =5f;
    public float mana;

    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
        mana = maxMana;
    }

    void Update()
    {
        
    }
}
