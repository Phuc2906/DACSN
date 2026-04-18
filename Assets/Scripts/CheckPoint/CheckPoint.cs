using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject checkpointCanvas;

    public string checkpointKey = "Checkpoint"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt(checkpointKey, 0) == 0)
        {
            PlayerPrefs.SetInt(checkpointKey, 1);

            if (checkpointCanvas != null)
                checkpointCanvas.SetActive(true);

            PlayerPrefs.SetFloat("SavedX", other.transform.position.x);
            PlayerPrefs.SetFloat("SavedY", other.transform.position.y);
            PlayerPrefs.SetFloat("SavedZ", other.transform.position.z);
            PlayerPrefs.Save();
        }
    }
}