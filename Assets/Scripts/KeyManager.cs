using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public int totalKeys = 3; 
    private int keysCollected = 0;

    public GameObject portal;

    void Start()
    {
        if (portal != null)
        {
            portal.SetActive(false);
        }
    }

    public void CollectKey()
    {
        keysCollected++;

        if (keysCollected >= totalKeys)
        {
            OpenPortal();
        }
    }

    private void OpenPortal()
    {
        if (portal != null)
        {
            portal.SetActive(true); 
        }
    }
}
