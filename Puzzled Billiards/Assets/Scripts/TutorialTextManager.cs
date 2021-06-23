using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextManager : MonoBehaviour
{
    [SerializeField] GameObject objBallTut;
    [SerializeField] GameObject phaseBallTut;
    [SerializeField] GameObject stickyBallTut;
    [SerializeField] GameObject switchTut;
    [SerializeField] GameObject restartTut;
    int stage;
    int clicks;

    // Start is called before the first frame update
    void Start()
    {
        clicks = 0;
        stage = GameManager.instance.stage;
        switch (stage)
        {
            case 1:
                ShowObjBallTut();
                break;
            case 2:
                ShowStickyBallTut();
                break;
            case 3:
                ShowSwitchBallTut();
                break;
            case 4:
                ShowRestartTut();
                break;
        }
    }

    public void ShowObjBallTut()
    {
        objBallTut.SetActive(true);
        phaseBallTut.SetActive(false);
        stickyBallTut.SetActive(false);
        switchTut.SetActive(false);
        restartTut.SetActive(false);
    }
    public void ShowPhaseBallTut()
    {
        objBallTut.SetActive(false);
        phaseBallTut.SetActive(true);
        stickyBallTut.SetActive(false);
        switchTut.SetActive(false);
        restartTut.SetActive(false);
    }
    public void ShowStickyBallTut()
    {
        objBallTut.SetActive(false);
        phaseBallTut.SetActive(false);
        stickyBallTut.SetActive(true);
        switchTut.SetActive(false);
        restartTut.SetActive(false);
    }
    public void ShowSwitchBallTut()
    {
        objBallTut.SetActive(false);
        phaseBallTut.SetActive(false);
        stickyBallTut.SetActive(false); ;
        switchTut.SetActive(true);
        restartTut.SetActive(false);
    }
    public void DisableTut()
    {
        objBallTut.SetActive(false);
        phaseBallTut.SetActive(false);
        stickyBallTut.SetActive(false); ;
        switchTut.SetActive(false);
        restartTut.SetActive(false);
    }
    public void ShowRestartTut()
    {
        objBallTut.SetActive(false);
        phaseBallTut.SetActive(false);
        stickyBallTut.SetActive(false); ;
        switchTut.SetActive(false);
        restartTut.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicks++;
            if(stage == 1)
            {
                ShowPhaseBallTut();
            }
            else
            {
                DisableTut();
            }
        }
    }
}
