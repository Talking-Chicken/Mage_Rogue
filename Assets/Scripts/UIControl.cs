using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*UI Control shows player health, experience, and moved steps. It also let players to choose 1 upgrade from 3 selectable sections with specific desctiptions
  of each upgrade.*/
public class UIControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText, experienceText, upgradeNameText, upgradeDesText, stepText;
    private PlayerStats playerStats;
    private UpgradeControl upgradeControl;

    void Start() {
        playerStats = FindObjectOfType<PlayerControl>().Stats;
        upgradeControl = FindObjectOfType<UpgradeControl>();
    }

    void Update()
    {
        healthText.text = "Health: " + playerStats.Health + "/" + playerStats.MaxHealth;
        experienceText.text = "Level " + playerStats.Level;
        stepText.text = PlayerPrefs.GetInt("steps") + " steps";
    }

    public void showUpgradeInfo(int index) {
        upgradeControl.UpgradeButtons[index].Select();
        upgradeNameText.text = upgradeControl.Upgrades[index].UpgradeName;
        upgradeDesText.text = upgradeControl.Upgrades[index].Desciption;
    }

    public void showUpgradeIcons() {
        for (int i = 0; i < upgradeControl.UpgradeImages.Length; i++)
            upgradeControl.UpgradeImages[i].sprite = upgradeControl.Upgrades[i].Icon;
    }

    public void enableUpgradeButtons() {
        upgradeControl.enableUpgradeButtons();
    }

    public void disableUpgradeButtons() {
        upgradeControl.disableUpgradeButtons();
    }

}
