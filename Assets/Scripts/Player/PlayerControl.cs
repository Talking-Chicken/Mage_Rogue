using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    public Vector3Int currentIndex;
    private Vector3Int movingDestination;

    private PathGenerator pathGenerator;

    //player stats
    private int health, experience;
    [SerializeField] private int maxHealth;

    //getters & setters
    public Vector3Int CurrentIndex {get=>currentIndex; private set=>currentIndex = value;}
    public Vector3Int MovingDestination {get=>movingDestination; private set=>movingDestination = value;}
    public int Health {get=>health; set=>health = value;}
    public int MaxHealth {get=>maxHealth;}
    public int Experience {get=>experience; set=>experience = value;}

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
        pathGenerator = FindObjectOfType<PathGenerator>();
        CurrentIndex = new Vector3Int(pathGenerator.Map.GetLength(0)/2, 0);
        MovingDestination = new Vector3Int(CurrentIndex.x, 1);

        Health = maxHealth;

        changeState(statePrepare);
    }

    
    void Update()
    {
        currentState.UpdateState(this);
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
        pathGenerator.setUnitToEmpty(currentIndex);
        CurrentIndex = MovingDestination;
        pathGenerator.Map[CurrentIndex.x, CurrentIndex.y].Type = TileType.Player;
    }

    /* react to the unit of current pos based on their TileType */
    public void react(Vector3Int index) {
        switch (pathGenerator.Map[index.x, index.y].Type) {
            case TileType.Experience:
                Experience++;
                break;
            case TileType.Health:
                Health = Mathf.Min(Health+1, maxHealth);
                break;
            case TileType.Enemy:
                Health--;
                break;
        }
    }
}
