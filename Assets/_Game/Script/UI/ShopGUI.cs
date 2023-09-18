using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PageState
{
    SKIN,
    BACKGROUND
}

public class ShopGUI : MonoBehaviour, IUIControl
{
    public static ShopGUI Instance;

    public static PageState currentPage;


    [SerializeField] private Image skinButtonImg;

    [SerializeField] private Image backgroundButtonImg;

    // Start is called before the first frame update
    [SerializeField] private GameObject skinPage;
    [SerializeField] private GameObject backgroundPage;

    [SerializeField] private Color buttonActiveColor;
    [SerializeField] private Color buttonInactiveColor;
    [SerializeField] private Color textActiveColor;
    [SerializeField] private Color textInactiveColor;

    [SerializeField] private TextMeshProUGUI skinTxt;
    [SerializeField] private TextMeshProUGUI backgroundTxt;

    [SerializeField] private Image LightSFX;

    void OnEnable()
    {
        LightEffect();
    }

    void OnDisable()
    {
        DOTween.KillAll();
    }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
        currentPage = PageState.SKIN;
    }


    public void OnSkinPageSelect()
    {
        skinButtonImg.color = buttonActiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        skinPage.gameObject.SetActive(true);
        backgroundPage.gameObject.SetActive(false);
        skinTxt.color = textActiveColor;
        backgroundTxt.color = textInactiveColor;
        currentPage = PageState.SKIN;
    }

    public void OnBackgroundPageSelect()
    {
        skinButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonInactiveColor;
        backgroundButtonImg.gameObject.GetComponentInChildren<Image>().color = buttonActiveColor;
        skinPage.gameObject.SetActive(false);
        backgroundPage.gameObject.SetActive(true);
        skinTxt.color = textInactiveColor;
        backgroundTxt.color = textActiveColor;
        currentPage = PageState.BACKGROUND;
    }

    public void LightEffect()
    {
        LightSFX.transform.DOScale(Vector3.one * 0.5f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        LightSFX.transform.DORotate(Vector3.forward * 360.0f, 4.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void BackBtn()
    {
        UIManager.Instance.OpenMain();
    }
}