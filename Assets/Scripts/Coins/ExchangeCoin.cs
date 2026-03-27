using UnityEngine;
using TMPro;

public class ExchangeCoin : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputRuby;
    public GameObject exchangeCanvas;
    public GameObject warningCanvas; 

    [Header("Rate")]
    public int rubyPerCoin = 5; 

    public void ExchangeByInput()
    {
        if (inputRuby == null) return;

        if (string.IsNullOrEmpty(inputRuby.text))
        {
            ShowWarning();
            return;
        }

        int value;
        if (!int.TryParse(inputRuby.text, out value))
        {
            ShowWarning();
            return;
        }

        if (value <= 0)
        {
            ShowWarning();
            return;
        }

        int currentRuby = RubyManager.Instance.GetRuby();

        if (value < rubyPerCoin)
        {
            ShowWarning();
            return;
        }

        if (currentRuby < value)
        {
            ShowWarning();
            return;
        }

        int coinReceive = value / rubyPerCoin;
        int rubyCost = coinReceive * rubyPerCoin;

        RubyManager.Instance.SpendRuby(rubyCost);
        CoinManager.Instance.AddCoin(coinReceive);

        if (exchangeCanvas != null)
            exchangeCanvas.SetActive(false);

        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }

    public void ExchangeAllRuby()
    {
        int currentRuby = RubyManager.Instance.GetRuby();

        if (currentRuby < rubyPerCoin)
        {
            ShowWarning();
            return;
        }

        int coinReceive = currentRuby / rubyPerCoin;
        int rubyCost = coinReceive * rubyPerCoin;

        RubyManager.Instance.SpendRuby(rubyCost);
        CoinManager.Instance.AddCoin(coinReceive);

        if (exchangeCanvas != null)
            exchangeCanvas.SetActive(false);

        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }

    private void ShowWarning()
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(true);
    }
}