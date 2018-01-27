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
    string gameSceneName;

    [SerializeField]
    float smoothTime;
    Vector3 velocity;

    [SerializeField]
    Transform mainCanvas;
    [SerializeField]
    Transform readyCanvas;
    [SerializeField]
    Camera cam;

    Vector3 targetPos;

    [SerializeField]
    List<Button> mainButtons;

    [SerializeField]
    List<Button> readyButtons;

    void Start ()
    {
        TogglePlayer2ReadyStatus();
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
    }

    public void TogglePlayer1ReadyStatus()
    {
        player1Ready = !player1Ready;
    }

    public void TogglePlayer2ReadyStatus()
    {
        player2Ready = !player2Ready;
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
        targetPos = new Vector3(readyCanvas.position.x, readyCanvas.position.y, cam.transform.position.z);

        foreach (Button b in readyButtons)
        {
            b.interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(readyButtons[0].gameObject);
    }

    public void InactivateReadyButtons()
    {
        foreach (Button b in readyButtons)
        {
            b.interactable = false;
        }
    }

    public void Test()
    {
        print("hi");
    }
}
