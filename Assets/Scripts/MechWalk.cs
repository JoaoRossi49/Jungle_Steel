using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MechWalk : MonoBehaviour
{
    //Gerenciamento de vida
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    //Animações
    CharacterController controller;
    private Animator anim;


    //Movimentação
    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float forwardSpeed = 5f;
    float strafeSpeed = 5f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;


    //Audios e Audio Source
    [SerializeField] private AudioSource passosAudioSource;
    [SerializeField] private AudioClip[] passosAudioClip;
    [SerializeField] private AudioClip[] jumpAudioClip;
    [SerializeField] private AudioClip[] jetAudioClip;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;

    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;
        
        vertical += gravity * Time.deltaTime * Vector3.up;

        if(controller.isGrounded){
            vertical = Vector3.down;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            vertical = jumpSpeed * Vector3.up;
        }

        //Validação para caso o player esbarre no teto, sua velocidade de subida seja zerada.
        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0){
            vertical = Vector3.zero;
        }

        Vector3 finalVelocity = forward + strafe + vertical;
        controller.Move(finalVelocity * Time.deltaTime);

        //controladores utilizados para animações
        anim.SetFloat(name:"jumpSpeed", GetComponent<Rigidbody>().velocity.y);
        anim.SetFloat(name:"Direction", strafeInput);
        anim.SetFloat(name:"Speed", forwardInput);

        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(10);
        }

        if(currentHealth == 0){
            Application.LoadLevel(Application.loadedLevel);
        }


    }
    private void Passos(){
        passosAudioSource.PlayOneShot(passosAudioClip[0]);
    }
    private void Jet(){
        passosAudioSource.PlayOneShot(jetAudioClip[0]);
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
