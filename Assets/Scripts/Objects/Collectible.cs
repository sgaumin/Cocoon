using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour, ICanInteract
{
    public ObjectTypes objectType;

    private PlayerAbilities playerAbilities;

    void Start()
    {
        playerAbilities = FindObjectOfType<PlayerAbilities>();
    }

    public IEnumerator Interaction()
    {
        if (GameStats.instance.objectTypeDetected == ObjectTypes.None)
        {
            GameStats.instance.objectTypeDetected = GetComponent<Collectible>().objectType;
            gameObject.SetActive(false);
        }

        yield return null;
    }

    public void Description()
    {
        if (GameStats.instance.objectTypeDetected == ObjectTypes.None)
            playerAbilities.abilityText.text = "Take this object.";
        else
            playerAbilities.abilityText.text = "You can't take an other objet.";
    }
}


