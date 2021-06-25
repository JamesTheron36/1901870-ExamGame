using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int totalShots = 3;
    [SerializeField] List<GameObject> phaseWalls;
    [SerializeField] GameObject abilityBall;
    [SerializeField] AbilityBall ability;
    [SerializeField] GameObject [] Stars = new GameObject[3];
    [SerializeField] GameObject clearMenu;
    [SerializeField] Text whiteRemText;
    [SerializeField] Text blueRemText;
    [SerializeField] Text orangeRemText;
    [SerializeField] ObjectiveBall objBall;
    public int stage = 1;

    int whiteRem = 1;
    public int blueRem;
    public int orangeRem;

    int remShots;
    bool objBallSet;
    int stars;
    bool clear;

    public bool GetClr()
    {
        return clear;
    }
    // Start is called before the first frame update
    void Start()
    {
        whiteRem = 1;
        clear = false;
        stars = 0;
        remShots = totalShots;
        objBallSet = false;
        abilityBall.SetActive(false);
        whiteRemText.text = "x1";
        blueRemText.text = "x" + blueRem.ToString();
        orangeRemText.text = "x" + orangeRem.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DisablePhaseWalls();
            Debug.Log("disable");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EnablePhaseWalls();
            Debug.Log("enable");
        }

        
        if(ability.GetAbility() == 0 && remShots > 0 && ability.GetAbilityUsed() == false && objBallSet)
        {
            Debug.Log("hello there");
            DisablePhaseWalls();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneController.instance.ResetCurrentScene();
        }

        if (blueRem == 0 && whiteRem == 0 && orangeRem == 0 && clear == false && objBall.isStationary())
        {
            StartCoroutine(ResetDelay());
        }

    }

    public void SetStars()
    {
        switch (stars)
        {
            case 0:
                Stars[0].SetActive(false);
                Stars[1].SetActive(false);
                Stars[2].SetActive(false);
                break;
            case 1:
                Stars[0].SetActive(true);
                Stars[1].SetActive(false);
                Stars[2].SetActive(false);
                break;
            case 2:
                Stars[0].SetActive(true);
                Stars[1].SetActive(true);
                Stars[2].SetActive(false);
                break;
            case 3:
                Stars[0].SetActive(true);
                Stars[1].SetActive(true);
                Stars[2].SetActive(true);
                break;

        }
    }
    public void HitBlueBall()
    {
        blueRem--;
        blueRemText.text = "x" + blueRem.ToString();
        
    }
    public void HitOrangeBall()
    {
        orangeRem--;
        orangeRemText.text = "x" + orangeRem.ToString();
        
    }
    public void CollectStar()
    {
        stars++;
    }
    public void SetObjBall()
    {
        objBallSet = true;
        abilityBall.SetActive(true);
    }
    public bool GetObjBall()
    {
        whiteRem = 0;
        whiteRemText.text = "x0";
        return objBallSet;
    }
    public void DisablePhaseWalls()
    {
        foreach(GameObject wall in phaseWalls)
        {
            Collider col1 = wall.GetComponent<MeshCollider>();
            Collider col2 = abilityBall.GetComponent<Collider>();
            Physics.IgnoreCollision(col1, col2, true);
        }
    }
    public void EnablePhaseWalls()
    {
        foreach (GameObject wall in phaseWalls)
        {
            Collider col1 = wall.GetComponent<MeshCollider>();
            Collider col2 = abilityBall.GetComponent<Collider>();
            Physics.IgnoreCollision(col1, col2, false);
        }
    }

    public void ClearStage()
    {
        Debug.Log(stars + "stars");
        StarManager.levels[stage - 1] = stars;
        clear = true;
        clearMenu.SetActive(true);
        SetStars();
        SoundManager.instance.PlayWinSound();
    }
     IEnumerator ResetDelay()
    {
        SceneController.instance.ResetCurrentScene();
        yield return new WaitForSeconds(1.5f);
    }
}
