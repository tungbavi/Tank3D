using UnityEngine;

public class CursorChange : MonoBehaviour
{
    [SerializeField] private Texture2D cursorPressed;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnMouseDown()
    {
        Cursor.SetCursor(cursorPressed, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}