using UnityEngine;
using TMPro;

public class RubyManager : MonoBehaviour
{
    public static RubyManager Instance;

    [Header("UI hiển thị ruby trong game")]
    public TextMeshProUGUI rubyText_Game;
    public TextMeshProUGUI rubyText_GameWin;
    public TextMeshProUGUI rubyText_GameOver;

    [Header("UI tổng ruby (nếu có)")]
    public TextMeshProUGUI rubyText_Total;

    private int ruby = 0;

    private const string RUBY_KEY = "Ruby";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ruby = PlayerPrefs.GetInt(RUBY_KEY, 0);

        UpdateAllTexts();
    }

    public void AddRuby(int value)
    {
        ruby += value;

        PlayerPrefs.SetInt(RUBY_KEY, ruby);
        PlayerPrefs.Save();

        UpdateAllTexts();
    }

    public bool SpendRuby(int cost)
    {
        if (ruby >= cost)
        {
            ruby -= cost;

            PlayerPrefs.SetInt(RUBY_KEY, ruby);
            PlayerPrefs.Save();

            UpdateAllTexts();
            return true;
        }
        return false;
    }

    private void UpdateAllTexts()
    {
        string s = ruby.ToString();

        if (rubyText_Game != null) rubyText_Game.text = s;
        if (rubyText_GameWin != null) rubyText_GameWin.text = s;
        if (rubyText_GameOver != null) rubyText_GameOver.text = s;
        if (rubyText_Total != null) rubyText_Total.text = s;
    }

    public int GetRuby() => ruby;
}
