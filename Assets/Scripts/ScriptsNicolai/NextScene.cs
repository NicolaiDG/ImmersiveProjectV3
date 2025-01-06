using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string scenename;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            // Corrected this line to use SceneManager
            SceneManager.LoadScene(scenename);
        }
    }
}
