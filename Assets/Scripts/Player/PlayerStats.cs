using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private int healthRegeneration = 1, health, maxHealth = 10;
    private int experience;
    private int sight = 3;
    private int zombieResistence, ratResistence, ironDummyResistence;
    private int emptyFrequency = 3, 
                enemyFrequency = 3, 
                experienceFrequency = 2, 
                healthFrequency = 2, 
                zombieFrequency = 2, 
                ratFrequency = 4, 
                ironDummyFrequency = 1;

    public int HealthRegeneration {get=>healthRegeneration; set=> healthRegeneration = value;}
    public int Health {get=>health; set=> health = value;}
    public int Experience {get=>experience; set=> experience = value;}
    public int Sight {get=>sight; set=> sight = value;}
    public int MaxHealth {get=>maxHealth; set=> maxHealth = value;}
    public int ZombieResistence {get=>zombieResistence; set=> zombieResistence = value;}
    public int RatResistence {get=>ratResistence; set=> ratResistence = value;}
    public int IronDummyResistence {get=>ironDummyResistence; set=> ironDummyResistence = value;}
    public int EmptyFrequency {get=>emptyFrequency; set=> emptyFrequency = value;}
    public int EnemyFrequency {get=>enemyFrequency; set=> enemyFrequency = value;}
    public int ExperienceFrequency {get=>experienceFrequency; set=> experienceFrequency = value;}
    public int HealthFrequency {get=>healthFrequency; set=> healthFrequency = value;}
    public int ZombieFrequency {get=>zombieFrequency; set=> zombieFrequency = value;}
    public int RatFrequency {get=>ratFrequency; set=> ratFrequency = value;}
    public int IronDummyFrequency {get=>ironDummyFrequency; set=> ironDummyFrequency = value;}
}
