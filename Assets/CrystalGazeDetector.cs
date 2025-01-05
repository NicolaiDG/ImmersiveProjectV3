using UnityEngine;

public class CrystalGazeDetector : MonoBehaviour
{
    [Tooltip("The maximum distance to detect crystals")]
    public float gazeDistance = 10f;

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

                    // Position and orient the text toward the player
                    activeTextObject.transform.position = crystal.transform.position + Vector3.up * 0.5f;
                    activeTextObject.transform.LookAt(vrCamera.transform);
                    activeTextObject.transform.Rotate(0, 180, 0); // Ensure the text faces the player
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
