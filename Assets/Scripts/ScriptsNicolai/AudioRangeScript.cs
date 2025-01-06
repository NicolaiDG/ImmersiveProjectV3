using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRangeScript : MonoBehaviour
{
    // Audio-instellingen
    public AudioClip audioClip; // Sleep je audiobestand hierin
    private AudioSource audioSource;

    // Afstandsinstellingen
    public float maxHearingDistance = 10f; // Maximale afstand waarop het geluid hoorbaar is
    public Transform player; // Sleep hier de speler of camera in

    void Start()
    {
        // Voeg een AudioSource-component toe als deze niet aanwezig is
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;

        // Start de audio
        audioSource.Play();
    }

    void Update()
    {
        // Controleer of de speler is ingesteld
        if (player != null)
        {
            // Bereken de afstand tussen speler en object
            float distance = Vector3.Distance(player.position, transform.position);

            // Pas het volume aan op basis van de afstand
            if (distance <= maxHearingDistance)
            {
                // Volume wordt sterker naarmate de speler dichterbij komt (lineair)
                audioSource.volume = 1f - (distance / maxHearingDistance);
            }
            else
            {
                // Buiten de maximale afstand is het geluid volledig stil
                audioSource.volume = 0f;
            }
        }
    }
}
