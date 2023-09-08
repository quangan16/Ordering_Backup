using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeUI : MonoBehaviour,IUIControl
{

    [SerializeField] List<Level> levels;
    [SerializeField] ChallegeItemAnimation challengeItem;
    [SerializeField] GameObject layout;
    public TextMeshProUGUI coin;

    private void Start()
    {
        coin.text = PlayerPrefs.GetInt("coin", 0).ToString();
        for (int i = 0; i< levels.Count;i++)
        {
            int j = i;
            ChallegeItemAnimation challenge = Instantiate(challengeItem,layout.transform);
            challenge.playButton.onClick.AddListener(
                () => OpenLevel(j)) ;
            challenge.SetData((i+1).ToString());
        }
    }
    public void BackBtn()
    {
        UIManager.Instance.OpenGameplay();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void OpenLevel(int level)
    {

        UIManager.Instance.OpenChallengeGameplay(level);
        Close();
    }


}
