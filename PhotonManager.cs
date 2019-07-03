using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.PunBehaviour
{
    public static PhotonManager instance;
    public static GameObject localPlayer;
    public GameObject joinBTN;
    public Text guideText;

    public void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        guideText.text = "connect to Photon..";
        PhotonNetwork.ConnectUsingSettings("network_v1.0");
        joinBTN.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        guideText.text = "Master Server에 연결되었습니다.";
        joinBTN.SetActive(true);
    }
}
