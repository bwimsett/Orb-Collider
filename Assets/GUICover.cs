using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GUICover : MonoBehaviour {

    public GameObject endScreen, mainMenu, infoScreen;
    public CanvasGroup endScreenCanvasGroup, mainMenuCanvasGroup, infoCanvasGroup;
    public Button mainMenuButton, infoBackButton;
    public float showDuration, hideDuration;
    public Ease showEase, hideEase;

    private bool endScreenDisplayed, infoScreenDisplayed;

    void Awake() {
        transform.position = new Vector3(0, 0, 0);
        DisplayMainMenu();
    }

    public void StartGame() {
        GameManager.levelManager.StartGame();
        Hide();
    }
    
    public void DisplayEndScreen() {
        mainMenuButton.interactable = true;
        endScreenCanvasGroup.alpha = 1;
        endScreenCanvasGroup.interactable = true;
        endScreen.gameObject.SetActive(true);
        infoScreen.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        infoScreenDisplayed = false;
        endScreenDisplayed = true;
        Show();
    }

    public void DisplayMainMenu() {
        if (infoScreenDisplayed) {
            infoScreenDisplayed = false;
            SwitchCanvasGroups(mainMenuCanvasGroup, infoCanvasGroup);
        } else if (endScreenDisplayed) {
            endScreenDisplayed = false;
            SwitchCanvasGroups(mainMenuCanvasGroup, endScreenCanvasGroup);
        } else {
            mainMenu.gameObject.SetActive(true);
            endScreen.gameObject.SetActive(false);
            infoScreen.gameObject.SetActive(false);
            Show();
        }
    }

    public void DisplayInfoScreenViaMenu() {
        Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
        buttonClickedEvent.AddListener(DisplayMainMenu);
        infoBackButton.onClick = buttonClickedEvent;
        SwitchCanvasGroups(infoCanvasGroup, mainMenuCanvasGroup);
        infoScreenDisplayed = true;
    }

    public void DisplayInfoScreenViaGame() {
        Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
        buttonClickedEvent.AddListener(Hide);
        infoBackButton.onClick = buttonClickedEvent;
        infoScreenDisplayed = true;
        mainMenu.gameObject.SetActive(false);
        infoScreen.gameObject.SetActive(true);
        infoCanvasGroup.alpha = 1;
        infoCanvasGroup.interactable = true;
        Show();
    }

    private void SwitchCanvasGroups(CanvasGroup toShow, CanvasGroup toHide) {
        toHide.interactable = false;
        toHide.DOFade(0, 0.15f).OnComplete(() => {
            toShow.gameObject.SetActive(true);
            toHide.gameObject.SetActive(false);
            toShow.interactable = true;
            toShow.DOFade(1, 0.15f);
        });
    }
    
    public void Hide() {
        transform.DOLocalMove(new Vector3(0, 1080), hideDuration).SetEase(hideEase);
    }

    public void Show() {
        transform.DOLocalMove(new Vector3(0, 0, 0), showDuration).SetEase(showEase);
    }
    
    

}
