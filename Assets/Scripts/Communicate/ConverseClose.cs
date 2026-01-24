using UnityEngine;

public class ConverseClose : MonoBehaviour
{
    public GameObject canvasToClose; 
    public Converse converseScript;    

    public void CanvasOnClick()
    {
        if (canvasToClose != null)
            canvasToClose.SetActive(false);

        if (converseScript != null)
            converseScript.StopInteract();
    }
}
