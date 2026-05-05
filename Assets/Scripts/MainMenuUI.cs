using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private GameObject levelsMenu;
    [SerializeField] private GameObject optionsMenu;
    private bool loading = false;





    public void OpenTitleMenu()
    {
        titleMenu.SetActive(true);
        levelsMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void OpenLevelsMenu()
    {
        titleMenu.SetActive(false);
        levelsMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void OpenOptionsMenu()
    {
        titleMenu.SetActive(false);
        optionsMenu.SetActive(true);
        levelsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSwingLevel()
    {
        if(loading) return;
        SceneManager.LoadScene(1);
        LevelLoading();
    }
    public void LoadJumpLevel()
    {
        if (loading) return;
        LevelLoading();
        SceneManager.LoadScene(2);
    }
    public void LoadTownLevel()
    {
        if (loading) return;
        LevelLoading();
        SceneManager.LoadScene(3);
    }
    public void LoadTestLevel()
    {
        if (loading) return;
        LevelLoading();
        SceneManager.LoadScene(4);
    }

    private void LevelLoading()
    {
        loading = true;
    }
}

