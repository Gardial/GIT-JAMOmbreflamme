using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Options : MonoBehaviour
{
    public GameObject Canvas;
    public AudioSource audioSource;
    public Slider slider;
    public Text txtVolume;
    private bool pause = false;


    private void Start()
    {
        SliderChanger();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Canvas.activeSelf == false)
            {
                pause = !pause;
                if (pause)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
                Canvas.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pause = !pause;
    }

    public void SliderChanger()
    {
        audioSource.volume = slider.value;
        txtVolume.text = "Volume : " + (audioSource.volume * 100).ToString("00") + "%";
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}
