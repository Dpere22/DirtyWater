using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);   // Keep it across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HideCursor();
    }

    void Update()
    {
        // Ensure cursor stays hidden
        if (Cursor.visible || Cursor.lockState != CursorLockMode.Locked)
        {
            HideCursor();
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
