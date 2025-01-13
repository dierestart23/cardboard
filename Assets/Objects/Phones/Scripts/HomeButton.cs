using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public AppOpener openedApp;

    public void CloseApp()
    {
        if(openedApp != null)
            openedApp.activateTransition = false;
    }
}
