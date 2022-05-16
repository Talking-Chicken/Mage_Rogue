using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Update() {
        scoreText.text = "You've Walked over " + PlayerPrefs.GetInt("steps") + " tiles!";
    }

    public void retrunToTittle() {
        SceneManager.LoadScene("Title");
    }
}
