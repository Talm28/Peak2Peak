using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    public GameObject settings;
    public GameObject game;
    public GameObject menu;
    public GameObject confirmPanel;
    Animator confirmPanelAnimator;
    public GameObject resetButton;

    public Slider musicSilder;
    public Slider soundEffectsSlider;

    bool isClickable;
    public bool isPlay;

    void Start()
    {
        isClickable = true;
        confirmPanelAnimator = confirmPanel.GetComponent<Animator>();

    }

    void Update()
    {
        SoundManager.musicVolume = musicSilder.value;
        SoundManager.soundEffectVolume = soundEffectsSlider.value;
        SoundManager.skiVolume = soundEffectsSlider.value;
    }

    public void OpenConfirmPanel()
    {
        if (isClickable)
        {
            SoundManager.PlaySound("Wosh in");
            confirmPanel.SetActive(true);
            isClickable = false;
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("Snow coins", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Level score", 0);

        PlayerPrefs.SetInt("Lifes", 3);

        PlayerPrefs.SetInt("Player_2", 0);
        PlayerPrefs.SetInt("Player_3", 0);
        PlayerPrefs.SetInt("Player_4", 0);
        PlayerPrefs.SetInt("Player_5", 0);
        PlayerPrefs.SetInt("Player_6", 0);
        PlayerPrefs.SetInt("Player_7", 0);
        PlayerPrefs.SetInt("Player_8", 0);
        PlayerPrefs.SetInt("Player_9", 0);
        PlayerPrefs.SetInt("Player_10", 0);
        PlayerPrefs.SetInt("Player_11", 0);
        PlayerPrefs.SetInt("Player_12", 0);
        PlayerPrefs.SetInt("Player_13", 0);
        PlayerPrefs.SetInt("Player_14", 0);
        PlayerPrefs.SetInt("Player_15", 0);
        PlayerPrefs.SetInt("Player_16", 0);
        PlayerPrefs.SetInt("Player_17", 0);
        PlayerPrefs.SetInt("Player_18", 0);
        PlayerPrefs.SetInt("Player_19", 0);
        PlayerPrefs.SetInt("Player_20", 0);

        PlayerPrefs.SetInt("Double coins", 0);
        PlayerPrefs.SetInt("Extra life", 0);
        PlayerPrefs.SetInt("Freeze", 0);
        PlayerPrefs.SetInt("Shield", 0);
        PlayerPrefs.SetInt("Keys", 0);

        StartCoroutine(CloseInstructionsPanel());
    }

    public void BackToMenu()
    {
        if (isClickable)
        {
            StartCoroutine(BackToMenuCoroutine());
        }
    }
    IEnumerator BackToMenuCoroutine()
    {
        if (isPlay)
        {
            SoundManager.PlaySound("Wosh out");
            settings.GetComponent<Animator>().SetBool("isClose", true);
            yield return new WaitForSeconds(0.6f);
            isPlay = false;
            GameController.isSettings = false;
            settings.SetActive(false);
        }
        else
        {
            SoundManager.PlaySound("Wosh out");
            settings.GetComponent<Animator>().SetBool("isClose", true);
            yield return new WaitForSeconds(0.3f);
            resetButton.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            SoundManager.PlaySound("Wosh in");
            menu.SetActive(true);
            settings.SetActive(false);
            isPlay = false;
        }
    }

    public void ShowInstructions()
    {
        if (isClickable)
        {
            GameController.isInstruction = true;
            GameController.instructionStage = 0;
            PlayerPrefs.SetInt("Instructions", 0);
            PlayerMovement.isInstruction = true;
            GameController.isSking = false;
        }
    }

    public void CloseInstructions()
    {
        StartCoroutine(CloseInstructionsPanel());
    }
    IEnumerator CloseInstructionsPanel()
    {
        isClickable = true;
        confirmPanelAnimator.SetBool("isClose", true);
        yield return new WaitForSeconds(0.2f);
        SoundManager.PlaySound("Wosh out");
        yield return new WaitForSeconds(0.5f);
        confirmPanel.SetActive(false);
    }

    public void SetPlay()
    {
        isPlay = true;
    }

    public void ActiveResetButton()
    {
        StartCoroutine(ActiveResetButonCourotine());
    }
    IEnumerator ActiveResetButonCourotine()
    {
        yield return new WaitForSeconds(0.3f);
        resetButton.SetActive(true);
    }


}
