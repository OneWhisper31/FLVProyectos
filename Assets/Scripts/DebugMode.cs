using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMode : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LoadScene(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            LoadScene(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            LoadScene(2);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        GameManager.Instance.pauseMode = false;
    }
}
