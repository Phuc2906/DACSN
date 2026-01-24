using UnityEngine;
using TMPro;

public class Exchange : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputRuby;

    [Header("Canvas")]
    public GameObject exchangeCanvas;
    public GameObject warningCanvas;

    public void ExchangeByInput()
    {
        if (inputRuby == null) return;

        if (string.IsNullOrEmpty(inputRuby.text))
        {
            ShowWarning();
            return;
        }

        int value;
        if (!int.TryParse(inputRuby.text, out value) || value <= 0)
        {
            ShowWarning();
            return;
        }

        int currentRuby = RubyManager.Instance.GetRuby();

        if (value > currentRuby)
        {
            ShowWarning();
            return;
        }

        RubyManager.Instance.SpendRuby(value);
        CoinManager.Instance.AddCoin(value);

        CloseExchangeCanvas();
    }

    public void ExchangeAllRuby()
    {
        int currentRuby = RubyManager.Instance.GetRuby();

        if (currentRuby <= 0)
        {
            ShowWarning();
            return;
        }

        RubyManager.Instance.SpendRuby(currentRuby);
        CoinManager.Instance.AddCoin(currentRuby);

        CloseExchangeCanvas();
    }

    private void ShowWarning()
    {
        if (warningCanvas != null)
            warningCanvas.SetActive(true);
    }

    private void CloseExchangeCanvas()
    {
        if (exchangeCanvas != null)
            exchangeCanvas.SetActive(false);

        if (warningCanvas != null)
            warningCanvas.SetActive(false);
    }
}
