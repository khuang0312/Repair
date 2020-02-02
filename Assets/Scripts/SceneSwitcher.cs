using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    string title = "TitleScene";
    string help = "HelpScene";
    string mainGame = "GameScene";


    public void Play()
    {
        SceneManager.LoadScene(mainGame);
    }

    public void Help()
    {
        SceneManager.LoadScene(help);
    }

    public void Title()
    {
        SceneManager.LoadScene(title);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
