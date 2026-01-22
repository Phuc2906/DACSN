using UnityEngine;
using System.Collections.Generic;

public class Warning : MonoBehaviour
{
    [Header("List GameObjects")]
    public List<GameObject> targetObjects = new List<GameObject>();

    [Header("Tag")]
    public string triggerTag = "Player";

    private bool hasOpened = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasOpened) return;
        if (!other.CompareTag(triggerTag)) return;

        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        hasOpened = true;
    }
}
