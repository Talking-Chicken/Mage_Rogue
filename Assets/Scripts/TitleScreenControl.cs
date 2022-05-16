using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenControl : MonoBehaviour
{
    [SerializeField] Animator controlImageAnimator, titleScreenAnimator;
    public void startGame() {
        SceneManager.LoadScene("Main");
    }

    public void showControl() {
        Debug.Log("showing");
        titleScreenAnimator.SetTrigger("Tittle Exit");
        controlImageAnimator.SetTrigger("Control Enter");
    }

    public void hideControl() {
        controlImageAnimator.SetTrigger("Control Exit");
        titleScreenAnimator.SetTrigger("Tittle Enter");
    }

    public void exitGame() {
        Application.Quit();
    }
}
