using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static GameModes gameMode;

    void Awake()
    {
        Reset();
    }
    
    // Indicate the actual level
    private void Reset()
    {
        if (SceneManager.GetActiveScene().name == Constants.Hideout)
            gameMode = GameModes.Hideout;

        if (SceneManager.GetActiveScene().name == Constants.House)
            gameMode = GameModes.House;
    }
}
