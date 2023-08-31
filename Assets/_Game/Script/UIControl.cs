using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Level> levels = new List<Level>();
    public TextMeshProUGUI tmp;
    Level current;
    public int currentLevel;
    void Start()
    {
        currentLevel = 0;
        Load();
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
        DOTween.Clear();
        Destroy(current.gameObject);
        if(currentLevel >= levels.Count)
        {
            currentLevel = 0;
        }
        Invoke(nameof(Load), 0.1f);

    }
    public void PrevLevel()
    {
        currentLevel--;
        DOTween.Clear();
        Destroy(current.gameObject);
        if (currentLevel < 0) 
        {
            currentLevel = levels.Count-1;
        }
        Invoke(nameof(Load), 0.1f);
    }
    public void Replay()
    {
        DOTween.Clear();
        Destroy(current.gameObject);
        Invoke(nameof(Load), 0.1f);


    }
    void Load()
    {
        tmp.text = "Level: "+ (currentLevel+1);
        current = Instantiate(levels[currentLevel]);
    }

}
