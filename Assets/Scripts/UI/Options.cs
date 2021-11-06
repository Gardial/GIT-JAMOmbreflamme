using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Options : MonoBehaviour
{
    public GameObject Panel;
   // private bool visible = false;

    public AudioSource audioSource;
    public Slider slider;
    public Text txtVolume;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Panel.activeSelf == false)
            {
                Panel.SetActive(true);
            }
        }
    }

    public void SliderChanger()
    {
        audioSource.volume = slider.value;
        txtVolume.text = "Volume : " + (audioSource.volume * 100).ToString("00") + "%";
        PlayerPrefs.SetFloat("Volume", slider.value);

    }

 
}
