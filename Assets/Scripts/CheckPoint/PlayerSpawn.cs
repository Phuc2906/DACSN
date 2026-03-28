using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Awake()
    {
        
        if (PlayerPrefs.HasKey("SavedX") &&
            PlayerPrefs.HasKey("SavedY") &&
            PlayerPrefs.HasKey("SavedZ"))
        {
            float x = PlayerPrefs.GetFloat("SavedX");
            float y = PlayerPrefs.GetFloat("SavedY");
            float z = PlayerPrefs.GetFloat("SavedZ");

            transform.position = new Vector3(x, y, z);
        }
    }
}
