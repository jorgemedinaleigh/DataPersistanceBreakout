using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // switch scenes
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
