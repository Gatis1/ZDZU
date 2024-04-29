using System;
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
    [Range(0f, .5f)]
    public float cursorLimitsRelativeToScreen;

    public bool CursorIsInsideScreen = true;

    
void Update()
    {
        // Get joystick input
        moveInput = joystick.Direction;

        // Combine joystick input with the direction based on the camera POV
        Vector3 moveDirection = transform.up * moveInput.y + transform.right * moveInput.x;
        
        // and calculate the desired movement
        Vector3 DesiredMovement = moveDirection * aimSensitivity;

        AimCursor.transform.position += DesiredMovement;

        // Check the cursor position relative to the canvas size (to check if it's whitihin the screen)
        Vector3 CursorsScreenPos = AimCursor.anchoredPosition;
        Vector2 canvasSize = AimCursor.root.GetComponent<RectTransform>().sizeDelta;
        Vector2 positionRelativeToScreenSize = new Vector2(CursorsScreenPos.x / canvasSize.x, CursorsScreenPos.y / canvasSize.y);   
        

        // Check if the cursor is in the screen. If it is, do nothing. If it isn't, rotate thee comera towards the cursor
        if ((positionRelativeToScreenSize.x > -cursorLimitsRelativeToScreen && positionRelativeToScreenSize.x < cursorLimitsRelativeToScreen) &&
            (positionRelativeToScreenSize.y > -cursorLimitsRelativeToScreen && positionRelativeToScreenSize.y < cursorLimitsRelativeToScreen))
        {
            CursorIsInsideScreen = true;
            // Apply movement
        }
        else
        {
            CursorIsInsideScreen = false;
            // Clamp the position within the screen bounds
            positionRelativeToScreenSize.x = Mathf.Clamp(positionRelativeToScreenSize.x, -cursorLimitsRelativeToScreen, cursorLimitsRelativeToScreen);
            positionRelativeToScreenSize.y = Mathf.Clamp(positionRelativeToScreenSize.y, -cursorLimitsRelativeToScreen, cursorLimitsRelativeToScreen);

            // Make a new position where player is whithin position, but still with the updated desired position
            Vector2 newPos = new Vector2(positionRelativeToScreenSize.x * canvasSize.x, positionRelativeToScreenSize.y * canvasSize.y);   

            // Update the position
            AimCursor.anchoredPosition = newPos;
        }
    }


    
}
