using System.Collections;
using UnityEngine;

public class CineManager : MonoBehaviour
{
    public Animator cineCamera1;
    public Animator cineCamera2;
    public Animator fadScreen;
    public AudioManager audioManager;

    void Start()
    {
        fadScreen.gameObject.SetActive(false);
        cineCamera1.gameObject.SetActive(false);
        cineCamera2.gameObject.SetActive(false);

        StartCoroutine(StartCine());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            StartCoroutine(GameSystem.instance.FadOutAndLoadScene(Constants.Hideout));
    }


    IEnumerator StartCine() {

        yield return new WaitForEndOfFrame();

        // PLay Bell Sounds
        StartCoroutine(audioManager.PlayBellSound(6));

        // Activate cinematic objects
        fadScreen.gameObject.SetActive(true);
        cineCamera1.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f);

        // Fad In
        fadScreen.SetTrigger("Fad");
        yield return new WaitForSeconds(1f);
        cineCamera1.gameObject.SetActive(false);

        // Transition
        cineCamera2.gameObject.SetActive(true);
        fadScreen.SetTrigger("FadOut");
        cineCamera2.SetTrigger("Cam");
        yield return new WaitForSeconds(4f);

        // fad Out
        fadScreen.SetTrigger("Fad");

        yield return new WaitForSeconds(2f);
        GameSystem.instance.PlayNextScene();

        yield break;
    }
}
