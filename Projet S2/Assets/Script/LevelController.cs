using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LevelController : MonoBehaviour
{

    [SerializeField] private GameObject SelectLevel1;
    [SerializeField] private GameObject SelectLevel2;
    [SerializeField] private GameObject SelectLevel3;

    // Start is called before the first frame update
    public void Level1()
    {
        if (PhotonNetwork.isMasterClient)
            PhotonNetwork.LoadLevel("Level1");
    }

}
