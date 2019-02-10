using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject abilityText;

    private AudioSource[] audioSources;
    private float[] stopedAtTime;

    private void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        audioSources = FindObjectsOfType<AudioSource>();

        stopedAtTime = new float[audioSources.Length];

        if (FindObjectOfType<PlayerInputs>())
            FindObjectOfType<PlayerInputs>().OnPause += PauseGame;
    }

    public void PauseGame()
    {
        bool abilityTextStatus;
        abilityTextStatus = abilityText.activeSelf;

        // Game is Playing
        if (GameSystem.instance.gameState == GameSystem.gameStates.Playing)
        {
            GameSystem.instance.gameState = GameSystem.gameStates.Pause;

            abilityText.SetActive(false);

            pauseMenu.SetActive(true);

            StopAudios(true);

            Time.timeScale = 0f;
        }

        // Game is Pausing
        else if (GameSystem.instance.gameState == GameSystem.gameStates.Pause)
        {
            GameSystem.instance.gameState = GameSystem.gameStates.Playing;

            Time.timeScale = 1f;

            abilityText.SetActive(abilityTextStatus);

            pauseMenu.SetActive(false);

            StopAudios(false);
        }
    }

    private void StopAudios(bool stop)
    {
        int compteur = 0;

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null)
            {
                if (stop)
                {
                    stopedAtTime[compteur] = audioSource.time;
                    audioSource.enabled = false;
                }
                else
                {
                    audioSource.time = stopedAtTime[compteur];
                    audioSource.enabled = true;
                }
            }

            compteur++;
        }
    }
}
