using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrientationManager : MonoBehaviour
{
    private void Awake()
    {
        
        Scene _scene = SceneManager.GetActiveScene();

        if (_scene.name == "3DViewer")
            Screen.orientation = ScreenOrientation.LandscapeLeft;

        else
            Screen.orientation = ScreenOrientation.Portrait;

          
    }
}
