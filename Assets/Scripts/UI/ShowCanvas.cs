using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    [SerializeField] private GameObject inputCanvas;
    [SerializeField] private GameObject interactCanvas;

    private bool daBatInteract = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (inputCanvas != null && !inputCanvas.activeSelf)
                inputCanvas.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (inputCanvas != null && inputCanvas.activeSelf)
                inputCanvas.SetActive(false);

            if (!daBatInteract && interactCanvas != null && !interactCanvas.activeSelf)
            {
                interactCanvas.SetActive(true);
                daBatInteract = true;
            }
        }
    }

    public void ResetInteract()
    {
        daBatInteract = false;
    }
}
