using UnityEngine;

public class CrystalGazeDetector : MonoBehaviour
{
    [Tooltip("The maximum distance to detect crystals")]
    public float gazeDistance = 10f;

    [Tooltip("Vertical offset factor for the text, based on the crystal's size")]
    public float textHeightMultiplier = 0.5f;

    private Camera vrCamera;
    private GameObject activeTextObject;

    void Start()
    {
        vrCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, gazeDistance))
        {
            CrystalInfo crystal = hit.transform.GetComponent<CrystalInfo>();

            if (crystal != null)
            {
                // If a crystal is hit, enable its info text
                if (crystal.infoTextObject != null)
                {
                    // Deactivate previously active text object
                    if (activeTextObject != null && activeTextObject != crystal.infoTextObject)
                    {
                        activeTextObject.SetActive(false);
                    }

                    activeTextObject = crystal.infoTextObject;
                    activeTextObject.SetActive(true);

                    // Dynamically position the text based on the crystal's scale
                    Vector3 newPosition = crystal.transform.position + Vector3.up * (crystal.transform.localScale.y * textHeightMultiplier);
                    newPosition.x = crystal.transform.position.x + 1f;
                    activeTextObject.transform.position = newPosition;

                    // Make the text face the player
                    activeTextObject.transform.LookAt(Camera.main.transform);
                    activeTextObject.transform.Rotate(0, 180, 0); // Ensure it faces the player
                }

                return;
            }
        }

        // Deactivate the active text object if no crystal is detected
        if (activeTextObject != null)
        {
            activeTextObject.SetActive(false);
            activeTextObject = null;
        }
    }
}
