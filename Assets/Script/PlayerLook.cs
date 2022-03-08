using UnityEngine;

[AddComponentMenu("Camera/Simple Smooth Mouse Look ")]
public class PlayerLook : MonoBehaviour
{
    [SerializeField] protected Transform playerCamera = null;

    [SerializeField] public float xSensitivity = 3.5f;
    [SerializeField] public float ySensitivity = 3.5f;

    private float xRotation = 0f;
    public bool lockCursor = true;

    // Start is called before the first frame update
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.fixedDeltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //apply this to our camera transform
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //rotation player to look left and right
        transform.Rotate((mouseX * Time.fixedDeltaTime) * xSensitivity * Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the cursor is always locked when set
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
