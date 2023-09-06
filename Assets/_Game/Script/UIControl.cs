using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject arrow;
    public static UIControl Instance;
    public static List<GameObject> hints = new List<GameObject>();
    public List<Level> levels = new List<Level>();
    public TextMeshProUGUI tmp;
    Level current;
    public List<ParticleSystem> winEff;
    public int currentLevel;
    void Start()
    {
        Instance = this;
        currentLevel = 0;
        Load();
    }

    // Update is called once per frame
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

        Camera.main.orthographicSize = current.cameraDist;
    }
    public void CallHint()
    {
        foreach( var p in current.HintPosition())
        {
            GameObject g = Instantiate(arrow, p, Quaternion.identity);
            hints.Add(g);
            g.transform.DOLocalMoveX(g.transform.localPosition.x - 0.7f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }        
    }
    public static void HintOff()
    {
        if (hints.Count > 0)
        {
            foreach(var hint in hints)
            {

                hint.transform.DOKill();
                Destroy(hint.gameObject);
            }
            hints.Clear();
        }
    }
    public void OnWin()
    {
        foreach ( var ef in winEff)
        {
            ef.Play();
        }
        Invoke(nameof(NextLevel), 2f);
    }
    public void ShowAds()
    {

    }
 
    

}
