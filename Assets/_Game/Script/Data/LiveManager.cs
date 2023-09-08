using UnityEngine;
public class LiveManager : MonoBehaviour
{
    public static LiveManager Instance { get; set; }
    
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
    
    [Button]
    public void AddCoin(int amount)
    {
        GameManager.Instance.Coin += amount;
    }

    public void MinusCoin(int amount)
    {
        GameManager.Instance.Coin -= amount;
    }
   
}
