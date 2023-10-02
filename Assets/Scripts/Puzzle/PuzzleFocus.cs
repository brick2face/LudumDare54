using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleFocus : MonoBehaviour
{
    public Image PuzzleFocusBackground;
    bool PuzzleStarted = false;

    public GameObject Puzzle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PuzzleStarted == true)
        {
            PuzzleFocusBackground.enabled = true;
        }
    }
    public void StartPuzzle()
    {
        PuzzleStarted = true;
        
    }

    public void Win()
    {
        Puzzle.GetComponent<InteractableObject>().ShouldSetGameStoryVariableOnInteract = true;
    }
    public void RestartPuzzle()
    {
        
    }
}
