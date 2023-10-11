using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechWalk : MonoBehaviour
{
    private Animator anim;
    public float stillRotationSpeed = 0.0025f;
    public float walkingRotationSpeed = 0.005f;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Controladores para estado de animação
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat(name:"Direction",h);
        float v = Input.GetAxis("Vertical");
        anim.SetFloat(name:"Speed",v);

        //Roda no próprio eixo
        if(v!=0){
            transform.Rotate(0, h*walkingRotationSpeed, 0);
        }else{
            transform.Translate(h*stillRotationSpeed,0,0);
        };

        //Caminha para frente ou para trás
        float translation = Input.GetAxis("Vertical") * speed;
        transform.Translate(0, 0, translation);

        
    }
}
