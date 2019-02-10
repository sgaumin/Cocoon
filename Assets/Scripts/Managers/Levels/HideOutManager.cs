using UnityEngine;

public class HideOutManager : MonoBehaviour
{
    private Collectible[] collectibles;

    void Start()
    {
        collectibles = FindObjectsOfType<Collectible>();

        // If object kept - check the object associate in the scene & activate all other objects kept
        int compteur = 0;
        foreach (Collectible collectible in collectibles)
        {
            if (collectible.objectType == GameStats.instance.objectTypeDetected)
            {
                collectible.GetComponent<MeshRenderer>().enabled = true;
                PlayerPrefs.SetInt(collectible.objectType.ToString(), 1);
                compteur++;
                continue;
            }

            if (PlayerPrefs.HasKey(collectible.objectType.ToString()))
                if (PlayerPrefs.GetInt(collectible.objectType.ToString()) == 1)
                {
                    collectible.GetComponent<MeshRenderer>().enabled = true;
                    compteur++;
                }
        }

        // If all objects collected
        if (compteur == collectibles.Length)
        {
            GameSystem.instance.LoadSceneByName("Win");
        }

        // Reset collectible value
        GameStats.instance.Reset();
    }
}
