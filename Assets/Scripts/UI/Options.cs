using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Options : MonoBehaviour
{
    public GameObject Canvas;
    private AudioSource audioSource;
    public Slider slider;
    public Text txtVolume;
    private bool pause = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                audioSource.Play();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pause = !pause;
        audioSource.Play();
    }

    public void SliderChanger()
    {
        audioSource.volume = slider.value;
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}
