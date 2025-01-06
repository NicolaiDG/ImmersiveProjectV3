using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDisplay : MonoBehaviour
{
    [Header("Scene Settings")]
    public string SceneName; // Naam van de scène die je wilt weergeven

    [Header("Render Target")]
    public Renderer TargetRenderer; // Renderer waar de scène op weergegeven wordt
    public int RenderTextureWidth = 1920;
    public int RenderTextureHeight = 1080;

    private Camera sceneCamera; // Camera van de geladen scène
    private RenderTexture renderTexture; // Texture waarop de scène rendert

    private void Start()
    {
        // Maak een nieuwe RenderTexture
        renderTexture = new RenderTexture(RenderTextureWidth, RenderTextureHeight, 24);
        
        // Koppel de RenderTexture aan het materiaal van het object
        if (TargetRenderer != null)
        {
            TargetRenderer.material.mainTexture = renderTexture;
        }

        // Laad de scène in de achtergrond
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive).completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation operation)
    {
        // Zoek de camera van de geladen scène
        Scene loadedScene = SceneManager.GetSceneByName(SceneName);
        if (loadedScene.isLoaded)
        {
            GameObject[] sceneObjects = loadedScene.GetRootGameObjects();
            foreach (GameObject obj in sceneObjects)
            {
                Camera cam = obj.GetComponentInChildren<Camera>();
                if (cam != null)
                {
                    sceneCamera = cam;
                    sceneCamera.targetTexture = renderTexture; // Koppel de camera aan de RenderTexture
                    sceneCamera.gameObject.SetActive(true);
                    return;
                }
            }
        }

        Debug.LogWarning($"No camera found in scene {SceneName}");
    }
}
