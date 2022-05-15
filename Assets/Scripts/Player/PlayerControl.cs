using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    private int currentIndexX;
    private int currentIndexY = 0;
    private Vector3Int movingDestination;

    private PathGenerator pathGenerator;
    //getters & setters
    public int CurrentIndexX {get=>currentIndexX; set=>currentIndexX = value;}
    public int CurrentIndexY {get=>currentIndexY; set=>currentIndexY = value;}
    public Vector3Int MovingDestination {get=>movingDestination; private set=>movingDestination = value;}

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
        CurrentIndexX = pathGenerator.Map.GetLength(0)/2;
        CurrentIndexY = 0;
        MovingDestination = new Vector3Int(CurrentIndexX, CurrentIndexY);
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
            if (pathGenerator.checkWalkable(MovingDestination.x-1, MovingDestination.y)) {
                pathGenerator.deleteIndicator(MovingDestination);
                MovingDestination = new Vector3Int(MovingDestination.x-1, movingDestination.y);
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (pathGenerator.checkWalkable(MovingDestination.x+1, MovingDestination.y)) {
                pathGenerator.deleteIndicator(MovingDestination);
                MovingDestination = new Vector3Int(MovingDestination.x+1, movingDestination.y);
            }
        }
    }
}
