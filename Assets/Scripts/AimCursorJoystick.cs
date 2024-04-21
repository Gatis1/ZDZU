using UnityEngine;

public class AimCursorJoystick : MonoBehaviour
{
    [Header("REFERENCES")]
    public Joystick joystick;
    public GameObject AimCursor;

    [Header("INPUT")]
    public Vector2 moveDirection;

    [Header("AIM SETTINGS")] 
    public float aimSensitivity;
    [Space] 
    
    public Vector2 DesiredMovement;

    void Update()
    {
        // Get the direction of the joystick
        moveDirection = joystick.Direction;
        // and calculate the desired movement
        DesiredMovement = moveDirection * aimSensitivity;

        // Apply movement
        AimCursor.transform.position += new Vector3(DesiredMovement.x, DesiredMovement.y, 0f);
    }
}
