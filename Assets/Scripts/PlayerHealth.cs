using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth = 10f;
    [SerializeField] public float health;
    [SerializeField] PlayerMovement playerMovement;
    Animator playerAnimator;
    public Slider healtslider;

    public GameObject panel;

    void Start()
    {
        health = maxHealth;
        playerAnimator = GetComponent<Animator>();
        healtslider.value = health;
    }

    void Update() {
        healtslider.value = health;
        
    }

    public float GetHealth(){
        return health;
    }

    public void TakeDamage(float damage) {
        playerMovement.takeHit();
        health -= damage;
        if (health <= 0) {
            playerMovement.Die();
            panel.SetActive(true);
        }
    }
}
