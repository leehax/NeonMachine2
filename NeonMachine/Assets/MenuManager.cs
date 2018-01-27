using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

    bool player1Ready;
    bool player2Ready;

    [SerializeField]
    Camera cam;

    [SerializeField]
    string gameSceneName;

    [SerializeField]
    float smoothTime;
    Vector3 velocity;
    Vector3 targetPos;

    [SerializeField]
    Transform mainCanvas;

    [SerializeField]
    List<Button> mainButtons;

    [SerializeField]
    Transform readyCanvas;

    [SerializeField]
    List<Button> readyButtons;
    bool isReadyScreen;

    [SerializeField]
    Transform creditsCanvas;

    [SerializeField]
    List<Button> creditsButtons;

    bool ply1CanReady;
    bool ply2CanReady;

    void Start ()
    {
        //TogglePlayer2ReadyStatus();
        targetPos = cam.transform.position;
    }
	
	void Update ()
    {
        if(player1Ready && player2Ready)
        {
            SceneManager.LoadScene(gameSceneName);
        }

        if (cam.transform.position != targetPos)
        {
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPos, ref velocity, smoothTime);
        }
        else if (isReadyScreen)
        {
            ReadyScreenInput();
        }
    }

    private void ReadyScreenInput()
    {
        if (Input.GetAxis("ThrusterGamePad0") > 0.0f && ply1CanReady)
        {
            ply1CanReady = false;
            TogglePlayer1ReadyStatus();
            print(player1Ready);
        }
        else if (Input.GetAxis("ThrusterGamePad0") <= 0.0f && !ply1CanReady)
        {
            ply1CanReady = true;
        }

        if (Input.GetAxis("ThrusterGamePad1") > 0.0f && ply2CanReady)
        {
            ply2CanReady = false;
            TogglePlayer2ReadyStatus();
            print(player2Ready);
        }
        else if (Input.GetAxis("ThrusterGamePad1") <= 0.0f && !ply2CanReady)
        {
            ply2CanReady = true;
        }
    }

    public void TogglePlayer1ReadyStatus()
    {
        player1Ready = !player1Ready;

        /*readyButtons[0].GetComponentInChildren<Text>().text = player1Ready ? "Ready" : "Not Ready";

        ColorBlock cb = readyButtons[0].colors;
        cb.highlightedColor = player1Ready ? Color.green : Color.red;
        readyButtons[0].colors = cb;*/
    }

    public void TogglePlayer2ReadyStatus()
    {
        player2Ready = !player2Ready;

        //readyButtons[1].GetComponentInChildren<Text>().text = player2Ready ? "Ready" : "Not Ready";
    }

    public void MovetoMainScreen()
    {
        targetPos = new Vector3(mainCanvas.position.x, mainCanvas.position.y, cam.transform.position.z);

        foreach (Button b in mainButtons)
        {
            b.interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(mainButtons[0].gameObject);
    }

    public void InactivateMainButtons()
    {
        foreach (Button b in mainButtons)
        {
            b.interactable = false;
        }
    }

    public void MovetoReadyScreen()
    {
        isReadyScreen = true;
        targetPos = new Vector3(readyCanvas.position.x, readyCanvas.position.y, cam.transform.position.z);

        foreach (Button b in readyButtons)
        {
            b.interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(readyButtons[0].gameObject);
    }

    public void InactivateReadyButtons()
    {
        player1Ready = false;
       // player2Ready = false;
        isReadyScreen = false;
        foreach (Button b in readyButtons)
        {
            b.interactable = false;
        }
    }

    public void MovetoCreditsScreen()
    {
        targetPos = new Vector3(creditsCanvas.position.x, creditsCanvas.position.y, cam.transform.position.z);

        foreach (Button b in creditsButtons)
        {
            b.interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(creditsButtons[0].gameObject);
    }

    public void InactivateCreditsButtons()
    {
        foreach (Button b in creditsButtons)
        {
            b.interactable = false;
        }
    }
}
