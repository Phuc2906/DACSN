using UnityEngine;

public class OpenGameObject : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (targetObject != null)
                targetObject.SetActive(true); 
        }
    }
}
