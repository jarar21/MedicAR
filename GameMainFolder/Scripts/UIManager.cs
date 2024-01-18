using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
public class UIManager : MonoBehaviour
{
    FirebaseUser User;

    bool isClicked;
    public void LoadScene(string name)
    {
        if (!isClicked)
        {
            isClicked = true;
            SceneManager.LoadScene(name);
        }
    }

    public void Logout()
    {
        User = null;

        LoadScene("LogicScreen");
    }
}
