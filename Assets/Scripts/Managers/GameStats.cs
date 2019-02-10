using UnityEngine;

public class GameStats : MonoBehaviour
{
    // Singleton
    public static GameStats instance;

    public ObjectTypes objectTypeDetected;

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

    public void Reset()
    {
        objectTypeDetected = ObjectTypes.None;
    }
}
