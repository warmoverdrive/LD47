using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /* 
    // SCENE REFERENCE //
    Main Menu   : 0
    Tutorial    : 1
    Credits     : 2
    Game        : 3
    */
    public void MainMenu() { SceneManager.LoadScene(0); }
    public void Tutorial() { SceneManager.LoadScene(1); }
    public void Credits() { SceneManager.LoadScene(2); }
    public void Game() { SceneManager.LoadScene(3); }
    public void Quit() { Application.Quit(); }
}
