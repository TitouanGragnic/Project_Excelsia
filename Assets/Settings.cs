using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


namespace scripts
{
    public class Settings : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public Dropdown resolutionDropdown;
        public GameObject setting;
        public GameObject menu;
        public Perso perso;

        Resolution[] resolutions;

        void Start()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentIndex = i;
                }
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentIndex;
            resolutionDropdown.RefreshShownValue();
        }
        public void SetResolution(int index)
        {
            Resolution resolution = resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
        }
        public void SetQuality(int quality)
        {
            QualitySettings.SetQualityLevel(quality);
        }

        public void SetFullscreen(bool isFull)
        {
            Screen.fullScreen = isFull;
        }

        public void Quit()
        {
            setting.SetActive(false);
            menu.SetActive(true);
        }
        public void Quit1()
        {
            setting.SetActive(false);
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public void Abandonne()
        {
            setting.SetActive(false);
            menu.SetActive(true);
            perso.health = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

