using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/* Boss Class is responsible for boss's place to spawn based on the recursive function that calculate player's best path,
   and damage the boss should make based on how many times player defeats the boss.*/
public class Boss : MonoBehaviour
{
    private PlayerControl player;
    private PathGenerator pathGenerator;
    [SerializeField] private FixedData mapData;
    private int damage = 3;
    private Dictionary<Vector3Int, float> playerDestinations = new Dictionary<Vector3Int, float>();
    private bool isBossEliminated = true;
    private int bossLevel = 0;
    private int bossYIndex = 1;

    public bool IsBossEliminated {get=>isBossEliminated; set=> isBossEliminated = value;}
    public int Damage {get=>damage; private set=> damage = value;}
    public int BossLevel {get=>bossLevel; private set=> bossLevel = value;}
    public int BossYIndex {get=>bossYIndex; set=> bossYIndex = value;}
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        pathGenerator = FindObjectOfType<PathGenerator>();
    }

    /* return the best destination that player can go, so that either player get good stuff and then goes into the boss or get worse stuff but avoid the boss
       the function will return the path that has the best score
       if two path has the same score, return the path that its final destination closer to the center lane */
    public Vector3Int findBestDestination(int depth, Vector3Int startingIndex) {
        playerDestinations.Clear();
        findBestDestination(depth, startingIndex, 0);
        if (playerDestinations.Count > 0) {
        Vector3Int currentDestination = playerDestinations.First().Key;
        foreach (Vector3Int destination in playerDestinations.Keys) {
            if (playerDestinations[destination] > playerDestinations[currentDestination])
                currentDestination = destination;
            else if (playerDestinations[destination] == playerDestinations[currentDestination])
                if (Mathf.Abs(destination.x - pathGenerator.Map.GetLength(0)/2) < Mathf.Abs(currentDestination.x - pathGenerator.Map.GetLength(0)/2))
                    currentDestination = destination;
        }
        return currentDestination;
        }
        return Vector3Int.zero;
    }

    /* a recursice function that trying to find all possible path player can take in the next couple moves (depth),
       each move will have a score,
       since there will multiple same final destination, the score will be the highest one*/
    public void findBestDestination(int depth, Vector3Int currentIndex, float score) {
        if (depth <= 0) {
            if (!playerDestinations.ContainsKey(currentIndex))
                playerDestinations.Add(currentIndex, score);
            else
                if (playerDestinations[currentIndex] < score)
                    playerDestinations[currentIndex] = score;
        } else {
            depth -= 1;
            Vector3Int nextDestination;
            //left
            nextDestination = currentIndex + new Vector3Int(-1,1,0);
            if (pathGenerator.checkWalkable(nextDestination)) {
                findBestDestination(depth, nextDestination, score + playerReactScore(nextDestination));
            }
            
            //forward
            nextDestination = currentIndex + new Vector3Int(0,1,0);
            if (pathGenerator.checkWalkable(nextDestination)) {
                findBestDestination(depth, nextDestination, score + playerReactScore(nextDestination));
            }

            //right
            nextDestination = currentIndex + new Vector3Int(1,1,0);
            if (pathGenerator.checkWalkable(nextDestination)) {
                findBestDestination(depth, nextDestination, score + playerReactScore(nextDestination));
            }
        }
        
    }

    /* return a score player will get from going to a specific position */
    public float playerReactScore(Vector3Int predictDestination) {
        switch (pathGenerator.Map[predictDestination.x, predictDestination.y].Type) {
            case TileType.Experience:
                return 1;
            case TileType.Health:
                //benefit from taking health potion is depending on how much health player left
                if (player.Stats.Health <= 2)
                    return 2;
                return 1.0f/(player.Stats.Health - 1);
            case TileType.Enemy:
                return -mapData.enemyDamage;
            case TileType.Rat:
                return -(mapData.ratDamage - player.Stats.RatResistence);
            case TileType.Zombie:
                return -(mapData.zombieDamage - player.Stats.ZombieResistence);
            case TileType.IronDummy:
                return -(mapData.ironDummyDamage - player.Stats.IronDummyResistence);
            default:
                return 0;
        }
    }

    /* boss will appear at the return position (the "best" position player will go) in the tilemap,
       each time a new boss come, its damage will increase */
    public Vector3Int bossPosition() {
        if (IsBossEliminated) {
            IsBossEliminated = false;
            Damage += 3;
            BossLevel++;
        }
        return findBestDestination(3, player.currentIndex);
    }
}
