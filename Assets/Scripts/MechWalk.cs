using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechWalk : MonoBehaviour
{
    CharacterController controller;
    private Animator anim;

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float forwardSpeed = 5f;
    float strafeSpeed = 5f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    [SerializeField] private AudioSource passosAudioSource;
    [SerializeField] private AudioClip[] passosAudioClip;

    void Start()
    {
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

        //anim.SetFloat(name:"jumpSpeed", vertical);
        anim.SetFloat(name:"Direction", strafeInput);
        anim.SetFloat(name:"Speed", forwardInput);
    }
    private void Passos(){
        passosAudioSource.PlayOneShot(passosAudioClip[0]);
    }
}
