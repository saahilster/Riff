using UnityEngine;
using UnityEngine.Events;
public class CollisionStarters : MonoBehaviour
{
    public UnityEvent startEncounter; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Started");
            startEncounter.Invoke();
        }
    }
}
