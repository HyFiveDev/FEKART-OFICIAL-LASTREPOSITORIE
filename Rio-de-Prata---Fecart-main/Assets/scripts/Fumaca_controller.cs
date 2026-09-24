using UnityEngine;

public class ParticleKeyTrigger : MonoBehaviour
{
    [Header("Circle")]
    public ParticleSystem particle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particle.Play();
        }
    }
}