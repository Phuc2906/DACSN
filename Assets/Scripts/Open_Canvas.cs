using UnityEngine;

public class Open_Canvas : MonoBehaviour
{
    [Header("Drag GameObject cần theo dõi (khi destroy → bật Canvas)")]
    [SerializeField] private GameObject targetObj; 

    [Header("Canvas sẽ bật khi targetObj destroy")]
    [SerializeField] private GameObject canvas;    

    private bool hasTriggered = false; 

    private void Update()
    {
        if (targetObj == null && !hasTriggered && canvas != null)
        {
            canvas.SetActive(true);
            hasTriggered = true;
            this.enabled = false;
        }
    }

    [ContextMenu("Reset State")]
    private void ResetState()
    {
        hasTriggered = false;
        this.enabled = true;
        if (canvas != null) canvas.SetActive(false);
    }
}