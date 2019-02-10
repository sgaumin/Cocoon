using System.Collections;
using UnityEngine;
using TMPro;

public class HouseManager : MonoBehaviour
{
    [Header("Debug")]
    public bool isDegub;

    [Header("Timer Parameters")]
    public float timer;
    public int step;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI stepText;
    public Animator fadScreen;

    [Header("Audio")]
    public new AudioManager audio;

    private Collectible[] collectibles;

    void Start()
    {
        collectibles = FindObjectsOfType<Collectible>();

        foreach (Collectible collectible in collectibles)
        {
            if (PlayerPrefs.HasKey(collectible.objectType.ToString()))
            {
                if (PlayerPrefs.GetInt(collectible.objectType.ToString()) == 1)
                {
                    collectible.gameObject.SetActive(false);
                }
            }
        }

        if (!isDegub) {
            timerText.gameObject.SetActive(false);
            stepText.gameObject.SetActive(false);
        }

        // Hide Fad screen
        fadScreen.gameObject.SetActive(true);

        // Start Timer
        StartCoroutine(CountDown(timer));

        // Init UI
        stepText.text = "Step: " + step;
    }

    IEnumerator CountDown(float time)
    {
        float timeTemp = time;
        int stepTemp = step;

        while (stepTemp > 0)
        {
            while (timeTemp > 0f)
            {
                timeTemp -= Time.deltaTime;
                if (isDegub)
                    timerText.text = timeTemp.ToString("0.0");
                yield return null;
            }

            // Reset time temp
            timeTemp = time;

            // Update step number
            stepTemp--;
            if (isDegub)
                stepText.text = stepTemp.ToString("Step: " + stepTemp);

            // Play Bell Sound
            yield return StartCoroutine(audio.PlayBellSound(step - stepTemp));
        }

        // Reset GameStats value - No object kept
        GameStats.instance.objectTypeDetected = ObjectTypes.None;

        // Load Hide Out Scene
        yield return GameSystem.instance.FadOutAndLoadScene(Constants.Hideout);

        yield break;
    }
}
