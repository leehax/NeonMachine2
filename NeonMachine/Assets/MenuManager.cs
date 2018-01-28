using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

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

    [SerializeField]
    Transform controlsCanvas;

    [SerializeField]
    List<Button> controlsButtons;

    bool ply1CanReady;
    bool ply2CanReady;

    [SerializeField]
    Vector2 startBtnSelectedSize;
    Vector2 startBtnInitialSize;
    RectTransform startBtnRT;
    [SerializeField]
    Vector2 creditSelectedBtnSize;
    Vector2 creditBtnInitialSize;
    RectTransform creditsBtnRT;
    RectTransform controlsBtnRT;

    [SerializeField]
    SpriteRenderer ply1TextSR;
    [SerializeField]
    SpriteRenderer ply1ImageSR;
    [SerializeField]
    Sprite ply1TextReadySprite;
    Sprite ply1TextNotReadySprite;
    [SerializeField]
    Sprite ply1ImageReadySprite;
    Sprite ply1ImageNotReadySprite;

    [SerializeField]
    SpriteRenderer ply2TextSR;
    [SerializeField]
    SpriteRenderer ply2ImageSR;
    [SerializeField]
    Sprite ply2TextReadySprite;
    Sprite ply2TextNotReadySprite;
    [SerializeField]
    Sprite ply2ImageReadySprite;
    Sprite ply2ImageNotReadySprite;

    [SerializeField]
    float readyDelay;
    float readyTime;
    bool counting;

    [SerializeField]
    GameObject ripple;
    [SerializeField]
    float rippleLifeTime;
    [SerializeField]
    float rippleDistortion;

    void Start()
    {
        //TogglePlayer2ReadyStatus();
        targetPos = cam.transform.position;

        startBtnRT = mainButtons[0].gameObject.GetComponent<RectTransform>();
        creditsBtnRT = mainButtons[1].gameObject.GetComponent<RectTransform>();
        controlsBtnRT = mainButtons[2].gameObject.GetComponent<RectTransform>();
        

        startBtnInitialSize = startBtnRT.sizeDelta;
        creditBtnInitialSize = creditsBtnRT.sizeDelta;


        ply1TextNotReadySprite = ply1TextSR.sprite;
        ply1ImageNotReadySprite = ply1ImageSR.sprite;

        ply2TextNotReadySprite = ply2TextSR.sprite;
        ply2ImageNotReadySprite = ply2ImageSR.sprite;

        TogglePlayer2ReadyStatus();
    }

    void Update()
    {
        if (player1Ready && player2Ready)
        {
            if(!counting)
            {
                GameObject go = Instantiate(ripple);
                go.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -2);
                Ripple r = go.GetComponent<Ripple>();
                r.ttl = rippleLifeTime;
                r.startDistortion = rippleDistortion;
                r.endRadius = 2.5f;
                
                counting = true;

                readyTime = Time.time + readyDelay;
            }
            else
            {
                if(Time.time > readyTime)
                {
                    SceneManager.LoadScene(gameSceneName);
                }
            }
        }
        else if(counting)
        {
            counting = false;
        }

        if (cam.transform.position != targetPos)
        {
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPos, ref velocity, smoothTime);
        }

        if (isReadyScreen && Vector3.Distance(cam.transform.position, targetPos) < 0.1f)
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
        }
        else if (Input.GetAxis("ThrusterGamePad0") <= 0.0f && !ply1CanReady)
        {
            ply1CanReady = true;
        }

        if (Input.GetAxis("ThrusterGamePad1") > 0.0f && ply2CanReady)
        {
            ply2CanReady = false;
            TogglePlayer2ReadyStatus();
        }
        else if (Input.GetAxis("ThrusterGamePad1") <= 0.0f && !ply2CanReady)
        {
            ply2CanReady = true;
        }
    }

    public void TogglePlayer1ReadyStatus()
    {
        player1Ready = !player1Ready;

        ply1TextSR.sprite = player1Ready ? ply1TextReadySprite : ply1TextNotReadySprite;
        ply1ImageSR.sprite = player1Ready ? ply1ImageReadySprite : ply1ImageNotReadySprite;
        /*readyButtons[0].GetComponentInChildren<Text>().text = player1Ready ? "Ready" : "Not Ready";

        ColorBlock cb = readyButtons[0].colors;
        cb.highlightedColor = player1Ready ? Color.green : Color.red;
        readyButtons[0].colors = cb;*/
    }

    public void TogglePlayer2ReadyStatus()
    {
        player2Ready = !player2Ready;

        ply2TextSR.sprite = player2Ready ? ply2TextReadySprite : ply2TextNotReadySprite;
        ply2ImageSR.sprite = player2Ready ? ply2ImageReadySprite : ply2ImageNotReadySprite;

        //readyButtons[1].GetComponentInChildren<Text>().text = player2Ready ? "Ready" : "Not Ready";
    }

    public void StartBtnHoverEnter()
    {
        startBtnRT.sizeDelta = startBtnSelectedSize;
    }

    public void StartBtnHoverExit()
    {
        startBtnRT.sizeDelta = startBtnInitialSize;
    }

    public void CreditBtnHoverEnter()
    {
        creditsBtnRT.sizeDelta = creditSelectedBtnSize;
    }

    public void CreditBtnHoverExit()
    {
        creditsBtnRT.sizeDelta = creditBtnInitialSize;
    }

    public void ControlsBtnHoverEnter()
    {
        controlsBtnRT.sizeDelta = startBtnSelectedSize;
    }

    public void ControlsBtnHoverExit()
    {
        controlsBtnRT.sizeDelta = startBtnInitialSize;
    }

    public void MovetoMainScreen()
    {
        targetPos = new Vector3(mainCanvas.position.x, mainCanvas.position.y, cam.transform.position.z);

        foreach (Button b in mainButtons)
        {
            b.interactable = true;
        }
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
        if (player1Ready)
        {
            TogglePlayer1ReadyStatus();
        }

        if (player2Ready)
        {
            TogglePlayer2ReadyStatus();
        }

        isReadyScreen = false;
        foreach (Button b in readyButtons)
        {
            b.interactable = false;
        }

        EventSystem.current.SetSelectedGameObject(mainButtons[0].gameObject);
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

        EventSystem.current.SetSelectedGameObject(mainButtons[1].gameObject);
    }

    public void MovetoControlsScreen()
    {
        targetPos = new Vector3(controlsCanvas.position.x, controlsCanvas.position.y, cam.transform.position.z);

        foreach (Button b in creditsButtons)
        {
            b.interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(controlsButtons[0].gameObject);
    }

    public void InactivateControlsButtons()
    {
        foreach (Button b in controlsButtons)
        {
            b.interactable = false;
        }

        EventSystem.current.SetSelectedGameObject(mainButtons[2].gameObject);
    }
}
