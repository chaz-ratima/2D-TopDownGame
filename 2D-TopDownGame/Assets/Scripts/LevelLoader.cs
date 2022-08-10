using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string myScene;
    private bool interactStairs;
    private bool canLoadNextScene;
    public void Update()
    {
        interactStairs = Input.GetKeyDown(KeyCode.E);

        LoadLevel();
    }

    public void LoadLevel()
    {
        if (interactStairs && canLoadNextScene)
        {
            SceneManager.LoadScene(myScene);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canLoadNextScene = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canLoadNextScene = false;
        }
    }
}
