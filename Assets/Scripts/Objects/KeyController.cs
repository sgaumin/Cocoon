using UnityEngine;
using System.Collections;

public class KeyController : MonoBehaviour, ICanInteract
{
    public int keyIndex;
    public MeshRenderer keyRenderer;

    private PlayerAbilities playerAbilities;

    void Start()
    {
        playerAbilities = FindObjectOfType<PlayerAbilities>();
    }

    public IEnumerator Interaction()
    {
        if (playerAbilities.hasKey == false)
        {
            playerAbilities.keyIndex = keyIndex;
            playerAbilities.hasKey = true;

            // Deactivate key Mesh
            keyRenderer.enabled = false;

            // Play Key Sound
            AudioSource keyAudio = GetComponent<AudioSource>();
            keyAudio.Play();

            yield return new WaitForSeconds(1.5f);

            gameObject.SetActive(false);

            yield break;
        }
    }

    public void Description()
    {
        if (playerAbilities.hasKey == false)
            playerAbilities.abilityText.text = "Take this key.";
        if (playerAbilities.hasKey == true)
            playerAbilities.abilityText.text = "You can't hold an other key.";
    }
}
