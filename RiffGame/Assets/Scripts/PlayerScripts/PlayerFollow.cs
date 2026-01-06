using Unity.VisualScripting;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    Transform leader;
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    private Vector3 target;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (leader == null)
        {
            return;
        }


        target = new Vector3(leader.position.x - xOffset, leader.position.y, leader.position.z - zOffset);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }

    public void SetTarget(Transform chosen)
    {
        leader = chosen;
    }

}
