using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ResetCurrentScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadCourse1()
    {
        SceneManager.LoadScene("Course_1");
    }
    public void LoadCourse2()
    {
        SceneManager.LoadScene("Course_2");
    }
    public void LoadCourse3()
    {
        SceneManager.LoadScene("Course_3");
    }
    public void LoadCourse4()
    {
        SceneManager.LoadScene("Course_4");
    }
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelector");
    }
    public void LoadCourse5()
    {
        SceneManager.LoadScene("Course_5");
    }
}
