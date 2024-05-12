using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AlternateRouteButton : MonoBehaviour
{
    public Button button;
    public int IndexOfAlternateLevel;
    
    private void Start()
    {
        button.onClick.AddListener(SwitchScenes);
    }

    public void SwitchScenes()
    {
        SceneManager.LoadScene(IndexOfAlternateLevel);
    }
}