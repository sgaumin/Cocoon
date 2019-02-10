using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour, ICanInteract
{
    public bool lokedByKey;
    public int keyIndex;

    private PlayerAbilities playerAbilities;
    private bool isOpen;

    void Start()
    {
        playerAbilities = FindObjectOfType<PlayerAbilities>();
    }

    // Check if the door is Open or Close
    public void SetStatut()
    {
        isOpen = !isOpen;
    }

    public IEnumerator Interaction()
    {
        if (playerAbilities.canOpenDoor)
        {
            if (lokedByKey)
            {
                if (playerAbilities.hasKey && keyIndex == playerAbilities.keyIndex)
                {
                    if (playerAbilities.canOpenDoor)
                    {
                        lokedByKey = false;
                        playerAbilities.hasKey = false;
                        yield return InteractWithDoor();
                    }
                }
            }
            else
            {
                yield return InteractWithDoor();
            }

            yield return new WaitForSeconds(1.2f);

            // Can open other doors
            playerAbilities.canOpenDoor = true;
        }

        yield break;
    }

    private IEnumerator InteractWithDoor()
    {
        SetStatut();

        playerAbilities.canOpenDoor = false;

        // Launch the Animation of Closing or Opening
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetTrigger("Door");

        // Play the sound
        AudioSource doorAudio = gameObject.GetComponent<AudioSource>();
        doorAudio.Play();

        yield break;
    }

    public void Description()
    {
        if (playerAbilities.canOpenDoor)
        {
            if (lokedByKey)
            {
                playerAbilities.abilityText.text = "Key needed to unlock this door.";
            }
            else
            {
                if (isOpen)
                    playerAbilities.abilityText.text = "Close this door.";

                if (!isOpen)
                    playerAbilities.abilityText.text = "Open this door.";
            }
        }
    }
}
