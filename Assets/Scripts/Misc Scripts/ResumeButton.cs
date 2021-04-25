using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ResumeButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick()
    {
        PauseMenu.isPaused = false;
    }
}
