using UnityEngine;
using UnityEngine.UI;

public class MobileNetwork : Photon.PunBehaviour
{
    private string roomName;
    private GyroController gyroController;
    public Text debugText;

    private void OnGUI()
    {
        //GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    // TODO-2.a: 
    // Copy and paste the Start() and OnJoinedLobby() methods from MobileNetwork_Cube.cs

	public override void OnJoinedRoom()
	{
        if (debugText != null)
            debugText.text = "Joined room";

        GetComponent<MobileShooter>().Activate();
		base.OnJoinedRoom ();
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        roomName = "testing";
        Screen.orientation = ScreenOrientation.Portrait;
        gyroController = GetComponent<GyroController>();
    }

    private void Update()
    {
        if (debugText != null)
            debugText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }
    public override void OnJoinedLobby()
    {
       

        PhotonNetwork.JoinRoom(roomName);
        

        base.OnJoinedLobby();
    }

}
