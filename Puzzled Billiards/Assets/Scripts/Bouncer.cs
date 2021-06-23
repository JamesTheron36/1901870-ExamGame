using UnityEngine;

public class Bouncer : MonoBehaviour{
    public float power;

    void OnCollisionEnter(Collision collision){
        Rigidbody rb = collision.rigidbody;
        Vector3 dir = collision.contacts[0].normal;
        rb.AddForce(dir * power, ForceMode.Impulse);
    }
}
