using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeyManager keyManager = FindObjectOfType<KeyManager>();
            if (keyManager != null)
            {
                keyManager.CollectKey();
            }

            Destroy(gameObject);
        }
    }
}
