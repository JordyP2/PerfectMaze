using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    //Lines 7 - 17: Storing all the walls of a MazeCell in vars to use later.
    [SerializeField]
    private GameObject _leftWall;
    [SerializeField]
    private GameObject _rightWall;
    [SerializeField]
    private GameObject _frontWall;
    [SerializeField]
    private GameObject _backWall;
    [SerializeField]
    private GameObject _unvisitedBlock;

    public bool IsVisited { get; private set; }

    //Lines 22 - 42: Methods to set walls of the MazeCell inactive to shape the MazeCell appropriately based on the generation of MazeGenerator.cs in which the methods get used.
    public void Visit()
    {
        IsVisited = true;
        _unvisitedBlock.SetActive(false);
    }
    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }
    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }
    public void ClearFrontWall()
    {
        _frontWall.SetActive(false);
    }
    public void ClearBackWall()
    {
        _backWall.SetActive(false);
    }

    public void ResetBlock()
    {
        if (IsVisited == true)
        {
            _unvisitedBlock.SetActive(true);

            if (_leftWall.activeSelf == false)
            _leftWall.SetActive(true);

            if (_rightWall.activeSelf == false)
                _rightWall.SetActive(true);
            
            if (_frontWall.activeSelf == false)
                _frontWall.SetActive(true);
            
            if (_backWall.activeSelf == false)
                _backWall.SetActive(true);

            IsVisited = false;
        }
    }
}
