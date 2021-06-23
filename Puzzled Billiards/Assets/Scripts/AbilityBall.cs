using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ability
{
    phase = 0,
    sticky = 1
}

public class AbilityBall : MonoBehaviour
{
    //public PredictionManager pm;
    [SerializeField] Camera main;
    [SerializeField] float bounceMod = 0.5f;
    [SerializeField] float lineDistance = 10f;
    [SerializeField] LayerMask mask;
    [SerializeField] Material phaseMat;
    [SerializeField] Material stickyMat;
    [SerializeField] Ability startingAbility;
    Ability ability;
    Vector3 point;
    Vector3 direction;
    Vector3 lastVelocity;
    Transform startPos;
    Rigidbody rb;
    LineRenderer lr;
    MeshRenderer mr;
    public float maxPower = 100f;
    bool abilityUsed;
    bool clicked;
    bool stuck;



    // Start is called before the first frame update
    void Start()
    {
        stuck = false;
        startPos = transform;
        clicked = false;
        abilityUsed = false;
        ability = startingAbility;
        Debug.Log(GetAbility() + "ability");
        SetMaterial();
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        mr = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        lastVelocity = rb.velocity;
        //Debug.Log("last velocity = " + lastVelocity);
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.tag != "ball")
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

            if(Input.GetMouseButtonDown(1) && clicked == false)
            {
                SwitchAbility();
            }

            //PredictionManager.instance.predict(this.gameObject, transform.position, force);
            if (Input.GetMouseButtonDown(0) && clicked == false && abilityUsed == false)
            {
                if(GetAbility() == 0 && GameManager.instance.blueRem > 0)
                {
                    Hit(power, direction);
                }
                else if(GetAbility() == 1 && GameManager.instance.orangeRem > 0)
                {
                    
                    Hit(power, direction);
                    if (stuck)
                    {
                        stuck = false;
                        abilityUsed = true;
                    }
                }
                
            }


        }
        else
        {
            point = transform.position;
        }
        if (stuck)
        {
            lr.enabled = true;
        }
        if(rb.velocity.magnitude == 0 && clicked == true)
        {
            lr.enabled = true;
            clicked = false;
            abilityUsed = false;
            int num = GetAbility();
            
            if(stuck == false)
            {
                switch (num)
                {
                    case 0:
                        GameManager.instance.HitBlueBall();
                        break;
                    case 1:
                        GameManager.instance.HitOrangeBall();
                        break;
                }
            }
            
        }
        



    }
    public int GetAbility()
    {
        return (int)ability;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GetAbility() == 1 && abilityUsed == false && collision.collider.tag != "ground" && collision.collider.tag != "ball")
        {
            rb.velocity = Vector3.zero;
            SoundManager.instance.PlayStickSound();
            stuck = true;
            clicked = false;
        }
        else if (collision.collider.tag != "ground")
        {
            Rigidbody rigid = GetComponent<Rigidbody>();
            float speed = lastVelocity.magnitude;
            speed = speed * bounceMod;
            Vector3 dir = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rigid.velocity = Mathf.Max(speed, 0f) * dir;
        }
        
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "oob")
        {
            SceneController.instance.ResetCurrentScene();
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "phase" && GetAbility() == 0)
        {
            Debug.Log("phased");
            abilityUsed = true;
            GameManager.instance.EnablePhaseWalls();
        }
    }

    public void Hit(float power, Vector3 direction)
    {
        clicked = true;
        lr.enabled = false;
        rb.velocity = power * direction;
    }
    public void Hit(Vector3 direction, float power)
    {
        rb.AddForce(direction * power, ForceMode.Impulse);
    }
    public bool GetAbilityUsed()
    {
        return abilityUsed;
    }
    public void DrawLine(List<Vector3> points)
    {
        Debug.Log(points.Count);
        lr.positionCount = points.Count;
        for (int loop = 0; loop < lr.positionCount; loop++)
        {
            lr.SetPosition(loop, points[loop]);
        }
    }
    public void SwitchAbility()
    {
        int num = GetAbility();
        switch (num)
        {
            case 0:
                ability = (Ability) 1;
                SetMaterial();
                break;
            case 1:
                ability = (Ability) 0;
                SetMaterial();
                break;
        }
    }
    public List<Vector3> SimLine(Vector3 direction, float distance)
    {
        List<Vector3> points = new List<Vector3>();
        points.Add(transform.position);
        float remainingDist = distance;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, remainingDist) && hit.collider.tag != "phase" && hit.collider.tag != "ground")
        {
            Debug.Log(hit.collider.gameObject.name);
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
        else
        {
            Vector3 endPt = (remainingDist * direction) + transform.position;
            points.Add(endPt);

            //Debug.DrawRay(transform.position, direction * remainingDist, Color.red);
        }
        return points;
    }

    

    public void SetMaterial()
    {
        int num = GetAbility();
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        switch (num)
        {
            case 0:
                mesh.material = phaseMat;
                break;
            case 1:
                mesh.material = stickyMat;
                break;
        }
    }
}
