using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    // Singleton
    public static GameSystem instance = null;

    // State of the Game
    public enum gameStates { Playing, Pause, End };
    public gameStates gameState = gameStates.Playing;

    // UI
    public Animator fadScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        // Hide Cursor
        //Cursor.visible = false;

        // DontDestroyOnLoad(gameObject);
    }

    // Allow to load a scene by its name
    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    // Allow to load a scene by its name
    public void LoadSceneByIndex(int buildIndexNumber)
    {
        SceneManager.LoadScene(buildIndexNumber);
    }

    // Relaod the actual scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //  Play the next scene present in the build
    public void PlayNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Load Menu Scene
    public void LoadMenu()
    {
        LoadSceneByName(Constants.MenuName);
    }

    // Load Credits Scene
    public void LoadCredits()
    {
        LoadSceneByName(Constants.CreditsName);
    }

    // Load Controls Scene
    public void LoadControls()
    {
        LoadSceneByName(Constants.ControlsName);
    }

    // Reset PlayerPrefs Values
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public IEnumerator FadOutAndLoadScene(string nameScene)
    {

        fadScreen.SetTrigger("Fad");

        yield return new WaitForSeconds(2f);
        LoadSceneByName(nameScene);

        yield break;
    }

    // Quit the game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
