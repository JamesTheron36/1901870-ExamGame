using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrier1 : MonoBehaviour
{
    [SerializeField] Collider walls;
    
    Collider col;

    public float speed;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<MeshCollider>();
        Physics.IgnoreCollision(col, walls, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
