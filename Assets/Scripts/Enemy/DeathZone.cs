using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            StartCoroutine(EndGameStep());
        }
    }

    IEnumerator EndGameStep() {
        GameSystem.instance.gameState = GameSystem.gameStates.End;

        yield return GameSystem.instance.FadOutAndLoadScene("Game Over");

        yield break;
    }
}
