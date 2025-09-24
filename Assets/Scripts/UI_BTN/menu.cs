using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void lv1()
    {
        SceneManager.LoadScene("Basement");
    }
    public void lv2()
    {
        SceneManager.LoadScene("Outdoor");
    }
    public void end()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Basement");
    }
    public void about()
    {
        SceneManager.LoadScene("ABOUT");
    }
    public void setting()
    {
        SceneManager.LoadScene("setting");
    }
    public void back()
    {
        SceneManager.LoadScene("title");
    }
}
