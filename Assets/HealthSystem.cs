using System;
using UnityEngine;


public class HealthSystem : MonoBehaviour {
    public float health;
    public float maxHealth;

    public event Action Death = delegate { };


    void Start() {
        health = maxHealth;
    }

    public void ReceiveDamage(float dmg) {
        health -= dmg;
        if (health > maxHealth) {
            health = maxHealth;
        }
        if (health < 0) {
            Death();
            Destroy(gameObject);
        }
    }

}
