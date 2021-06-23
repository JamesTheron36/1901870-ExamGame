using UnityEngine;

public class Collectable : MonoBehaviour{
    
    public float rotationSpeed = 30;
    public GameObject explosionParticles;

    void OnTriggerEnter(Collider other) {
        GameManager.instance.CollectStar();
        SoundManager.instance.PlayStarSound();
        Destroy(gameObject);
        var p = Instantiate(explosionParticles);
        p.transform.position = transform.position;
        Destroy(p, 1f);  
    }

    void Update(){
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}
