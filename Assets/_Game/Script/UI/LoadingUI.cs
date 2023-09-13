using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Slider loadSlider;

    private float maxLoadTime = 1.0f;

    private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        loadSlider.value = 0.0f;
        elapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        SliderLoading();
        LoadSceneOnFinish();
    }

    void SliderLoading()
    {
        loadSlider.value = Mathf.Lerp(0, 1, elapsedTime / maxLoadTime);
        elapsedTime += Time.deltaTime;
    }

    void LoadSceneOnFinish()
    {
        if (loadSlider.value >= 1.0f)
        {
            Close();
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);

    }

    public void Close()
    {
        gameObject.SetActive(false);
        OnNext();


    }
    public void OnNext()
    {

        switch (GameManager.Instance.gameMode)
        {
            case GameMode.Normal:
                {
                    DataManager.Instance.SetNormalLevel(GameManager.Instance.currentLevel + 1);
                    GameManager.Instance.NextLevel();
                    break;
                }
            case GameMode.Boss:
                {
                    DataManager.Instance.SetBossLevel(GameManager.Instance.currentLevel + 1);
                    UIManager.Instance.OpenGameplay();
                    break;
                }
            case GameMode.Challenge:
                {
                    UIManager.Instance.OpenChallenge();
                    break;
                }

        }
    }
}
