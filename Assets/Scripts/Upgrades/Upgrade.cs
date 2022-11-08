using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Upgrade is the parent class of all other upgrades. It stores name, description, and icon. It also has a virtual function that take effect eveytimg player choose the upgrade.*/
public class Upgrade : MonoBehaviour
{
    [SerializeField] private string upgradeName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public string UpgradeName {get => upgradeName;}
    public string Desciption {get => description;}
    public Sprite Icon {get=>icon;}

    public virtual void upgrade() {
        FindObjectOfType<PlayerControl>().Stats.IsLeveledUp = false;
    }
    
}
