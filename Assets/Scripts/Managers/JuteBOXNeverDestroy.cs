using UnityEngine;
using UnityEngine.SceneManagement;

public class JuteBOXNeverDestroy : MonoBehaviour
{
    public static JuteBOXNeverDestroy instance;

    [SerializeField]
    private AudioClip sonMenu;
    [SerializeField]
    private AudioClip sonJeu;
    [SerializeField]
    private string sceneactive;

    private AudioSource MyAudio;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MyAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        sceneactive = SceneManager.GetActiveScene().name;

        if (sceneactive == "1-Intro_Cine") { MyAudio.volume -= 0.01f; }

        if (sceneactive == Constants.Hideout || sceneactive == Constants.MenuName)
        {
            if (MyAudio.clip.name != "Menu")
            {
                if (MyAudio.volume > 0.1f) { MyAudio.volume -= 0.01f; }
                if (MyAudio.volume <= 0.1f) { MyAudio.clip = sonMenu; MyAudio.Play(0); }

            }
        }

        if (sceneactive == Constants.Hideout || sceneactive == Constants.MenuName)
        {
            if (MyAudio.clip.name == "Menu" && MyAudio.volume < 0.4)
            {
                MyAudio.volume += 0.01f;
            }
        }

        if (sceneactive == Constants.House && MyAudio.clip.name != "Jeu")
        {
            if (MyAudio.volume > 0.1f) { MyAudio.volume -= 0.01f; }
            if (MyAudio.volume <= 0.1f) { MyAudio.clip = sonJeu; MyAudio.Play(0); }

        }

        if (sceneactive == Constants.House && MyAudio.clip.name == "Jeu" && MyAudio.volume < 0.2)
        {
            MyAudio.volume += 0.01f;
        }
    }
}
