using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f; // Speed of camera moving
    public float panBorderThickness = 10f; // Distance from border for mouse to move camera
    public Vector2 panLimit;

    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position; // Finds position and defines it under pos
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) // If w is pressed or mouse is at top of screen
        {
            pos.z += panSpeed * Time.deltaTime; // Move the camera positively on the z axis
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= 0 + panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }


        if (Input.GetKey("a") || Input.mousePosition.x <= 0 + panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }


        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y += -scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);


        transform.position = pos; 
    }
}
