using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public GameObject levelChanger;
    public GameObject ExitPanel;


    void Update()
    {
        if (levelChanger.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            levelChanger.SetActive(false);
        }
        else if (ExitPanel.activeSelf == false && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPanel.SetActive(false);
        }

    }
   
   public void OnClickStart()
    {
        levelChanger.SetActive(true);
    }
    
    public void OnClickExit()
    {
        Application.Quit();
    }
    
    public void levelBttns(int level)
    {
        SceneManager.LoadScene(level);
    }
}
