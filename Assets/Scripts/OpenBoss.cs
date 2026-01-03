using UnityEngine;

public class OpenBoss : MonoBehaviour
{
    [Header("Kéo 3 object vào đây theo thứ tự")]
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private GameObject obj3;

    private void Update()
    {
        if (obj1 == null && obj2 != null && !obj2.activeSelf)
        {
            obj2.SetActive(true);
        }
        
        if (obj2 == null && obj3 != null && !obj3.activeSelf)
        {
            obj3.SetActive(true);
        }
        
        if (obj3 != null && obj3.activeSelf)
        {
            this.enabled = false; 
        }
    }

    [ContextMenu("Reset State")]
    private void ResetState()
    {
        if (obj1 != null) obj1.SetActive(true);
        if (obj2 != null) obj2.SetActive(false);
        if (obj3 != null) obj3.SetActive(false);
        this.enabled = true;
 }
}