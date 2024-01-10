using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private MazeGenerator _mazeGenerator;

    private GameObject _uiHeader;

    private Slider _mazeWidthSlider;
    private TMP_Text _mazeWidthText;

    private Slider _mazeDepthSlider;
    private TMP_Text _mazeDepthText;

    private Button _generateMazeButton;
    private GameObject _regenerateMazeButton;


    private void Start()
    {
        _mazeGenerator = GameObject.Find("Maze Generator").GetComponent<MazeGenerator>();

        _uiHeader = GameObject.Find("UI Header");

        _mazeWidthSlider = GameObject.Find("Maze Width Slider").GetComponent<Slider>();
        _mazeWidthText = GameObject.Find("Maze Width Text").GetComponent<TMP_Text>();

        _mazeDepthSlider = GameObject.Find("Maze Depth Slider").GetComponent<Slider>();
        _mazeDepthText = GameObject.Find("Maze Depth Text").GetComponent<TMP_Text>();

        _generateMazeButton = GameObject.Find("UI Generate Maze Button").GetComponent<Button>();
        _regenerateMazeButton = GameObject.Find("UI Regenerate Maze Button");

        _generateMazeButton.onClick.AddListener(OnGenerateMazeButtonClick);
        _regenerateMazeButton.GetComponent<Button>().onClick.AddListener(OnRegenerateMazeButtonClick);
        _regenerateMazeButton.SetActive(false);
    }

    private void OnGUI()
    {
        _mazeWidthText.text = _mazeWidthSlider.value.ToString();
        _mazeDepthText.text = _mazeDepthSlider.value.ToString();
    }

    private void OnGenerateMazeButtonClick()
    {
        _mazeGenerator.mazeWidth = (int)_mazeWidthSlider.value;
        _mazeGenerator.mazeDepth = (int)_mazeDepthSlider.value;
        //Debug.Log(_mazeWidthSlider.value + " " + _mazeDepthSlider.value);
        //StartCoroutine(_mazeGenerator.StartMaze());
        _mazeGenerator.StartMaze();
        _uiHeader.SetActive(false);
        _regenerateMazeButton.SetActive(true);
    }

    private void OnRegenerateMazeButtonClick()
    {
        _regenerateMazeButton.SetActive(false);
        _uiHeader.SetActive(true);
    }
}
