using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSceneManager : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }
}
