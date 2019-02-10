using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameStats gameStats;

    void Start()
    {
        gameStats = FindObjectOfType<GameStats>();

        // Reset Time scale
        Time.timeScale = 1f;

        if (gameStats != null)
            gameStats.Reset();
    }
}
