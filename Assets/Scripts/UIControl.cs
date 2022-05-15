using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText, experienceText;
    private PlayerStats playerStats;

    void Start() {
        playerStats = FindObjectOfType<PlayerControl>().Stats;
    }

    void Update()
    {
        healthText.text = playerStats.Health + "/" + playerStats.MaxHealth;
    }
}
