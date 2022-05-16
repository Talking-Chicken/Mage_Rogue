using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public Vector3Int currentIndex;
    private Vector3Int movingDestination;
    private PathGenerator pathGenerator;
    [SerializeField] private FixedData mapData;
    private PlayerStats playerStats = new PlayerStats();
    private UpgradeControl upgradeControl;
    private Boss boss;


    //getters & setters
    public Vector3Int CurrentIndex {get=>currentIndex; private set=>currentIndex = value;}
    public Vector3Int MovingDestination {get=>movingDestination; private set=>movingDestination = value;}
    public PlayerStats Stats {get=>playerStats;}

    private PlayerStateBase currentState;
    public PlayerStatePrepare statePrepare = new PlayerStatePrepare();
    public PlayerStateMove stateMove = new PlayerStateMove();
    public PlayerStateUpgrade stateUpgrade = new PlayerStateUpgrade();
    public void changeState(PlayerStateBase newState) {
        if (newState != currentState) {
            if (currentState != null)
            {
                currentState.LeaveState(this);
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("steps", 0);
        pathGenerator = FindObjectOfType<PathGenerator>();
        upgradeControl = FindObjectOfType<UpgradeControl>();
        boss = FindObjectOfType<Boss>();
        CurrentIndex = new Vector3Int(pathGenerator.Map.GetLength(0)/2, 0);
        MovingDestination = new Vector3Int(CurrentIndex.x, 1);

        playerStats.Health = playerStats.MaxHealth;

        changeState(statePrepare);
    }

    
    void Update()
    {
        currentState.UpdateState(this);
        if (Stats.Health <= 0)
            SceneManager.LoadScene("Ending");
    }

    /* call pathGenerator's drawIndicator() function */
    public void drawIndicator() {
        pathGenerator.drawIndicator();
    }

    /* select next moving positiong by using arrow keys */
    public void selectMovingDestination() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (pathGenerator.checkWalkable(MovingDestination + Vector3Int.left)) {
                pathGenerator.deleteIndicator(MovingDestination);
                MovingDestination += Vector3Int.left;
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (pathGenerator.checkWalkable(MovingDestination + Vector3Int.right)) {
                pathGenerator.deleteIndicator(MovingDestination);
                MovingDestination += Vector3Int.right;
            }
        }
    }

    /* move to movingDestination */
    public void move() {
        CurrentIndex = MovingDestination;
        pathGenerator.Map[CurrentIndex.x, CurrentIndex.y].Type = TileType.Player;
        PlayerPrefs.SetInt("steps", PlayerPrefs.GetInt("steps")+1);
    }

    /* react to the unit of current pos based on their TileType */
    public void react(Vector3Int index) {
        switch (pathGenerator.Map[index.x, index.y].Type) {
            case TileType.Experience:
                playerStats.Experience++;
                if (playerStats.Experience >= mapData.levelUpRequirements[Mathf.Min(playerStats.Level, mapData.levelUpRequirements.Length-1)]) {
                    playerStats.Level++;
                    playerStats.Experience = 0;
                    playerStats.IsLeveledUp = true;
                    upgradeControl.randomlySetUpgrades();
                    upgradeControl.showUpgradeInfo(0);
                }
                break;
            case TileType.Health:
                playerStats.Health = Mathf.Min(playerStats.Health+playerStats.HealthRegeneration, playerStats.MaxHealth);
                break;
            case TileType.Enemy:
                playerStats.Health--;
                break;
            case TileType.Zombie:
                playerStats.Health -= Mathf.Max(0,mapData.zombieDamage - playerStats.ZombieResistence);
                break;
            case TileType.Rat:
                playerStats.Health -= Mathf.Max(0,mapData.ratDamage - playerStats.RatResistence);
                break;
            case TileType.IronDummy:
                playerStats.Health -= Mathf.Max(0,mapData.ironDummyDamage - playerStats.IronDummyResistence);
                break;
            case TileType.Boss:
                playerStats.Health -= boss.Damage;
                //after defeating several bosses
                if (boss.BossLevel >= 5)
                    SceneManager.LoadScene("Ending");
                else
                    boss.IsBossEliminated = true;
                break;
        }
    }

    public void levelUp() {
        if (playerStats.IsLeveledUp) {
            upgradeControl.showUpgradeIcons();
            upgradeControl.enableUpgradeButtons();
        } else {
            upgradeControl.disableUpgradeButtons();
        }
    }

    public void navigatingUpgrades() {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            upgradeControl.selectNextUpgrade();
        if (Input.GetKeyDown(KeyCode.UpArrow))
            upgradeControl.selectPreviousUpgrade();
    }

    public void upgrade() {
        if (playerStats.IsLeveledUp)
            upgradeControl.useCurrentUpgrade();
    }
    
    public void summonBoss() {
        if (playerStats.Level >= mapData.bossAppearLevel[boss.BossLevel] && boss.IsBossEliminated) 
            pathGenerator.drawBoss();
    }
}
