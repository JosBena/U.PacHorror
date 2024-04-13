using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private Scores _playerMetaScore;
    public Text metaCaption;
    public Text metaPoints;
    public GameObject difficulties;
    public GameObject startButton;

    void Start()
    {
        _playerMetaScore = Scriptables.PlayerMetaScore;
        metaPoints.text = "Meta Points: " + _playerMetaScore.coins;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowDifficulties()
    {
        startButton.SetActive(false);
        difficulties.SetActive(true);
    }

    public void ToggleWindow(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }

    IEnumerator UpdateMetaCaption(string caption)
    {
        metaCaption.text = caption;
        yield return new WaitForSeconds(5);
        metaCaption.text = "";
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        StartCoroutine(UpdateMetaCaption(logString));
    }
}
