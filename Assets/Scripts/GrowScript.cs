using UnityEngine;

public class GrowOnProximity : MonoBehaviour
{
    public Transform player; 
    public float growRange = 80f; 
    public float growSpeed = 2f; 
    public Vector3 smallScale = new Vector3(15f, 15f, 15f);
    public Vector3 largeScale = new Vector3(50f, 50f, 50f); 

    private Vector3 targetScale; 

    void Start()
    {
        transform.localScale = smallScale;
        targetScale = smallScale;
    }

    void Update()
    {
        if (player != null)
        {
           
            float distance = Vector3.Distance(transform.position, player.position);

           
            if (distance <= growRange)
            {
                targetScale = largeScale;
            }
            else
            {
                targetScale = smallScale;
            }

         
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * growSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, growRange);
    }
}
