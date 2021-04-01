using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utilities;

public class SceneController : MonoBehaviour
{
    /*
        SceneController manages transitions between scenes.
    */

    /*
        Will call the gamescene, loadscene will load on the next update and remain until the gamescene is loaded.
    */
    public void LaunchGame()
    {
        LaunchLevel1();
    }

    public void LaunchLevel1()
    {
        SceneManager.LoadSceneAsync(SceneNames.LEVEL_1);
    }

    public void LaunchLevel2()
    {
        SceneManager.LoadSceneAsync(SceneNames.LEVEL_2);
    }

    public void LaunchLevel3()
    {
        SceneManager.LoadSceneAsync(SceneNames.LEVEL_3);
    }

    public void LaunchLevel4()
    {
        SceneManager.LoadSceneAsync(SceneNames.LEVEL_4);
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    /*
        Will call the menuscene, loadscene will load on the next update and remain until the menuscene is loaded.
    */
    public void LaunchMainMenu()
    {
        // Set the loader callback action to load the game scene
        Debug.Log("Test");
        SceneManager.LoadSceneAsync(SceneNames.MAINMENU);
    }

    /*
        Exits the application.
    */
    public void QuitGame()
    {
        Application.Quit();
    }
}