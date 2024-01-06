using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    private int _mazeWidth;
    public int mazeWidth
    {
        get
        {
            return _mazeWidth;
        }
        set
        {
            _mazeWidth = value;
        }
    }


    private int _mazeDepth;
    public int mazeDepth
    {
        get
        {
            return _mazeDepth;
        }
        set
        {
            _mazeDepth = value; 
        }
    }

    private MazeCell[,] _mazeGrid;

    //Line 20: Changed the traditional void of the Start method into an IEnumerator to generate the 
    public IEnumerator StartMaze()
    {
        Debug.Log(_mazeWidth + " " + _mazeDepth);
        //Lines 23 - 31: Generation of the maze grid based on _mazeWidth and _mazeDepth
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
        //Line 33: Starts the Maze generation from MazeCell 0, 0 in the MazeGrid. As at the start, there is no previousCell yet, there's a null given in this parameter.
        yield return GenerateMaze(null, _mazeGrid[0, 0]);
    }


    private IEnumerator GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        //Line 39 - 59: So long as there are unvisited cells in the CellGrid, this IEnumerator keeps visiting nearby unvisited cells.
        currentCell.Visit();
        //Uses the ClearWalls method to shape the now visited cell relative to how the generator approaches the now current cell from the previous cell.
        ClearWalls(previousCell, currentCell);
        //Waits 0.01 seconds before moving on the next part of the method. This creates the effect of the maze generating on screen instead of instantanious.
        yield return new WaitForSeconds(0.01f);

        MazeCell nextCell;
        
        do
        {
            //tries to look for the nearest unvisited cells relative to the currentCell.
            nextCell = GetNextUnvisitedCell(currentCell);

            //if an unvisited Cell has been found, Callback.
            if (nextCell != null)
            {
                yield return GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        //Checks all unvisited cells left in the grid.
        IEnumerable<MazeCell> unvisitedCells = GetUnvisitedCells(currentCell);
        //shuffles the IEnumerable and returns the first MazeCell in the list.
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        //sets the current cell's position into variables.
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        //checks if the X position to the right is within the Grid
        if (x + 1 < _mazeWidth)
        {
            MazeCell cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }
        //checks if the X position to the left is within the Grid
        if (x - 1 >= 0)
        {
            MazeCell cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }
        //checks if the Z position to the front is within the Grid
        if (z + 1 < _mazeDepth)
        {
            MazeCell cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }
        //checks if the X position to the back is within the Grid
        if (z - 1 >= 0)
        {
            MazeCell cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        //First checks if there even is a previous cell.
        if (previousCell == null)
        {
            return;
        }

        //Line 129 - 156: checks what position the currentCell is from the previousCell and uses Methods from MazeCell.cs to disable the proper parts of the MazeCell to shape it accordingly.
        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}
