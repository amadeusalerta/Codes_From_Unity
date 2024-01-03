using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    // Camera movement speed
    public float normalSpeed = 20f;
    public float fastSpeed = 40f;
    private float currentSpeed;

    // Camera zoom speed
    public float zoomSpeed = 20f;

    // Limits for camera movement
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    // Zoom limits
    public float minY = 10f;
    public float maxY = 80f;

    // Rotation speed
    public float rotationSpeed = 50f;

    void Start()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        // Handle camera movement
        HandlePan();

        // Handle camera zoom
        HandleZoom();

        // Handle camera rotation
        HandleRotation();

        // Handle speed change when holding Shift
        HandleSpeedChange();
    }

    void HandlePan()
    {
        Vector3 pos = transform.position;

        // Get the local movement direction
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos += forward * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos -= forward * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos -= right * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos += right * currentSpeed * Time.deltaTime;
        }

        // Apply limits to camera movement
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * zoomSpeed * 100f * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationX);
        }
    }

    void HandleSpeedChange()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = fastSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }
    }
}
