using UnityEngine;

public class BounceWithObject : MonoBehaviour
{
    [SerializeField] private string bounceTag;
    [SerializeField] private float bounceSpeed = 4f;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(bounceTag))
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // Optional: only bounce if falling downwards
            
            // Bounce upwards
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, bounceSpeed, rb.linearVelocity.z);
        }
    }
}
