using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeLevel : Level
{
    public float time;
    bool timeStart = false;
    private void Update()
    {
        if(!timeStart)
        {
            foreach(var s in solidList)
            {
                if(s.isTouch)
                {
                    timeStart = true;                
                break;
                }
            }
        }
        else
        {
            if(time >0)
            time -= Time.deltaTime;
            DisplayTime();
        }
    }
    public string DisplayTime()
    {
        float second = Mathf.RoundToInt(time);
        return "00:" + second;
    }   

}
