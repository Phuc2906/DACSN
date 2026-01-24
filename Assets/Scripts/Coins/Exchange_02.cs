using UnityEngine;
using TMPro;

public class Exchange_02 : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputCoin;

    [Header("Canvas")]
    public GameObject exchangeCanvas;
    public GameObject warningCanvas;

    public void ExchangeByInput()
    {
        if (inputCoin == null) return;

        if (string.IsNullOrEmpty(inputCoin.text))
        {
            ShowWarning();
            return;
        }

        int value;
        if (!int.TryParse(inputCoin.text, out value) || value <= 0)
        {
            ShowWarning();
            return;
        }

        int currentCoin = CoinManager.Instance.GetCoin();

        if (value > currentCoin)
        {
            ShowWarning();
            return;
        }

        CoinManager.Instance.SpendCoin(value);
        RubyManager.Instance.AddRuby(value);

        CloseExchangeCanvas();
    }

    public void ExchangeAllCoin()
    {
        int currentCoin = CoinManager.Instance.GetCoin();

        if (currentCoin <= 0)
        {
            ShowWarning();
            return;
        }

        CoinManager.Instance.SpendCoin(currentCoin);
        RubyManager.Instance.AddRuby(currentCoin);

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
