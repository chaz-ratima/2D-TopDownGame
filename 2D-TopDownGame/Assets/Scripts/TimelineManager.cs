using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineManager : MonoBehaviour
{
    public GameObject cineCamera;
    public GameObject canvas;

    PlayableDirector myPlayDirector;

    public void Start()
    {
        myPlayDirector = GetComponent<PlayableDirector>();
    }

    public void DisableCinemachine()
    {
        cineCamera.SetActive(false);
    }

    public void ActivateCinemachine()
    {
        cineCamera.SetActive(true);
    }

    public void DisableCanvas()
    {
        canvas.SetActive(false);
    }

    public void ActivateCanvas()
    {
        canvas.SetActive(true);
    }

    public void CutscenePause()
    {
        myPlayDirector.Pause();
    }

    public void CutsceneResume()
    {
        myPlayDirector.Resume();
    }
}
