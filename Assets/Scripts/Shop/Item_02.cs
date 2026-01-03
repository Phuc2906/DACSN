using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_02 : MonoBehaviour
{
    [Header("UI Reference")]
    public Button buyButton;
    public Image buttonImage;
    public TMP_Text buttonText;

    [Header("Notice")]
    public Canvas noticeCanvas;

    [Header("Item Properties")]
    public int price = 2;
    public Color normalColor = Color.yellow;
    public Color boughtColor = Color.gray;

    private bool isBought = false;
    private string playerPrefKey = "Item_02_Bought";

    private void Start()
    {
        Debug.Log("[Item_02] Start() được gọi");

        // Load trạng thái đã mua
        isBought = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;
        Debug.Log($"[Item_02] Load PlayerPrefs: {playerPrefKey} = {PlayerPrefs.GetInt(playerPrefKey, 0)}");
        Debug.Log($"[Item_02] isBought = {isBought}");

        // Check UI null
        if (buyButton == null) Debug.LogError("[Item_02] buyButton chưa gán!");
        if (buttonImage == null) Debug.LogError("[Item_02] buttonImage chưa gán!");
        if (buttonText == null) Debug.LogError("[Item_02] buttonText chưa gán!");

        buyButton.onClick.AddListener(OnBuyButtonClicked);

        UpdateButtonUI();
    }

    private void OnBuyButtonClicked()
    {
        Debug.Log("[Item_02] Button được click!");

        if (isBought)
        {
            Debug.LogWarning("[Item_02] Đã mua rồi, không thể mua lại!");
            return;
        }

        if (CoinManager.Instance == null)
        {
            Debug.LogError("[Item_02] CoinManager.Instance = NULL! Không thể trừ coin.");
            return;
        }

        Debug.Log($"[Item_02] Thử mua với giá {price} coin...");

        if (CoinManager.Instance.SpendCoin(price))
        {
            Debug.Log("[Item_02] ✓ Đủ tiền! Đang lưu...");
            isBought = true;

            // Lưu trạng thái mua
            PlayerPrefs.SetInt(playerPrefKey, 1);
            PlayerPrefs.Save();

            int verify = PlayerPrefs.GetInt(playerPrefKey, -1);
            Debug.Log($"[Item_02] ✓ Đã lưu PlayerPrefs! Verify: {playerPrefKey} = {verify}");

            UpdateButtonUI();
        }
        else
        {
            Debug.LogWarning("[Item_02] ❌ Không đủ tiền!");
            if (noticeCanvas != null)
                noticeCanvas.gameObject.SetActive(true);
        }
    }

    private void UpdateButtonUI()
    {
        if (isBought)
        {
            buttonImage.color = boughtColor;
            buttonText.text = "Bought";
            buyButton.interactable = false;
            Debug.Log("[Item_02] UI cập nhật: BOUGHT");
        }
        else
        {
            buttonImage.color = normalColor;
            buttonText.text = price.ToString();
            buyButton.interactable = true;
            Debug.Log($"[Item_02] UI cập nhật: {price} coin");
        }
    }
}
