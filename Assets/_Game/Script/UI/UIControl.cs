using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject setting;
    public List<Level> levels = new List<Level>();
    public LevelScript level;
    public TextMeshProUGUI tmp;
    public List<ParticleSystem> winEff;
    public int currentLevel;
    Level current;
    public static bool getHint = false;
    void Start()
    {
        currentLevel = 0;
        ChangeMode(level);            
    }

    // Update is called once per frame
    public void NextLevel()
    {        
        currentLevel++;
        DOTween.Clear();
        Destroy(current.gameObject);
        if (currentLevel >= levels.Count)
        {
            currentLevel = 0;
        }
        Invoke(nameof(Load), 0.1f);
    }
    public void PrevLevel()
    {
        Solid.canClick = true;
        currentLevel--;
        DOTween.Clear();
        Destroy(current.gameObject);
        if (currentLevel < 0)
        {
            currentLevel = levels.Count - 1;
        }
        Invoke(nameof(Load), 0.1f);



    }
    public void Replay()
    {
        Solid.canClick = true;
        DOTween.Clear();
        Destroy(current.gameObject);
        UIManager.Instance.ShowAds();
        Invoke(nameof(Load), 0.1f);
              


    }
    void Load()
    {
        Solid.canClick = true;
        tmp.text = "Level: "+ (currentLevel+1);
        current = Instantiate(levels[currentLevel]);

        Camera.main.orthographicSize = current.cameraDist;
    }
    public void ChangeMode(LevelScript script)
    {
        if(Solid.canClick)
        {
            DOTween.Clear();
            levels = script.levels.ToList();
            if (current != null)
            {
                Destroy(current.gameObject);

            }

            Invoke(nameof(Load), 0.1f);
        }
        
    }
    public void CallHint()
    {
        getHint = true;
           
    }
    public void OnWin()
    {
        foreach ( var ef in winEff)
        {
            ef.Play();
        }
        Invoke(nameof(OpenWin), 2f);
    }
    public void OpenSetting()
    {
        setting.gameObject.SetActive(!setting.activeSelf);
    }
    void OpenWin()
    {
        UIManager.Instance.ShowWin();
    }
 
    

}
