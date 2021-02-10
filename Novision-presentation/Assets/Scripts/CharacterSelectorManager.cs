using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectorManager : MonoBehaviour
{

    public CameraFollow CameraFollow;
    
    public Button ButtonAlde;
    public Button ButtonBilly;
    public Button ButtonDavid;
    public Button ButtonSam;

    public GameObject Alde;
    public GameObject Billy;
    public GameObject David;
    public GameObject Sam;

    // Start is called before the first frame update
    void Start()
    {
        ButtonAlde.onClick.AddListener(() => { SetCharacter(Alde); });
        ButtonBilly.onClick.AddListener(() => { SetCharacter(Billy); });
        ButtonDavid.onClick.AddListener(() => { SetCharacter(David); });
        ButtonSam.onClick.AddListener(() => { SetCharacter(Sam); });
    }

    private void SetCharacter(GameObject mainCharacter)
    {
        // Sets every character to unactive except for the selected player
        Alde.SetActive(mainCharacter == Alde);
        Billy.SetActive(mainCharacter == Billy);
        David.SetActive(mainCharacter == David);
        Sam.SetActive(mainCharacter == Sam);

        // Sets the camera follow target
        CameraFollow.followTransform = mainCharacter.transform.GetChild(0).transform;


        // Hides the UI
        this.gameObject.SetActive(false);
    }
} 