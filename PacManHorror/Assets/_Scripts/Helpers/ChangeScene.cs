using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;
    [HideInInspector]
    public string currentScene, currentBackgroundMusic;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
    }

    public void changeScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);

        //Sound Data
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Map")
        {
            AudioManager.instance.StopAll();
            currentBackgroundMusic = "BackgroundMusicMap";
            AudioManager.instance.Play("BackgroundMusicMap");
        }
        else if (currentScene == "Shop")
        {
            AudioManager.instance.StopAll();
            currentBackgroundMusic = "BackgroundMusicShop";
            AudioManager.instance.Play("BackgroundMusicShop");
        }
        else if (currentScene == "Menu")
        {
            AudioManager.instance.StopAll();
            currentBackgroundMusic = "BackgroundMusicShop";
            AudioManager.instance.Play("BackgroundMusicShop"); //place holder
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Map")
        {
            AudioManager.instance.StopAll();
            currentBackgroundMusic = "BackgroundMusicMap";
            AudioManager.instance.Play("BackgroundMusicMap");
        }
        else if (currentScene == "Shop")
        {
            AudioManager.instance.StopAll();
            currentBackgroundMusic = "BackgroundMusicShop";
            AudioManager.instance.Play("BackgroundMusicShop");
        }
        else if (currentScene == "Menu")
        {
            AudioManager.instance.StopAll();
            currentBackgroundMusic = "BackgroundMusicShop";
            AudioManager.instance.Play("BackgroundMusicShop"); //place holder
        }
    }

    public enum LevelList
    {
        EASY, MEDIUM, HARD
    }
    public void SetDifficutly(LevelList difficulty)
    {
        switch (difficulty)
        {
            case LevelList.EASY:
                Scriptables.LevelManager.totalLevels = 2;
                break;
            case LevelList.MEDIUM:
                Scriptables.LevelManager.totalLevels = 4;
                break;
            case LevelList.HARD:
                Scriptables.LevelManager.totalLevels = 6;
                break;
            default:
                Debug.Log("Invalid Difficulty");
                break;
        }
        Scriptables.LevelManager.levelsLeft = Scriptables.LevelManager.totalLevels;
    }

    /// <summary>
    /// This version is used with integers for Buttons
    /// </summary>
    /// <param name="difficulty"></param>
    public void SetDifficutly(int difficulty) 
    {

        switch ((LevelList)difficulty)
        {
            case LevelList.EASY:
                Scriptables.LevelManager.totalLevels = 2;
                Scriptables.LevelManager.columnSize = 11;
                Scriptables.LevelManager.rowSize = 11;
                break;
            case LevelList.MEDIUM:
                Scriptables.LevelManager.totalLevels = 4;
                Scriptables.LevelManager.columnSize = 15;
                Scriptables.LevelManager.rowSize = 15;
                break;
            case LevelList.HARD:
                Scriptables.LevelManager.totalLevels = 6;
                Scriptables.LevelManager.columnSize = 21;
                Scriptables.LevelManager.rowSize = 21;
                break;
            default:
                Debug.Log("Invalid Difficulty");
                break;
        }
        Scriptables.LevelManager.levelsLeft = Scriptables.LevelManager.totalLevels;
    }
    public void CompletedLevel()
    {
        Scriptables.LevelManager.levelsLeft--;
        if (Scriptables.LevelManager.levelsLeft <= 0)
        {
            instance.changeScene("Menu");
            return;
        }
        instance.changeScene("Shop");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
