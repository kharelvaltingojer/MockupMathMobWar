using UnityEngine;

public class PauseInputController : MonoBehaviour
{
    private static PauseInputController _instance;
    private PauseInput _pauseInput;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        
        _pauseInput = new PauseInput();
    }
    
    private void OnEnable() => EnableActions();
    private void OnDisable() => DisableActions();
    private void OnDestroy() => DisableActions();
    private void EnableActions()
    {
        _pauseInput.Enable();
        _pauseInput.Pause.PauseGame.performed += OnPauseGamePerformed;
    }
    private void DisableActions()
    {
        _pauseInput.Pause.PauseGame.performed -= OnPauseGamePerformed;
        _pauseInput.Disable();
        _pauseInput.Dispose();
        if (destroyPanelOnDisable)
            Destroy(_pausePanelInstance);
    }
    
    private void OnPauseGamePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _isPaused = TogglePause();
    }

    
    private bool _isPaused;
    [SerializeField]
    private GameObject pausePanelPrefab;
    private GameObject _pausePanelInstance;
    [SerializeField] private string canvasUniqueName = "Canvas";
    [SerializeField] private string pausePanelUniqueName = "PausePanel";
    [SerializeField]
    private bool destroyPanelOnDisable = true;
    public static bool IsPaused => _instance._isPaused;
    public static bool TogglePause()
    {
        _instance.CreateOrBindUI();
        _instance._isPaused = !_instance._isPaused;
        _instance._pausePanelInstance.SetActive(_instance._isPaused);
        return _instance._isPaused;
    }

    private void CreateOrBindUI()
    {
        GameObject canvas = GameObject.Find(canvasUniqueName);
        _pausePanelInstance = canvas.transform.Find(pausePanelUniqueName)?.gameObject;
        if (_pausePanelInstance == null)
        {
            _pausePanelInstance = Instantiate(pausePanelPrefab, canvas.transform);
            _pausePanelInstance.gameObject.name = pausePanelUniqueName;
        }
        _pausePanelInstance.SetActive(false);
    }

}
