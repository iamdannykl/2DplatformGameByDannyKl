using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIsc : MonoBehaviour
{
    public GameObject mianBan;

    public void openMb()
    {Debug.Log("huihuihui");
        mianBan.SetActive(true);
        Time.timeScale = 0;
    }

    public void hardStart()
    {
        SceneManager.LoadScene("pathOfPain");
    }
    public void backMb()
    {
        mianBan.SetActive(false);
        Time.timeScale = 1;
    }

    public void exitGm()
    {
        Application.Quit();
    }

    public void startGm()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void toStart()
    {
        SceneManager.LoadScene("start");
        Time.timeScale = 1;
    }
}
