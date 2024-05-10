using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SkipButton : MonoBehaviour
{
    public Button button;
    private void Start()
    {
        button.onClick.AddListener(SwitchScenes);
    }

    public void SwitchScenes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
