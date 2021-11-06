using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject Panel;
    private bool visible = false;
    public AudioSource audioSource;

     void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("Volume");
    }


    public void Play()
    {
        PlayerPrefs.SetFloat("Volume", audioSource.volume);
        SceneManager.LoadScene("Level 1");  
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MenuOptions()
    {
        Debug.Log("BoutonOption");
        visible = true;
        Panel.SetActive(visible);
    }

}
