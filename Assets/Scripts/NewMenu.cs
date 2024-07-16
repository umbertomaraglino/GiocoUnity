using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewMenu : MonoBehaviour
{

    public GameObject panel;


    public void PlayGame(){
        SceneManager.LoadScene("SampleScene");
        //SceneManager.UnloadScene("NewMenu");
    }

    public void OnResume(){
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMainMenu(){
        Debug.Log("cliccato");
        SceneManager.LoadScene("NewMenu");
        Time.timeScale = 1f;
        //SceneManager.UnloadScene(sceneName: "SampleScene");
    }

}
