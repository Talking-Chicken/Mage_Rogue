using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenControl : MonoBehaviour
{
    [SerializeField] Animator controlImageAnimator, titleScreenAnimator, howToPlayAnimator;
    public void startGame() {
        SceneManager.LoadScene("Main");
    }

    public void showControl() {
        titleScreenAnimator.SetTrigger("Tittle Exit");
        controlImageAnimator.SetTrigger("Control Enter");
    }

    public void hideControl() {
        controlImageAnimator.SetTrigger("Control Exit");
        titleScreenAnimator.SetTrigger("Tittle Enter");
    }

    public void showHowToPlay() {
        titleScreenAnimator.SetTrigger("Tittle Exit");
        howToPlayAnimator.SetTrigger("How To Play Enter");
    }

    public void hideHowToPlay() {
        titleScreenAnimator.SetTrigger("Tittle Enter");
        howToPlayAnimator.SetTrigger("How To Play Exit");
    }

    public void exitGame() {
        Application.Quit();
    }
}
