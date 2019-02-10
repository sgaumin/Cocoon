using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
//using EZCameraShake;

// TO DO:
// - Add sound when seeing monster
// - Shake camera when is close to monster

public class PlayerVision : MonoBehaviour
{
    public float fearDistance;
    public Ennemy[] enemies;
    public float[] distancesWithEnemies;
    public  PostProcessVolume volume;

    private bool hasFearVision;

    Vignette vignetteLayer = null;

    private void Start()
    {
        volume.profile.TryGetSettings(out vignetteLayer);
        vignetteLayer.enabled.value = false;
    }

    void Update()
    {
        if (!hasFearVision)
            CalculateEnemyDistance();
    }

    void CalculateEnemyDistance()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            distancesWithEnemies[i] = Vector3.Distance(transform.position, enemies[i].transform.position);

            if (distancesWithEnemies[i] < fearDistance)
            {
                FearVision();
            }
        }
    }

    IEnumerator FearVision()
    {

        hasFearVision = true;

        // Vignette
        vignetteLayer.enabled.value = true;

        // Add red vision

        // Shake sometimes
        //StartCoroutine(FearShake());

        yield return new WaitForSeconds(5f);

        yield break;
    }

    //IEnumerator FearShake()
    //{
    //    CameraShaker.Instance.ShakeOnce(2f, 2f, 0.1f, 1f);
    //    yield return new WaitForSeconds(5f);

    //    hasFearVision = false;
    //    yield break;
    //}
}
