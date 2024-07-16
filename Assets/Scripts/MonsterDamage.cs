using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float KBForce;
    [SerializeField] float KBTime;

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            playerHealth.TakeDamage(damage);

            int KBDirection;

            if (collision.transform.position.x <= transform.position.x){
                KBDirection = -1;
            }
            else{
                KBDirection = 1;
            }
            
            playerMovement.GetKnocked(KBForce, KBDirection);           
        }
    }
}
