using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject OB1;
    public GameObject OB2;

    private bool started = false;

    void Update()
    {
        if (enemy.activeSelf && !started)
        {
            started = true;
            StartCoroutine(SpawnSequence());
        }
    }

    IEnumerator SpawnSequence()
    {
        yield return new WaitForSeconds(5f);  
        OB1.SetActive(true);

        yield return new WaitForSeconds(2f);  
        OB1.SetActive(false);


        yield return new WaitForSeconds(5f);  
        OB2.SetActive(true);

        yield return new WaitForSeconds(2f);  
        OB2.SetActive(false);
    }
}
