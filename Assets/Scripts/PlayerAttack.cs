using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rb;
	Animator playerAnimator;
    PlayerMovement playerMovement;
   
    SpriteRenderer SR;
    bool attacking;

    
    //private float timeToAttack = 0.5f;
    //private float timer = 0f;
    [SerializeField] float KBForce;
    [SerializeField] float attackAnimDelay1;
    [SerializeField] float attackAnimDelay2;
    [SerializeField] int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        playerAnimator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onAttack(InputAction.CallbackContext context){
        if(!attacking){
            if(context.ReadValue<float>() < 0){ // Left mouse button
                StartCoroutine(AttackCoroutine(1, attackAnimDelay1));
            }
            if(context.ReadValue<float>() > 0){ // Right mouse button
                StartCoroutine(AttackCoroutine(2, attackAnimDelay2));
            }
            
        }
    }

     private IEnumerator AttackCoroutine(int type, float animationDelay)
    {
        attacking = true;
        playerMovement.setAttacking(true);
        if (type == 1)
            playerAnimator.SetBool("Attacking1", true);
        else if (type == 2)
            playerAnimator.SetBool("Attacking2", true);
        yield return new WaitForSeconds(animationDelay  * 0.4f);
        if(!playerMovement.hit && !playerMovement.dead){
            DealDamageToEnemies(type);
            yield return new WaitForSeconds(animationDelay * 0.6f);
        }
        playerAnimator.SetBool("Attacking1", false);
        playerAnimator.SetBool("Attacking2", false);
        attacking = false;
        playerMovement.setAttacking(false);
    }


    private void DealDamageToEnemies(int type)
    {
        Vector3 hitboxDelta;
        if (SR.flipX) { // Character facing left
            hitboxDelta = new Vector3(-2.8f,0f,0f);
        }else{// Character facing right
            hitboxDelta = new Vector3(1.8f,0f,0f);
        }
        Vector2 attackPosition = transform.position + hitboxDelta; // Center of the attack
        Vector2 attackSize = new Vector2(5f, 4.0f); // Size of the rectangle (width x height)
        float attackAngle = 0f; // Angle of the rectangle, if needed
        // Perform the overlap box check
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPosition, attackSize, attackAngle);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                //Debug.Log("Hit an enemy!");
                enemy.GetComponent<MonsterHealth>().TakeDamage(damage);
                int KBDirection;
                if(type==2){
                    if (enemy.transform.position.x <= transform.position.x){
                        KBDirection = -1;
                    }
                    else{
                        KBDirection = 1;
                    }
                    enemy.GetComponent<MonsterMovement>().GetKnocked(KBForce, KBDirection);
                }     
            }else {
                 //Debug.Log("NOONE!");
            }
        }
    }
    
}
