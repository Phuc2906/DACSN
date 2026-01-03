using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    public GameObject notEnoughLevelCanvas;   // Canvas hiện khi chưa đủ level
    public int requiredLevel = 10;            // Level cần để qua map

    private PlayerExpManager playerExp;

    private void Start()
    {
        if (notEnoughLevelCanvas != null)
            notEnoughLevelCanvas.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerExp = collision.gameObject.GetComponent<PlayerExpManager>();

            if (playerExp == null)
            {
                Debug.LogError("Không tìm thấy PlayerExpManager trên Player!");
                return;
            }

            if (playerExp.GetLevel() >= requiredLevel)
            {
                // Đủ level → qua màn
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                // Không đủ level → bật canvas
                if (notEnoughLevelCanvas != null)
                    notEnoughLevelCanvas.SetActive(true);
            }
        }
    }
}
