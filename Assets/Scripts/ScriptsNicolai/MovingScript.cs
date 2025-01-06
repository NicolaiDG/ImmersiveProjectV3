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

    void Start()
    {
        // Sla de beginpositie van het object op
        startPosition = transform.position;
    }

    void Update()
    {
        // Horizontale rotatie
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Verticale oscillatie
        float newY = startPosition.y + Mathf.Sin(Time.time * oscillationSpeed) * oscillationHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
