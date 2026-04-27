
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
    
    public GameObject pauseMenuUI;
    public InputManager inputManager;
    public GameObject[] otherMenus;

    public Selectable firstSelectedObject;
    private AudioSource bgm;
    private bool isGamePaused = false;
    private void OnEnable()
    {
        inputManager.PauseGame += OnPause;
    }
    private void OnDisable()
    {
        inputManager.PauseGame -= OnPause;
    }


    public void OnPause()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void Awake()
    {
        if (otherMenus == null)
        {
            otherMenus = GameObject.FindGameObjectsWithTag("Menu");
        }
        bgm = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
    }

    public void ResumeGame()
    {
        //Time.timeScale = 1f;
        bgm.UnPause();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputManager.DisableUIActions();
        inputManager.EnablePlayerActions();
        isGamePaused = false;
        pauseMenuUI.SetActive(false);
    }
    
    public void PauseGame()
    {
        foreach (var menu in otherMenus)
        {
            menu.SetActive(false);
        }
        
        //Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputManager.DisablePlayerActions();
        inputManager.EnableUIActions();
        bgm.Pause();
        isGamePaused = true;

        pauseMenuUI.SetActive(true);
        firstSelectedObject.Select();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }




}
