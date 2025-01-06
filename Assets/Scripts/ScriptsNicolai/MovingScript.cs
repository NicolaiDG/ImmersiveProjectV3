using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour
{
    // Snelheid van de rotatie (in graden per seconde)
    public float rotationSpeed = 50f;

    // Hoogte-oscillatie instellingen
    public float oscillationSpeed = 2f; // Hoe snel het object op en neer beweegt
    public float oscillationHeight = 0.5f; // Hoeveel het object op en neer beweegt

    // Oorspronkelijke positie van het object
    private Vector3 startPosition;

    // Audio-instellingen
    public AudioClip audioClip; // Sleep je audiobestand hierin
    private AudioSource audioSource;
    public float maxHearingDistance = 10f; // Maximale afstand waarop het geluid hoorbaar is
    public Transform player; // Sleep hier de speler of camera in

    void Start()
    {
        // Sla de beginpositie van het object op
        startPosition = transform.position;

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
        // Horizontale rotatie
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Verticale oscillatie
        float newY = startPosition.y + Mathf.Sin(Time.time * oscillationSpeed) * oscillationHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Afstand tussen speler en object berekenen
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            // Pas het volume aan op basis van de afstand
            if (distance <= maxHearingDistance)
            {
                // Volume is sterker naarmate de speler dichterbij komt (lineair)
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
