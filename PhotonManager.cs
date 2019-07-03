using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.PunBehaviour
{
    #region Variables

    public static PhotonManager instance;
    public static GameObject localPlayer;
    public GameObject joinBTN;
    public Text guideText;

    #endregion

    #region Scene, Network loading

    public void Awake()
    {
        //접속관리객체 PhotonManager가 이미 있는 경우
        if(instance != null)
        {
            Destroy(gameObject);        //생성된 객체를 없애고 리턴
            return;
        }
        DontDestroyOnLoad(gameObject);  //(위에서 이미 있지 않은 경우) PhotonManager객체 없애지 말자
        
        //같은 Room안에 있는 client는 master와 같은 레벨로 이동한다.
        PhotonNetwork.automaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        guideText.text = "connect to Photon..";
        PhotonNetwork.ConnectUsingSettings("network_v1.0");
        joinBTN.SetActive(false);
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }

    //Callback : Start에서 서버 호출이 정상적으로 이루어졌을 경우
    public override void OnConnectedToMaster()
    {
        guideText.text = "Master Server에 연결되었습니다.";
        joinBTN.SetActive(true);
    }

    #region Push button

    //접속 버튼을 눌렀을 경우 호출되는 method
    public void JoinGameRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        //룸 접속
        PhotonNetwork.JoinOrCreateRoom("Room1", options, TypedLobby.Default);
    }

    #endregion

    #region After push button

    //Callback : 룸 접속이 완료되었을때
    public override void OnJoinedRoom()
    {
        guideText.text = "Join the room";

        //유저가 마스터 클라이언트인 경우
        if (PhotonNetwork.isMasterClient)
        {
            print("OnLevelisLoading");
            //mainScene 접속
            PhotonNetwork.LoadLevel("mainScene");
        }
    }

    //Callback : 레벨이 로드되었을때
    private void OnLevelWasLoaded(int level)
    {
        print("OnLevelisLoaded");
        if (!PhotonNetwork.inRoom) return;
        print("OnLevelWasLoaded");
        
        //Photon View 스크립트가 들어간 프리팹 객체를 로드한다.
        localPlayer = PhotonNetwork.Instantiate(
            "Capsule",
            new Vector3(0, 1, 0),
            Quaternion.identity, 0
            );
    }
    #endregion
}
