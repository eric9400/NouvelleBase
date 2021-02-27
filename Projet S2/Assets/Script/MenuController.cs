using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersioName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private GameObject MenuDebut;
    [SerializeField] private GameObject MenuResolution;


    [SerializeField] private InputField UsernameInput;
    [SerializeField] private InputField CreateGameInput;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton;



    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersioName);
    }

    private void Start()
    {
        Screen.SetResolution(Screen.width, Screen.height, false);
        PhotonNetwork.automaticallySyncScene = true;

    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 3)
            StartButton.SetActive(true);
        else
            StartButton.SetActive(false);
    }

    public void SetUserName()
    {
        UsernameMenu.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateGame()
    {
        if (CreateGameInput.text.Length > 0)
            PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 2}, null);
    }

    public void JoinGame()
    {
        if (JoinGameInput.text.Length > 0)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
        }
    }

    private void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
            PhotonNetwork.LoadLevel("LevelChoice");
    }

    public void PlayButton()
    {
        MenuDebut.SetActive(false);
        MenuResolution.SetActive(false);
    }


    public void ResolutionButton()
    {
        MenuDebut.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResolutionMin()
    {
        ChangeResolution(640,480);
    }

    public void ResolutionMiddle()
    {
        ChangeResolution(1280,800);
    }

    public void ResolutionMax()
    {
        ChangeResolution(1920,1080);
    }

    public void ChangeFullscreen()
    {
        Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
    }

    private void ChangeResolution(int _width, int _height)
    {
        Screen.SetResolution(_width, _height, Screen.fullScreen);
    }

    public void BackToMenu()
    {
        MenuDebut.SetActive(true);
    }

}
