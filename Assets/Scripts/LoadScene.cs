using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void loadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
