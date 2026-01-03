using UnityEngine;

public class Close_GO : MonoBehaviour
{
public GameObject objectToClose;
public GameObject Canvas;
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        objectToClose.SetActive(false);
        Canvas.SetActive(true);
    }
}
}
