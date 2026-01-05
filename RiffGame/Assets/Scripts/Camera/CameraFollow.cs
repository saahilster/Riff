using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform parent;
    public Transform target;
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
        if (parent.childCount <= 0)
        {
            return;
        }
        else
        {
            foreach (Transform child in parent)
            {
                if (child.name == "MagePrefab(Clone)" ||
                    child.name == "MedicPrefab(Clone)" ||
                    child.name == "KnightPrefab(Clone)")
                {
                    target = child;
                    break;
                }
            }
        }

        followVector = new Vector3(target.position.x, yOffset, target.position.z - zOffset);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followVector, followSpeed * Time.deltaTime);
    }
}
