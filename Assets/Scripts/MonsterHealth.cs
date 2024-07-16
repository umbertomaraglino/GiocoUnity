using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float health;

    [SerializeField] MonsterMovement monsterMovement;
    Animator monsterAnimator;

    public Slider healtslider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healtslider.value = health;
        monsterAnimator = GetComponent<Animator>();
        monsterMovement = GetComponent<MonsterMovement>();
    }

     public void TakeDamage(float damage) {
        monsterMovement.takeHit();
        health -= damage;
        if (health <= 0) {
            monsterMovement.Die();
        }
    }
    // Update is called once per frame
    void Update()
    {
        healtslider.value = health;
    }
}
