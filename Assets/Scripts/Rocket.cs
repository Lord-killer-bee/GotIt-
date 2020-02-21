using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject successPanel;

    [SerializeField] private ParticleSystem burstParticles;
    [SerializeField] private ParticleSystem trailParticles;

    bool launchInitiated = false;

    private Vector3 startPosition = new Vector3(-1f, 0, 0);
    private Vector3 finalPosition = new Vector3(-1f, 45, 0);

    private void Update()
    {
        if (launchInitiated)
        {
            transform.position = Vector3.Lerp(transform.position, finalPosition, 0.01f);

            if(Vector3.Distance(transform.position, finalPosition) <= 0.05f)
            {
                transform.position = finalPosition;
                successPanel.SetActive(true);
            }
        }
    }

    public void BurstOnce()
    {
        burstParticles.Play();
    }

    public void EnableTrail()
    {
        trailParticles.Play();
    }

    public void LaunchRocket()
    {
        launchInitiated = true;
    }

    public void ResetRocket()
    {
        transform.position = startPosition;
        launchInitiated = false;
    }
}
