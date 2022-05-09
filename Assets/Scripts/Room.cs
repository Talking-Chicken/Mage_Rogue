using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    private int width, height, doorCount;
    private RectInt rect;
    private List<Vector3Int> bound; //perimeter of the entrie Room
    private List<Vector3Int> bottomBound = new List<Vector3Int>(), //bottom side without corners
                                leftBound = new List<Vector3Int>(), //left side without corners
                                rightBound = new List<Vector3Int>(), //right side without corners
                                topBound = new List<Vector3Int>(), //top side without corners
                                corners,
                                verticalWalls = new List<Vector3Int>(),
                                horizontalWalls = new List<Vector3Int>(),
                                topLeftCorner = new List<Vector3Int>(),
                                topRightCorner = new List<Vector3Int>(),
                                bottomLeftCorner = new List<Vector3Int>(),
                                bottomRightCorner = new List<Vector3Int>(),
                                allTiles = new List<Vector3Int>();

    //getters & setters
    public RectInt Rect {get => rect; set => rect = value;}
    public List<Vector3Int> Bound {get => bound; private set => bound = value;}
    public List<Vector3Int> BottomBound {get => bottomBound; private set => bottomBound = value;}
    public List<Vector3Int> TopBound {get => topBound; private set => topBound = value;}
    public List<Vector3Int> LeftBound {get => leftBound; private set => leftBound = value;}
    public List<Vector3Int> RightBound {get => rightBound; private set => rightBound = value;}
    public List<Vector3Int> Corners {get => corners; private set => corners = value;}
    public List<Vector3Int> VerticalWalls {get => verticalWalls; private set => verticalWalls = value;}
    public List<Vector3Int> HorizontalWalls {get => horizontalWalls; private set => horizontalWalls = value;}


    public Room(int width, int height, Vector3Int startPos) {
        this.width = width;
        this.height = height;
        rect = new RectInt(startPos.x, startPos.y, width, height);
        Bound = getBounds();
        Corners = new List<Vector3Int> {new Vector3Int(Rect.xMin, Rect.yMin),
                                        new Vector3Int(Rect.xMax-1, Rect.yMin),
                                        new Vector3Int(Rect.xMin, Rect.yMax-1),
                                        new Vector3Int(Rect.xMax-1, Rect.yMax-1)};
        for (int x = Rect.xMin+1; x < Rect.xMax; x++) {
            TopBound.Add(new Vector3Int(x, Rect.yMin));
            BottomBound.Add(new Vector3Int(x, Rect.yMax-1));
        }
        horizontalWalls.AddRange(TopBound);
        horizontalWalls.AddRange(BottomBound);

        for (int y = Rect.yMin+1; y < Rect.yMax; y++) {
            LeftBound.Add(new Vector3Int(Rect.xMin, y));
            RightBound.Add(new Vector3Int(Rect.xMax-1, y));
        }
        verticalWalls.AddRange(LeftBound);
        verticalWalls.AddRange(RightBound);

        for (int x = Rect.xMin; x < Rect.xMax; x++) {
            for (int y = Rect.yMin; y < Rect.yMax; y++) {
                allTiles.Add(new Vector3Int(x,y));
            }
        }

        categorizeCorners();
    }

    public Vector3Int randomDoorPos() {
        return getBoundsButNotConors()[Random.Range(0, bound.Count)];
    }

    /* return a list of Vector3Int that is the perimeter of the rectangle */
    public List<Vector3Int> getBounds() {
        List<Vector3Int> bound = new List<Vector3Int>();
        for (int x = Rect.xMin; x < Rect.xMax; x++) {
            bound.Add(new Vector3Int(x, Rect.yMin));
            bound.Add(new Vector3Int(x, Rect.yMax-1));
        }

        for (int y = Rect.yMin; y < Rect.yMax; y++) {
            bound.Add(new Vector3Int(Rect.xMin, y));
            bound.Add(new Vector3Int(Rect.xMax-1, y));
        }
        Debug.Log("bound has " + bound.Count);
        return bound;
    }

    /* find all corners of the room then categorize corners into four categories */ 
    public void categorizeCorners() {
        foreach (Vector3Int wall in allTiles) {
            if (Bound.Contains(wall+Vector3Int.down)) {
                if (Bound.Contains(wall+Vector3Int.right))
                    topLeftCorner.Add(wall);
                if (Bound.Contains(wall+Vector3Int.left))
                    topRightCorner.Add(wall);
            }
            if (Bound.Contains(wall+Vector3Int.up)) {
                if (Bound.Contains(wall+Vector3Int.right))
                    bottomLeftCorner.Add(wall);
                if (Bound.Contains(wall+Vector3Int.left))
                    bottomRightCorner.Add(wall);
            }
        }
        Debug.Log(topLeftCorner[0] + "\n" + topLeftCorner[1]);
    }

    /* return all vector3Int positions that lying on the perimeter of the rectangle, but not including four conors */
    public List<Vector3Int> getBoundsButNotConors() {
        List<Vector3Int> boundWithoutConors = Bound;
        Vector3Int[] cornorsPos = new Vector3Int[] {new Vector3Int(Rect.xMin, Rect.yMin),
                                                    new Vector3Int(Rect.xMax-1, Rect.yMin),
                                                    new Vector3Int(Rect.xMin, Rect.yMax-1),
                                                    new Vector3Int(Rect.xMax-1, Rect.yMax-1)};

        List<Vector3Int> cornors = new List<Vector3Int>();

        //remove all cornors
        foreach (Vector3Int cell in boundWithoutConors) {
            foreach (Vector3Int cornor in cornorsPos)
                if (cell.Equals(cornor))
                    cornors.Add(cornor);
        }
        foreach (Vector3Int cornor in cornors) {
            boundWithoutConors.Remove(cornor);
        }

        return boundWithoutConors;
    }
}

