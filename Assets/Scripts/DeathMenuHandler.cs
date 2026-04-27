using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuHandler : MonoBehaviour
{
    public GameObject deathMenu;
    public GameObject[] otherMenus;
    public Selectable firstSelectedObject;
    private AudioSource bgm;
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += ShowDeathMenu;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= ShowDeathMenu;
    }

    private void Awake()
    {
        bgm = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        if (otherMenus == null)
        {
            otherMenus = GameObject.FindGameObjectsWithTag("Menu");
        }
    }

    private void ShowDeathMenu()
    {
        foreach(var menu in otherMenus)
        {
            menu.SetActive(false);
        }
        deathMenu.SetActive(true);
        bgm.Stop();
        firstSelectedObject.Select();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
