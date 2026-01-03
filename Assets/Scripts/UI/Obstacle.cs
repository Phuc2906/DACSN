using UnityEngine;


public class Obstacle : MonoBehaviour
{
    public GameObject gameoverCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameoverCanvas.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
}
