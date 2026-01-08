using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_06 : MonoBehaviour
{
    public Button buyButton;
    public Image buttonImage;
    public TMP_Text buttonText;

    public Color normalColor = Color.yellow;
    public Color boughtColor = Color.gray;

    private bool isBought = false;
    private string playerPrefKey = "Item_06_Bought";

    private void Start()
    {
        isBought = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;

        buyButton.onClick.AddListener(OnBuyButtonClicked);
        UpdateButtonUI();
    }

    private void OnBuyButtonClicked()
    {
        
        if (isBought)
        {
            return;
        }
        
        isBought = true;
        
        PlayerPrefs.SetInt(playerPrefKey, 1);
        PlayerPrefs.Save();
        
        int verify = PlayerPrefs.GetInt(playerPrefKey, -1);
        UpdateButtonUI();
    }

    private void UpdateButtonUI()
    {
        if (isBought)
        {
            buttonImage.color = boughtColor;
            buttonText.text = "Đã nhận";
            buyButton.interactable = false;
        }
        else
        {
            buttonImage.color = normalColor;
            buttonText.text = "Nhận";
            buyButton.interactable = true;
        }
    }
}