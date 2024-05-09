using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathDiversion : MonoBehaviour
{
    // Read the comment below for clarification.
    public void PathA()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // This build index will be according to the order of left choice (ask manuel about the two routes)
    public void PathB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
