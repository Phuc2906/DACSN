using UnityEngine;

public class Clear : MonoBehaviour
{
        void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Đã xóa toàn bộ PlayerPrefs");
    }
}
