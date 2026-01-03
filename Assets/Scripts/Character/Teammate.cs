using UnityEngine;

public class Teammate : MonoBehaviour
{
    [SerializeField] private Transform player;      
    [SerializeField] private GameObject Canvas_01;   
    [SerializeField] private float showDistance = 3f; 

    void Update()
    {
        if (player == null || Canvas_01 == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= showDistance)
        {
            Canvas_01.SetActive(true);   
        }
        else
        {
            Canvas_01.SetActive(false);  
        }
    }
}
