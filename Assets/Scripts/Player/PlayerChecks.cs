using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    public float interactionsDistance;

    private PlayerInputs playerInputs;
    private PlayerAbilities playerAbilities;
    private Camera cam;
    private RaycastHit hit;
    private float interactionsDistanceTemp;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        playerInputs = GetComponent<PlayerInputs>();
        playerAbilities = GetComponent<PlayerAbilities>();

        // Set the interaction distance according to the level
        if (PlayerController.gameMode == GameModes.Hideout)
            interactionsDistanceTemp = Mathf.Infinity;
        else
            interactionsDistanceTemp = interactionsDistance;
    }

    void FixedUpdate()
    {
        // Raycasting without colliding with triggers
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, interactionsDistanceTemp, -5, QueryTriggerInteraction.Ignore))
        {
            // Looking for Interactions objects, which have ICanInteract Interface
            ICanInteract interactibleObject = hit.collider.GetComponent<ICanInteract>();

            // Activate the Ability Text
            playerAbilities.ShowAbilityText(true);

            if (interactibleObject != null)
            {
                // Update
                interactibleObject.Description();

                if (playerInputs.Interactions)
                    // Launch the interaction behavior of the object
                    StartCoroutine(interactibleObject.Interaction());
            }
            else
                // Deactivate the Ability Text
                playerAbilities.ShowAbilityText(false);
        }
        else
        {
            // Deactivate the Ability Text
            playerAbilities.ShowAbilityText(false);
        }
    }
}