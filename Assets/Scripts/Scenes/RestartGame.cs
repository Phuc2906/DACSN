using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [Header("Tổng số coin / reward ban đầu")]
    public int totalCoins = 48;

    [Header("Tổng số box ban đầu")]
    public int totalBoxes = 48;

    [Header("Tổng số ItemHealth ban đầu")]
    public int totalHealthItems = 48;

    [Header("Tổng số Enemy ban đầu")]
    public int totalEnemies = 20;

    public void RestartCurrentScene()
    {
        for (int i = 0; i < totalCoins; i++)
        {
            PlayerPrefs.DeleteKey("Coin_" + i);
        }

        for (int i = 0; i < totalBoxes; i++)
        {
            PlayerPrefs.DeleteKey("Box_" + i);
        }

        for (int i = 0; i < totalHealthItems; i++)
        {
            PlayerPrefs.DeleteKey("ItemHealth_" + i);
        }

        for (int i = 0; i < totalEnemies; i++)
        {
            PlayerPrefs.DeleteKey("Enemy_" + i);

            PlayerPrefs.DeleteKey("Enemy_X_" + i);
            PlayerPrefs.DeleteKey("Enemy_Y_" + i);
            PlayerPrefs.DeleteKey("Enemy_Facing_" + i);
            PlayerPrefs.DeleteKey("EnemyHealth_" + i);
            PlayerPrefs.DeleteKey("EnemyDead_" + i);
        }

        PlayerPrefs.DeleteKey("PlayerHealth");

        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerFacing");

        PlayerPrefs.DeleteKey("NPC_A_Destroyed");
        PlayerPrefs.DeleteKey("Mission_A_Destroyed");
        PlayerPrefs.DeleteKey("Toggle_Unlocked");

        PlayerPrefs.DeleteKey("TeammateDead");
        PlayerPrefs.DeleteKey("TeammateHealth");

        PlayerPrefs.DeleteKey("Potion_01");
        PlayerPrefs.DeleteKey("Potion_02");

        PlayerPrefs.DeleteKey("Coin");


        PlayerPrefs.Save();

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
