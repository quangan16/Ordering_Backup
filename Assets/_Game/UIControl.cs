using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Level> levels = new List<Level>();
    Level current;
    public int currentLevel;
    void Start()
    {
        currentLevel = 0;
        current = Instantiate(levels[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (current != null)
        {
            if(current.isWin)
            {
                NextLevel();
            }
        }
    }
    public void NextLevel()
    {
        currentLevel++;
        Destroy(current.gameObject);
        if(currentLevel >= levels.Count)
        {
            currentLevel = 0;
        }
        current = Instantiate(levels[currentLevel]);

    }
    public void Replay()
    {
        Destroy(current.gameObject);
       
        current = Instantiate(levels[currentLevel]);
    }

}
