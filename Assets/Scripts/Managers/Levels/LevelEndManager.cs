using UnityEngine;
using System.Collections;

public class LevelEndManager : MonoBehaviour
{
    public Animator fadScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndLevel());
    }

    IEnumerator EndLevel() {

        fadScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        fadScreen.SetTrigger("Fad");

        yield return new WaitForSeconds(1.5f);

        GameSystem.instance.LoadMenu();

        yield break;
    }
}
