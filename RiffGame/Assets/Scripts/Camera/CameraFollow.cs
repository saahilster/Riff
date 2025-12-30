using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed;
    [SerializeField] float yOffset;
    [SerializeField] float zOffset;
    
    private Vector3 followVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        followVector = new Vector3(target.position.x, yOffset, target.position.z - zOffset);        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followVector, followSpeed * Time.deltaTime);
    }
}
