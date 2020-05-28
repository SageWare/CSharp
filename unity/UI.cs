using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameObject GC_Obj;
    GameController GC;

    public GameObject startMenu;
    public GameObject PlayerMenu;
    public GameObject LoadMenu;
    public GameObject SaveMenu;
    public GameObject DeleteMenu;
    public GameObject CCMenu;

    //Start Menu's Load Button (only interactable if any 1 file exists)
    public Button startLoadButton;

    // Load File Buttons (only interactable if the file exists)
    public Button loadFile1;
    public Button loadFile2;
    public Button loadFile3;
    public Button loadFile4;
    public Button loadFile5;
    public Button loadFile6;

    public string currentScene;

    void Start () {

        GC_Obj = GameObject.FindWithTag ("GameController");
        GC = GC_Obj.GetComponent<GameController> ();

        startMenu = GameObject.FindWithTag ("StartMenu");
        PlayerMenu = GameObject.FindWithTag ("PlayerMenu");
        LoadMenu = GameObject.FindWithTag ("LoadMenu");
        SaveMenu = GameObject.FindWithTag ("SaveMenu");
        DeleteMenu = GameObject.FindWithTag ("DeleteMenu");
        CCMenu = GameObject.FindWithTag ("CCMenu");

        LoadMenu.SetActive (false);
        SaveMenu.SetActive (false);
        DeleteMenu.SetActive (false);
        PlayerMenu.SetActive (false);

        currentScene = SceneManager.GetActiveScene ().name;

        if (currentScene.Contains ("Start")) {
            startMenu.SetActive (true);
        } else {
            startMenu.SetActive (false);
        }

        if (currentScene.Contains ("CC")) {
            CCMenu.SetActive (true);
        } else {
            CCMenu.SetActive (false);
        }

    }

    void Update () {

        if (GC.existsFile1) {
            loadFile1.interactable = true;
        } else {
            loadFile1.interactable = false;
        }

        if (GC.existsFile2) {
            loadFile2.interactable = true;
        } else {
            loadFile2.interactable = false;
        }

        if (GC.existsFile3) {
            loadFile3.interactable = true;
        } else {
            loadFile3.interactable = false;
        }

        if (GC.existsFile4) {
            loadFile4.interactable = true;
        } else {
            loadFile4.interactable = false;
        }

        if (GC.existsFile5) {
            loadFile5.interactable = true;
        } else {
            loadFile5.interactable = false;
        }

        if (GC.existsFile6) {
            loadFile6.interactable = true;
        } else {
            loadFile6.interactable = false;
        }

        if (GC.existsFile1 || GC.existsFile2 || GC.existsFile3 || GC.existsFile4 || GC.existsFile5 || GC.existsFile6) {
            startLoadButton.interactable = true;
        } else {
            startLoadButton.interactable = false;
        }

    }

    public void newGame () {
        SceneManager.LoadScene ("CC");
    }

    public void openLoad () {
        LoadMenu.SetActive (true);
        GC.paused = true;

    }

    public void closeLoad () {
        LoadMenu.SetActive (false);
        GC.paused = false;
    }

    public void openSave () {
        SaveMenu.SetActive (true);
        GC.paused = true;
    }

    public void closeSave () {
        SaveMenu.SetActive (false);
        GC.paused = false;
    }

    public void openConfirmSave () {
        if ((GC.fileNumber == 1) && GC.existsFile1) {
            //open Confirm Save
        } else if ((GC.fileNumber == 2) && GC.existsFile2) {
            //open Confirm Save
        } else if ((GC.fileNumber == 3) && GC.existsFile3) {
            //open Confirm Save
        } else if ((GC.fileNumber == 4) && GC.existsFile4) {
            //open Confirm Save
        } else if ((GC.fileNumber == 5) && GC.existsFile5) {
            //open Confirm Save
        } else if ((GC.fileNumber == 6) && GC.existsFile6) {
            //open Confirm Save
        } else {
            GC.saveFile();
        }

    }

    public void closeConfirmSave () {

    }

    public void openPlayerMenu () {
        PlayerMenu.SetActive (true);
    }

    public void closePlayerMenu () {
        PlayerMenu.SetActive (false);
    }

    public void openDelete () {
        DeleteMenu.SetActive (true);
    }

    public void closeDelete () {
        DeleteMenu.SetActive (false);
    }

    public void startGame () {
        SceneManager.LoadScene ("World");
    }

}
