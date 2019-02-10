using System.Collections;
using UnityEngine;

public class Entrance : MonoBehaviour, ICanInteract
{
    [SerializeField] private GameModes entrance = GameModes.House;

    private BoxCollider boxCollider;

    private PlayerAbilities playerAbilities;

    void Start()
    {
        playerAbilities = FindObjectOfType<PlayerAbilities>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public IEnumerator Interaction()
    {
        boxCollider.enabled = false;

        if (entrance == GameModes.House)
            yield return GameSystem.instance.FadOutAndLoadScene(Constants.House);

        if (entrance == GameModes.Hideout)
            yield return GameSystem.instance.FadOutAndLoadScene(Constants.Hideout);

        yield break;
    }

    public void Description()
    {
        if (entrance == GameModes.House)
            playerAbilities.abilityText.text = "Leave the Hideout.";

        if (entrance == GameModes.Hideout)
            playerAbilities.abilityText.text = "Enter in the Hideout.";
    }
}
