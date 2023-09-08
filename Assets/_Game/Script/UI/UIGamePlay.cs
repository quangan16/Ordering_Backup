using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UIGamePlay : MonoBehaviour,IUIControl
{
    // Start is called before the first frame update
    public List<Level> levels = new List<Level>();
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI coin;
    public int currentLevel;
    public Level current;
    public static bool getHint = false;

    private void Start()
    {
        currentLevel = 0;
        Load();
    }
    public virtual void Open()
    {
        enabled= true;
        if (current)
        {
            current.gameObject.SetActive(true);
        }
        Camera.main.orthographicSize = current.cameraDist;
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        enabled= false;
        if(current)
        {
            current.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
    // Update is called once per frame
    public void NextLevel()
    {        
        currentLevel++;
        
        current.gameObject.SetActive(false);
        if (currentLevel >= levels.Count)
        {
            currentLevel = 0;
        }
        Invoke(nameof(Load), 0.1f);
    }
    public void Replay()
    {
        Solid.canClick = true;
        current.gameObject.SetActive(false);
        UIManager.Instance.ShowAds();
        Invoke(nameof(Load), 0.1f);
              


    }
    public void Load()
    {
        Solid.canClick = true;
        tmp.text = "Level: "+ (currentLevel+1);
        current = Instantiate(levels[currentLevel]);
        coin.text = PlayerPrefs.GetInt("coin", 0).ToString();

        Camera.main.orthographicSize = current.cameraDist;
    }
    public virtual void CallHint()
    {
        getHint = true;
           
    }
    public void OnWin()
    {
        UIManager.Instance.PlayEffect();
        Invoke(nameof(OpenWin), 2f);
    }
    void OpenWin()
    {
        UIManager.Instance.ShowWin();
    }
 
    

}
