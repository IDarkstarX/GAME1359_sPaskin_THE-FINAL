using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pressedStart()
    {
        goToScene(1);
    }

    public void pressedQuit()
    {
        Application.Quit();
    }
    public void goToScene(int s)
    {
        SceneManager.LoadScene(s);
    }
}
