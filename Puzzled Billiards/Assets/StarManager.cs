using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    int stars;

    


    public int GetStars()
    {
        return stars;
    }

    public void setStars(int s)
    {
        stars = s;
    }
}
public class StarManager : Singleton<StarManager>
{
    public static int [] levels  = new int [4];
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStar(int s, int index)
    {
        if (index > 4)
        {
            Debug.Log("out of bounds");
            return;
        }
        else
        {
            levels[index - 1] = s;
        }
    }
    public int GetStar(int index)
    {
        return levels[index - 1];
    }

}


