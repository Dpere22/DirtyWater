using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    [Header("Dialog UI")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    private Story currentStory;

    public bool DialogIsPlaying { get; private set; }
    
    private static DialogManager _instance;
    

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("More than one DialogManager in scene.");
        }
        _instance = this;
    }
    
    public static DialogManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        var playerInput = FindFirstObjectByType<PlayerMovement>().GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogWarning("No player input found.");
        }
        playerInput.actions["Submit"].performed += ContinueStoryCallBack;
        DialogIsPlaying = false;
        dialogPanel.SetActive(false);
    }

    public void EnterDialogMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        DialogIsPlaying = true;
        dialogPanel.SetActive(true);
        ContinueStory();

    }

    private void ExitDialogMode()
    {
        DialogIsPlaying = false;
        dialogPanel.SetActive(false);
        dialogText.text = "";
    }

    private void ContinueStoryCallBack(InputAction.CallbackContext context)
    {
        if (!DialogIsPlaying) return;
        ContinueStory();
    }
    
    
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogMode();
        }
    }
}
