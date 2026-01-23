using UnityEngine;

public class TeammateController : MonoBehaviour
{
    public GameObject targetObject;
    public TeammateMove teammateMove;

    void Awake()
    {
        ApplyState();
    }

    void OnEnable()
    {
        ApplyState();
    }

    void Update()
    {
        ApplyState();
    }

    void ApplyState()
    {
        if (targetObject == null || teammateMove == null) return;

        bool shouldDisableMove = targetObject.activeSelf;

        if (teammateMove.enabled == shouldDisableMove)
        {
            teammateMove.enabled = !shouldDisableMove;
        }
    }
}
