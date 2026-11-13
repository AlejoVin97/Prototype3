using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public InputAction jumpAction;
    public InputAction dashAction;
    //private bool isInMenu=false;
    public float jumpForce = 10;
    public float doubleJumpForce = 5;
    public float gravityModifier;
    public float dashSpeedModifier = 2.0f;
	public bool isOnGround = true;
    public bool isAirJump = false;
    public bool isDashing;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    private AudioSource playerAudio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jumpAction.Enable();
        dashAction.Enable();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //While the game hasn't ended
        if (!gameOver)
        {
            //if the player has pressed the jump button
            if (jumpAction.triggered)
            {
                if (isOnGround)//while on the ground
                {
                    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);//lifts the character off the ground
                    isOnGround = false;
                    dirtParticle.Stop();
                    playerAnim.SetTrigger("Jump_trig");//show the character jumping
                    playerAudio.PlayOneShot(jumpSound, 1.0f);
                    isAirJump = false;//and identify that an air jump hasn't been done yet 
                }
                else if (!isOnGround && !isAirJump)//else, while not in the ground and not having done an air jump before
                {
                    playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);//lift the character from its current position
                    isAirJump = true;//identify that an air jump has been made
                    playerAnim.Play("Running_Jump",3,0f);//and show the character jumping
                    playerAudio.PlayOneShot(jumpSound, 1.0f);
                }


            }
            //if the player is (still) pressing the dash button
            if (dashAction.IsPressed())
            {//the character starts dashing
                isDashing = true;
                playerAnim.SetFloat("Speed_Multiplier", dashSpeedModifier);
            }
            else if (isDashing)//else, if the player isn't dashing
            {//the character finishes dashing, and moves at a base speed
                isDashing = false;
                playerAnim.SetFloat("Speed_Multiplier", 1.0f);
            }
        }
        
        

        

    }
	private void OnCollisionEnter(Collision collision)
	{
        if (!gameOver) { 
            if (collision.gameObject.CompareTag("Ground") && !gameOver)
            {
                isOnGround = true;
                dirtParticle.Play();
            }
            else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
            {
            
                gameOver = true;
                Debug.Log("Game Over!");
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSound, 1.0f);
            }
        }

    }
}
