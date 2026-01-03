using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject canvasA;
    [SerializeField] private GameObject canvasB;
    [SerializeField] private Teammate teammateScript; 

    public void SwitchCanvas()
    {
        if(canvasB != null)
            canvasB.SetActive(true); 

        if(canvasA != null)
            canvasA.SetActive(false); 

        if(teammateScript != null)
            teammateScript.enabled = false; 
    }
}
