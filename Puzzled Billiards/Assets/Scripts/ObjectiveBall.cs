using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBall : MonoBehaviour
{
    //public PredictionManager pm;
    [SerializeField] Camera main;
    [SerializeField] float bounceMod = 0.5f;
    [SerializeField] float lineDistance = 10f;
    [SerializeField] LayerMask mask;
    [SerializeField] Transform holePoint;
    [SerializeField] float bouncerMod;
    [SerializeField] ForceField ff;

    bool clicked;
    Vector3 point;
    Vector3 direction;
    Vector3 lastVelocity;
    Rigidbody rb;
    LineRenderer lr;
    public float maxPower = 100f;
    bool overHole;
    
    // Start is called before the first frame update
    void Start()
    {
        overHole = false;
        clicked = false;
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        lastVelocity = rb.velocity;
        if(overHole == false)
        {
            lastVelocity.y = 0;
            rb.velocity = lastVelocity;
        }
        
        //Debug.Log("last velocity = " + lastVelocity);
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider.tag != "ball")
        {
            point = hit.point;
            direction = point - transform.position;
            //Debug.Log(direction.magnitude);
            direction.y = 0;
            float mag = Mathf.Abs(direction.magnitude);
            direction = direction.normalized;
            float power = 0;
            if (mag >= lineDistance)
            {
                List<Vector3> points = SimLine(direction, lineDistance);
                DrawLine(points);
                power = maxPower;
            }
            else
            {

                List<Vector3> points = SimLine(direction, mag);
                DrawLine(points);
                power = (mag / lineDistance) * maxPower;
            }
            
            //Debug.DrawRay(transform.position, direction, Color.blue);
            //Debug.Log(point);
            //Vector3 force = direction * maxPower;

            
    
            //PredictionManager.instance.predict(this.gameObject, transform.position, force);
            if (Input.GetMouseButtonDown(0) && GameManager.instance.GetObjBall() == false && clicked == false)
            {
                SoundManager.instance.PlayHitSound();
                clicked = true;
                Hit(power, direction);
            }
            
            
        }
        else
        {
            point = transform.position;
        }
        
        
        if(rb.velocity.magnitude < 0.5f && clicked)
        {
            rb.velocity = Vector3.zero;
            GameManager.instance.SetObjBall();
        }



    }

    private void OnTriggerStay(Collider col)
    {
        
        if (col.tag == "hole")
        {
            overHole = true;
            rb.constraints = RigidbodyConstraints.None;
            Debug.Log("in field");
            Vector3 dir = holePoint.position - transform.position;
            dir = dir.normalized;
            rb.velocity = dir * lastVelocity.magnitude;
        }
        
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "forceSwitch")
        {
            ff.Switch();
        }
    }
    public bool isStationary()
    {
        if(rb.velocity.magnitude == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "phase")
        {
            rb.velocity = lastVelocity * -1;
        }
        else if(col.tag == "goal" && !GameManager.instance.GetClr())
        {
            SoundManager.instance.PlayPocketSound();
            StartCoroutine(ClearDelay());
        }
        else if(col.tag == "oob")
        {
            Debug.Log("out of bounds");
        }
        if(col.tag == "forceField")
        {
            rb.velocity = ff.direction * ff.magnitude;
        }
        if (col.tag == "oob")
        {
            SceneController.instance.ResetCurrentScene();
        }
    }

    public void Hit(float power, Vector3 direction)
    {
        lr.enabled = false;
        rb.velocity = power * direction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag != "ground" && collision.collider.tag != "ball")
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            float speed = lastVelocity.magnitude;
            speed = speed * bounceMod;
            Vector3 dir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rigid.velocity = Mathf.Max(speed, 0f) * dir;
        }
        else if (collision.collider.tag == "ball")
        {
            SoundManager.instance.PlayColideSound();
        }
        else if(collision.collider.tag == "bouncer")
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            float speed = lastVelocity.magnitude;
            speed = speed * bouncerMod;
            Vector3 dir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rigid.velocity = Mathf.Max(speed, 0f) * dir;
        }
        /*
        else if(collision.collider.tag == "bouncer")
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.drag = 0;
            float speed = lastVelocity.magnitude;
            speed = speed * bounceMod;
            Vector3 dir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rigid.velocity = Mathf.Max(speed, 0f) * dir;
        }
        */
    }
    /*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "ball")
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            Vector3 ballVel = collision.rigidbody.velocity;
            float magnitude = ballVel.magnitude;
            rigid.velocity = (ballVel.normalized * (-1 * magnitude));

        }
    }
    */

    public void Hit(Vector3 direction, float power)
    {
        rb.AddForce(direction * power, ForceMode.Impulse);
    }
    public void DrawLine(List<Vector3> points)
    {
        //Debug.Log(points.Count);
        lr.positionCount = points.Count;
        for(int loop = 0; loop < lr.positionCount; loop++)
        {
            lr.SetPosition(loop, points[loop]);
        }
    }

    public List<Vector3> SimLine(Vector3 direction, float distance)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(transform.position);
        float remainingDist = distance;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, remainingDist))
        {
            Vector3 endPt = (remainingDist * direction) + transform.position;
            points.Add(endPt);

            //Debug.DrawRay(transform.position, direction * remainingDist, Color.red);
        }

        else if (Physics.Raycast(ray, out hit, remainingDist))
        {
            Vector3 point = hit.point;
            points.Add(point);
            Vector3 line = point - transform.position;
            float dis = line.magnitude;
            //Debug.DrawRay(transform.position, direction * dis, Color.red);
            remainingDist -= dis;
            //Debug.Log(remainingDist + "remaining");
            Vector3 reflectDir = Vector3.Reflect(line.normalized, hit.normal);
            ray = new Ray(point, reflectDir);
            //Debug.DrawRay(point, reflectDir * remainingDist, Color.red);
            Vector3 endPt = point + (reflectDir * remainingDist);
            points.Add(endPt);
            
        }
        return points;
    }
    IEnumerator ClearDelay()
    {
        GameManager.instance.ClearStage();
        yield return new WaitForSeconds(1);
    }
}
