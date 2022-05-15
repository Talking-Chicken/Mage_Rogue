using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText, experienceText;
    private PlayerControl player;
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    
    void Update()
    {
        healthText.text = player.Health + "/" + player.MaxHealth;
    }
}
