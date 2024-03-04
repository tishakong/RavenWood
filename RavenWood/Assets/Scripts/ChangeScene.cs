using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeScenefunc()
    {
        switch (this.gameObject.name)
        {
            case "StartBtn":
                SceneManager.LoadScene(1);
                break;

            case "RestartBtn": 
                SceneManager.LoadScene(0);
                break;
            case "ExitBtn":
                Application.Quit();
                break;
        }
    }
}
