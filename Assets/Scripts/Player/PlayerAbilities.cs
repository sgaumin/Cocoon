using UnityEngine;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    public TextMeshProUGUI abilityText;

    [Header("Key properties")]
    [HideInInspector] public bool hasKey;   
    [HideInInspector] public int keyIndex;

    [Header("Door property")]
    [HideInInspector] public bool canOpenDoor;

    private void Start()
    {
        abilityText.gameObject.SetActive(false);

        canOpenDoor = true;

        keyIndex = -1;
    }

    public void ShowAbilityText(bool show)
    {
        if (show)
            abilityText.gameObject.SetActive(true);
        else
            abilityText.gameObject.SetActive(false);
    }
}
