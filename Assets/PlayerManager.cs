using UnityEngine;
using Unity.Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] players;
    public CinemachineCamera cam;

    void Awake()
    {
        int id = PlayerPrefs.GetInt("SelectedPlayerID", 0);

        for (int i = 0; i < players.Length; i++)
        {
            bool active = (i == id);
            players[i].SetActive(active);

            if (active)
            {
                cam.Follow = players[i].transform;
            }
        }
    }
}
