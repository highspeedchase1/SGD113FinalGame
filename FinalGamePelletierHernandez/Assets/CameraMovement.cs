using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float mouseSpeed = 5f;
    float yaw;
    float pitch;


    private void Update()
    {
        //Moves camera as player moves
        transform.position = player.position + offset;

        //Assigns input for rotation control
        yaw += Input.GetAxisRaw("Mouse X") * mouseSpeed;
        pitch -= Input.GetAxisRaw("Mouse Y") * mouseSpeed;

        pitch = Mathf.Clamp(pitch, -20.9f, 40.9f);

        //Adjusts camera rotation around character
        Vector3 targetRotation = new Vector2(pitch, yaw);
        transform.eulerAngles = targetRotation;

    }
}
