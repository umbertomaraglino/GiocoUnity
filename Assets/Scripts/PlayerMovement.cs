using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	[Tooltip("Speed of horizontaly Movement")]
	[Range(5, 15)]
	[SerializeField] float speed = 1;
	private Vector2 movementInput=Vector2.zero;
	
	[Header("Jumping")]
	[Tooltip("Jumping height")]
	[Range(0, 100)]
	[SerializeField] float thrust = 300;
	bool grounded = true;
	bool jumped;
	bool knocked = false;
	public bool dead = false;
	bool attacking = false;
	public bool hit = false;

	bool sync = true;

	Rigidbody2D rb;
	public Animator playerAnimator;
	CameraController camera;
    SpriteRenderer SR;
	//OnMove checks if controlls for the movement are pressed
	public void OnMove(InputAction.CallbackContext context){
    	movementInput = context.ReadValue<Vector2>();
    }
    
	//OnJump checks if controlls for the jumping are pressed
    public void OnJump(InputAction.CallbackContext context){
    	jumped = context.action.triggered;
    }
	public void GetKnocked(float KBForce, float KBDirection){
		rb.velocity = new Vector2(KBForce*KBDirection, KBForce);
		knocked = true;
	}
	public void Die(){
		playerAnimator.SetBool("Dead", true);
		rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
		dead = true;
		disableCollision("Enemy");
	}
	//Jump is the class that includes jumping mechanics
	void Jump()
    {
		//If jump is pressed and the player is on the ground, execute jumping
		if(jumped && grounded){
						//This will add a force to the players rigidbody, so the player will jump
			rb.AddForce(transform.up*thrust*100);
			//Player is now in the air, therefore he cant jump anymore. Set this true again, if player hits the floor.
    		grounded=false;
			playerAnimator.SetTrigger("Jumping");
		}
    }
	
    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody attached to the player
        rb = GetComponent<Rigidbody2D> ();
        playerAnimator = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
    }
    
    //Move is the class that includes running mechanics
	void Move()
    {
		//Add force to the players rigidbody in the direction, of the input in movementInput.x
		rb.velocity = new Vector2 ( movementInput.x*speed, rb.velocity.y );
        if(Mathf.Abs(movementInput.x)>0.01f){ 
            playerAnimator.SetBool("Running", true);
			Debug.Log("passato");
        }
        else {
            playerAnimator.SetBool("Running", false);
        }
        if(movementInput.x>0.01f) SR.flipX = false;
        if(movementInput.x<-0.01f) SR.flipX = true;
    }
	

    // Update is called once per frame
    void FixedUpdate()
    {
		if (!dead & !hit){
			playerAnimator.SetBool("Grounded", grounded);
			playerAnimator.SetFloat("Velocity", rb.velocity.y);
			if (!knocked && !attacking && sync){
				Jump();
				Move();
			}
		}
    }

	public void setAttacking(bool flag){
		attacking = flag;
	}
    
    // OnCollisionEnter2D is called when the object collides with an collider of another object
    void OnCollisionEnter2D(Collision2D col){
    	grounded=true;
		knocked=false;
    	//checks wethere the collision is made with the floor
    	//if(col.gameObject.tag=="Floor"){
    		
    		//This bool will make jumping possible if true
    		//grounded=true;    	}
    }

	public void disableCollision(string tag){
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
	}

	public void takeHit(){
        StartCoroutine(HitCoroutine());
    }

     private IEnumerator HitCoroutine()
    {
        hit = true;
        playerAnimator.SetTrigger("Hit");
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(0.5f);
        hit = false;
    }

	public void SetSync(bool var){
		sync = var;
	}
}