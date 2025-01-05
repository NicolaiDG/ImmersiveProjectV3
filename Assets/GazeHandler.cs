using UnityEngine;

public class GazeHandler : MonoBehaviour
{
    public GameObject infoText;

   void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger Enter detected with: " + other.name);
    if (other.CompareTag("MainCamera"))
    {
        Debug.Log("MainCamera entered the trigger.");
        if (infoText != null) infoText.SetActive(true);
    }
}

void OnTriggerExit(Collider other)
{
    Debug.Log("Trigger Exit detected with: " + other.name);
    if (other.CompareTag("MainCamera"))
    {
        Debug.Log("MainCamera exited the trigger.");
        if (infoText != null) infoText.SetActive(false);
    }
}

}
