using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 velocity;
    float speed = 140;
    Transform player;
    void Start()
    {
      player = GameObject.Find("PlayerMech").transform;
      transform.LookAt(player);
    }
    void Update()
    {
      transform.position += velocity * Time.deltaTime;  
      transform.LookAt(player);  
    }
    void SetDirection(Vector3 direction){
        velocity = direction * speed;
    }

    void OnTriggerEnter(Collider other)
    {
      Debug.Log("Entrou no collider");
        if (other.gameObject.tag == "Player")
        {
            MechWalk player = other.GetComponent<MechWalk>();
            Debug.Log(player);
            if (player != null)
            {
                player.TakeDamage(10);
            }
            Destroy(gameObject);
        }
    }
}
