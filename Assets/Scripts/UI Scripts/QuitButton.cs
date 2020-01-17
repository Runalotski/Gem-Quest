using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{

    public static long score = 0;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Quit"))
            Application.Quit();

        if (GUI.Button(new Rect(10, 70, 300, 50), "Score : " + score + (score > 0 ? " M" : ""))) ;
    }
}
