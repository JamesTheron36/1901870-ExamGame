using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrier2 : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "ball")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = rb.velocity / 10;
            anim.enabled = true;
        }
    }
}
