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
        Debug.Log("[Item_06] Start() được gọi");
        
        // Load trạng thái đã mua
        isBought = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;
        Debug.Log($"[Item_06] Load PlayerPrefs: {playerPrefKey} = {PlayerPrefs.GetInt(playerPrefKey, 0)}");
        Debug.Log($"[Item_06] isBought = {isBought}");

        buyButton.onClick.AddListener(OnBuyButtonClicked);
        UpdateButtonUI();
    }

    private void OnBuyButtonClicked()
    {
        Debug.Log("[Item_06] Button được click!");
        
        if (isBought)
        {
            Debug.LogWarning("[Item_06] Đã mua rồi, không thể mua lại!");
            return;
        }

        Debug.Log("[Item_06] ✓ Mua miễn phí! Đang lưu...");
        
        isBought = true;
        
        // LƯU VÀO PLAYERPREFS
        PlayerPrefs.SetInt(playerPrefKey, 1);
        PlayerPrefs.Save();
        
        // Verify đã lưu thành công
        int verify = PlayerPrefs.GetInt(playerPrefKey, -1);
        Debug.Log($"[Item_06] ✓ Đã lưu PlayerPrefs! Verify: {playerPrefKey} = {verify}");
        
        UpdateButtonUI();
    }

    private void UpdateButtonUI()
    {
        if (isBought)
        {
            buttonImage.color = boughtColor;
            buttonText.text = "Bought";
            buyButton.interactable = false;
            Debug.Log("[Item_06] UI cập nhật: BOUGHT");
        }
        else
        {
            buttonImage.color = normalColor;
            buttonText.text = "Buy";
            buyButton.interactable = true;
            Debug.Log("[Item_06] UI cập nhật: Buy (Free)");
        }
    }
}