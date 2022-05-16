using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UpgradeType {Sight, MaxHealth, HealthRegeneration, ZombieResistence, RatResistence, IronDummyResistence}
public class UpgradeControl : MonoBehaviour
{
    [SerializeField] private List<Upgrade> upgradesList;
    private Upgrade[] upgrades = new Upgrade[3];
    [SerializeField] private Button[] upgradButtons;
    private Image[] upgradeImages;
    [SerializeField] private TextMeshProUGUI upgradeNameText, upgradeDesText;
    private int currentSelectingUpgrade = 0;
    [SerializeField] private GameObject indicator;

    public Upgrade[] Upgrades {get=>upgrades;}
    public Button[] UpgradeButtons {get=>upgradButtons;}
    public Image[] UpgradeImages {get=>upgradeImages;}

    void Start() {
        upgradeImages = new Image[UpgradeButtons.Length];
        for (int i = 0; i < UpgradeButtons.Length; i++)
            upgradeImages[i] = UpgradeButtons[i].GetComponentInChildren<Image>();
        disableUpgradeButtons();
    }

    public void showUpgradeInfo(int index) {
        currentSelectingUpgrade = index;
        indicator.transform.position = upgradButtons[index].transform.position;
        UpgradeButtons[index].Select();
        upgradeNameText.text = Upgrades[index].UpgradeName;
        upgradeDesText.text = Upgrades[index].Desciption;
    }

    public void showUpgradeIcons() {
        for (int i = 0; i < UpgradeImages.Length; i++)
            UpgradeImages[i].sprite = Upgrades[i].Icon;
    }

    public void randomlySetUpgrades() {
        for (int i = 0; i < Upgrades.Length; i++)
            Upgrades[i] = upgradesList[Random.Range(0,upgradesList.Count)];
    }
    
    /* diable buttons, set icon image to invisible and set indicator inactive */
    public void disableUpgradeButtons() {
        foreach(Button upgradeButton in upgradButtons)
            upgradeButton.interactable = false;
        foreach(Image image in upgradeImages)
            image.color = new Color (255,255,255,0);
        indicator.SetActive(false);
    }

    /* enable buttons, set icon image to visible and set indicator active */
    public void enableUpgradeButtons() {
        foreach(Button upgradeButton in upgradButtons)
            upgradeButton.interactable = true;
        foreach(Image image in upgradeImages)
            image.color = new Color (255,255,255,255);
        indicator.SetActive(true);
    }

    /* to use this upgrade, making the upgrade effect */
    public void useCurrentUpgrade() {
        upgrades[currentSelectingUpgrade].upgrade();
    }

    /* to select next upgrade, only show info */
    public void selectNextUpgrade() {
        currentSelectingUpgrade = Mathf.Min(currentSelectingUpgrade+1, upgrades.Length-1);
        showUpgradeInfo(currentSelectingUpgrade);
    }

    /* to select previous upgrade, only show info */
    public void selectPreviousUpgrade() {
        currentSelectingUpgrade = Mathf.Max(currentSelectingUpgrade-1, 0);
        showUpgradeInfo(currentSelectingUpgrade);
    }
}
