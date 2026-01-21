using UnityEngine;

public class UI : MonoBehaviour
{
    Vector3 startScale;

    void Awake()
    {
        startScale = transform.localScale;
    }

    void LateUpdate()
    {
        transform.localScale = startScale;
    }
}
