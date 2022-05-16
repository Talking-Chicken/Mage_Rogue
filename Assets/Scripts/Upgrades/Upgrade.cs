using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
