using UnityEngine;

public class CursorController : MonoBehaviour
{
    public GameObject customCursor;
    private Vector3 lockedCursorPosition;

    void Start()
    {
        UnlockCursor(); // initially cursor unlocked
        Cursor.visible = false; // Hide the system cursor initially
    }

    void Update()
    {
        Cursor.visible = false; // Ensure the system cursor is always hidden

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor(); // Unlock the cursor with ESC
        }
        else if (Input.GetMouseButtonDown(0))
        {
            LockCursor(); // Lock the cursor with mouse click
        }

        UpdateCustomCursor(); // Update the position of the custom cursor
    }

    void LockCursor()
    {
        lockedCursorPosition = customCursor.transform.position; // Save the position of the custom cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Hide the system cursor when locked
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false; // Keep the system cursor hidden when unlocked
        customCursor.transform.position = lockedCursorPosition; // Set the custom cursor to the locked position
    }

    void UpdateCustomCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f; // The distance from the camera
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            customCursor.transform.position = new Vector3(cursorPosition.x, cursorPosition.y, 0);
        }
        else
        {
            // Maintain the locked position
            customCursor.transform.position = lockedCursorPosition;
        }

        customCursor.SetActive(true); // Ensure the custom cursor is always visible
    }
}
