using TreeEditor;
using UnityEngine;
using UnityEngine.UI;


public class AimCursorJoystick : MonoBehaviour
{
    [Header("REFERENCES")]
    public Joystick joystick;
    public RectTransform AimCursor;

    [Header("INPUT")]
    public Vector2 moveInput;

    [Header("AIM SETTINGS")] 
    public float aimSensitivity;
    public float CameraToCursorSensitivity;

    public bool CursorIsInsideScreen = true;

    void Update()
    {
        // Get joystick input
        moveInput = joystick.Direction;

        // Combine joystick input with the direction based on the camera POV
        Vector3 moveDirection = transform.up * moveInput.y + transform.right * moveInput.x;
        
        // and calculate the desired movement
        Vector3 DesiredMovement = moveDirection * aimSensitivity;

        // Apply movement
        AimCursor.transform.position += DesiredMovement;

        /*
         * Now, if the cursor reaches the edge of the screen, or gets close to it, then rotate camera towards cursor
         */
        
        // Convert coordinates of cursor to 
        Vector3 CursorsScreenPos = Camera.main.WorldToScreenPoint(AimCursor.position);
        
        // Check if the cursor is in the screen. If it is, do nothing. If it isn't, rotate thee comera towards the cursor
        if (CursorsScreenPos.x < 0 || CursorsScreenPos.x > Screen.width ||
            CursorsScreenPos.y < 0 || CursorsScreenPos.y > Screen.height)
        {
            CursorIsInsideScreen = false;
            RotateCameraTowardsCursor();
        }
        else
        {
            CursorIsInsideScreen = true;
        }
    }

    private void RotateCameraTowardsCursor()
    {
        // Get aim cursors position based on camera's POV
        Vector3 CursorPos = AimCursor.transform.position - transform.position;

        // Calculate the rotation to face the target
        Quaternion desiredRotation = Quaternion.LookRotation(CursorPos);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, CameraToCursorSensitivity * Time.deltaTime);

    }
    
}
