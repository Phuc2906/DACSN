using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("UI hiển thị coin trong game")]
    public TextMeshProUGUI coinText_Game;
    public TextMeshProUGUI coinText_GameWin;
    public TextMeshProUGUI coinText_GameOver;

    [Header("UI tổng coin (nếu có)")]
    public TextMeshProUGUI coinText_Total;

    [Header("Biến đã lưu (Key_01)")]
    public string keyLoaded;   

    [Header("Biến sẽ lưu (Key_02)")]
    public string keySaving;    

    [Header("Coin mặc định khi bắt đầu")]
    public int defaultCoin = 30;

    private int coin;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        InitCoin();
    }

    private void InitCoin()
    {
        coin = PlayerPrefs.GetInt(keyLoaded, defaultCoin);

        PlayerPrefs.SetInt(keySaving, coin);
        PlayerPrefs.Save();

        UpdateAllTexts();
    }

    public void AddCoin(int value)
    {
        coin += value;

        PlayerPrefs.SetInt(keySaving, coin);
        PlayerPrefs.Save();

        UpdateAllTexts();
    }

    public bool SpendCoin(int cost)
    {
        if (coin < cost) return false;

        coin -= cost;

        PlayerPrefs.SetInt(keySaving, coin);
        PlayerPrefs.Save();

        UpdateAllTexts();
        return true;
    }

    private void UpdateAllTexts()
    {
        string s = coin.ToString();

        if (coinText_Game != null) coinText_Game.text = s;
        if (coinText_GameWin != null) coinText_GameWin.text = s;
        if (coinText_GameOver != null) coinText_GameOver.text = s;
        if (coinText_Total != null) coinText_Total.text = s;
    }

    public int GetCoin() => coin;
}
