using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Solid> solidList => GetComponentsInChildren<Solid>().ToList();
    public bool isWin;
   

    // Update is called once per frame
    void Update()
    {
        CheckLevel();
    }
    void CheckLevel()
    {
        if (solidList.Count > 0)
            return;


        isWin= true;
    }
}
