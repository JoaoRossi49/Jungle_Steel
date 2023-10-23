using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 velocity;
    float speed = 20;
    void Start()
    {
        
    }
    void Update()
    {
      transform.position += velocity * Time.deltaTime;  
        
    }
    void SetDirection(Vector3 direction){
        velocity = direction * speed;
    }
}
