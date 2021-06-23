using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int stage;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;
    int starCount;
    
    // Start is called before the first frame update
    void Start()
    {
        SetStar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStar()
    {
        int stars = StarManager.levels[stage - 1];
        Debug.Log("stars" + stars);
        switch (stars)
        {
            case 0:
                Debug.Log("0 what");
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 1:
                Debug.Log("1 what");
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
            case 2:
                Debug.Log("2 what");
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                break;
            case 3:
                Debug.Log("3 what");
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                break;
        }
    }
}
