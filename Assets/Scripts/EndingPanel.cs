using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingPanel : MonoBehaviour
{
    public int qoute;

    public Text text;

    public string[] qoutes;

    private void Awake()
    {
        qoute = Random.RandomRange(0, 5);

        text.text = qoutes[qoute];
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Room");
    }

}
