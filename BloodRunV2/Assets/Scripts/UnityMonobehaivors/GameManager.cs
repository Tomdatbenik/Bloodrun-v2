using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Game game;

    #region DoNotDestroy
    /// <summary>Static reference to the instance of our DataManager</summary>
    public static GameManager instance;

    /// <summary>Awake is called when the script instance is being loaded.</summary>
    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }
    #endregion

}
