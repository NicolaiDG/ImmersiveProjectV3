using UnityEngine;

public class ProximityGrowth : MonoBehaviour
{
    [Header("Proximity Settings")]
    public float detectionRadius = 5f;
    public float growthSpeed = 1f;
    public float shrinkSpeed = 0.5f;
    public Vector3 maxScale = new Vector3(2f, 2f, 2f);
    public Vector3 minScale = Vector3.one;
    public float maxLightIntensity = 10f;
    public float minLightIntensity = 0f;

    [Header("Detection Settings")]
    public string playerTag = "Player";
    public string growableTag = "CanGrow";

    [Header("Special Object Settings")]
    [Tooltip("The object to move once the condition is met")]
    public GameObject specialObject;

    [Tooltip("The position to move the special object to")]
    public Vector3 targetPosition = Vector3.zero;

    [Tooltip("The intensity threshold for triggering the object move")]
    public float intensityThreshold = 8f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;
        if (specialObject == null)
        {
            Debug.LogWarning("Special object is not assigned.");
        }
    }

    void Update()
    {
        if (player == null) return;

        GameObject[] growableObjects = GameObject.FindGameObjectsWithTag(growableTag);
        bool allLightsAboveThreshold = true;

        foreach (GameObject obj in growableObjects)
        {
            float distanceToPlayer = Vector3.Distance(player.position, obj.transform.position);

            if (distanceToPlayer <= detectionRadius)
            {
                GrowObject(obj);
            }
            else
            {
                ShrinkObject(obj);
            }

            // Check if all lights are above the intensity threshold
            if (obj.TryGetComponent(out Light pointLight))
            {
                if (pointLight.intensity < intensityThreshold)
                {
                    allLightsAboveThreshold = false;
                }
            }
        }

        // If all lights are above the threshold, move the special object
        if (allLightsAboveThreshold && specialObject != null)
        {
            specialObject.transform.position = targetPosition;
        }
    }

    void GrowObject(GameObject obj)
    {
        if (!obj.TryGetComponent(out ObjectScaleTracker tracker))
        {
            tracker = obj.AddComponent<ObjectScaleTracker>();
            tracker.originalScale = obj.transform.localScale;
        }

        obj.transform.localScale = Vector3.Lerp(
            obj.transform.localScale,
            maxScale,
            growthSpeed * Time.deltaTime
        );

        if (obj.TryGetComponent(out Light pointLight))
        {
            pointLight.range = Mathf.Lerp(pointLight.range, maxLightIntensity, growthSpeed * Time.deltaTime);
            pointLight.intensity = pointLight.range;
        }
    }

    void ShrinkObject(GameObject obj)
    {
        if (obj.TryGetComponent(out ObjectScaleTracker tracker))
        {
            obj.transform.localScale = Vector3.Lerp(
                obj.transform.localScale,
                tracker.originalScale,
                shrinkSpeed * Time.deltaTime
            );
        }

        if (obj.TryGetComponent(out Light pointLight))
        {
            pointLight.range = Mathf.Lerp(pointLight.range, minLightIntensity, shrinkSpeed * Time.deltaTime / 5);
            pointLight.intensity = pointLight.range;
        }
    }

    private class ObjectScaleTracker : MonoBehaviour
    {
        public Vector3 originalScale;
    }

    void OnDrawGizmosSelected()
    {
        GameObject[] growableObjects = GameObject.FindGameObjectsWithTag(growableTag);

        foreach (GameObject obj in growableObjects)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(obj.transform.position, detectionRadius);
        }
    }
}