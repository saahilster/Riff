using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Camera cam;

    private float scroll;

    [SerializeField] float fovStep = 2f;
    [SerializeField] float moveYStep = 1f;
    [SerializeField] float pitchStepDegrees = 2f; // smaller = less pitch per scroll

    void Update()
    {
        scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll < 0)
        {

            cam.fieldOfView += fovStep;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + moveYStep,
                transform.position.z - 1);

            transform.Rotate(pitchStepDegrees, 0f, 0f, Space.Self);
        }
        else if (scroll > 0)
        {

            cam.fieldOfView -= fovStep;

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - moveYStep,
                transform.position.z + 1);

            transform.Rotate(-pitchStepDegrees, 0f, 0f, Space.Self);
        }
    }
}
