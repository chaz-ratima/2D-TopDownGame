using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EndEditorRun : MonoBehaviour
{

    public void GameEnd()
    {
        EditorApplication.isPlaying = false;
    }
}
