using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic
{
    public void LoadScene(string sceneName)
    {

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
