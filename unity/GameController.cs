using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject UI_Obj;
    UI UserI;

    public string currentScene;
    public string savedScene;
    public float gameSpeed = 1f; //seconds
    public int selectedFile = 1;
    public int fileNumber;
    public bool existsFile1 = false;
    public bool existsFile2 = false;
    public bool existsFile3 = false;
    public bool existsFile4 = false;
    public bool existsFile5 = false;
    public bool existsFile6 = false;

    //CLOCK, DAY, MONTH, YEAR
    IEnumerator clock;
    static string[] days = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    public string currentDay = days[0];
    static string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public string currentMonth = months[5];
    public int numDays = 1;
    public int numYears = 1;
    public int clockHour = 11;
    public int clockMinute = 0;
    public string clockInfo = "AM";
    public int dayIndex;
    public int monthIndex;
    public bool leapYear;

    public bool paused;
    public bool needClock;

    void Start () {

        UI_Obj = GameObject.FindWithTag ("UI");
        UserI = UI_Obj.GetComponent<UI> ();

        /* GET WHICH FILES EXIST*/
        existsFile1 = PlayerPrefs.GetInt ("existsFile1") == 1;
        existsFile2 = PlayerPrefs.GetInt ("existsFile1") == 1;
        existsFile3 = PlayerPrefs.GetInt ("existsFile1") == 1;
        existsFile4 = PlayerPrefs.GetInt ("existsFile1") == 1;
        existsFile5 = PlayerPrefs.GetInt ("existsFile1") == 1;
        existsFile6 = PlayerPrefs.GetInt ("existsFile1") == 1;

        needClock = true;
        currentScene = SceneManager.GetActiveScene ().name;

        clock = clockCounter ();
        dayIndex = System.Array.IndexOf (days, currentDay); //Returns the index location of currentDay in days[]
        monthIndex = System.Array.IndexOf (months, currentMonth); //Returns the index location of currentMonth in months[]

    }

    void Update () {

        Debug.Log ("Selected File: " + selectedFile);

        if (!currentScene.Contains ("Start") && !currentScene.Contains ("CC")) {

            // CLOCK handler
            if (paused) {
                StopCoroutine (clock);
                needClock = true;
            }
            if (!paused && needClock) {
                StartCoroutine (clock);
                needClock = false;
            }
            dayIndex = System.Array.IndexOf (days, currentDay); //Returns the index location of currentDay in days[]
            monthIndex = System.Array.IndexOf (months, currentMonth); //Returns the index location of currentMonth in months[]

            if (numYears % 4 == 0) {
                leapYear = true;
            } else {
                leapYear = false;
            }

        }
    }

    IEnumerator clockCounter () {
        while (true) {
            clockMinute = clockMinute + 1;
            if (clockMinute > 59) {
                clockHour = clockHour + 1;
                //Change AM/PM at noon and midnight
                if (clockHour > 11 && clockHour <= 12) {
                    if (clockInfo.Contains ("AM")) {
                        clockInfo = "PM";
                    } else if (!clockInfo.Contains ("AM")) {
                        clockInfo = "AM";
                    }
                    //else change from 12 to 1
                } else if (clockHour > 12) {
                    clockHour = 1;
                }
                clockMinute = 0;
            }
            //Change the Day
            if (clockInfo.Contains ("AM") && clockHour == 12 && clockMinute == 0) {
                numDays = numDays + 1;
                if ((dayIndex + 1) <= days.Length - 1) {
                    currentDay = days[dayIndex + 1];
                } else {
                    currentDay = days[0];
                }
            }
            //Change the Month
            //30 day months
            if (numDays > 30 && ((currentMonth.Contains (months[3])) || (currentMonth.Contains (months[5])) || (currentMonth.Contains (months[8])) || (currentMonth.Contains (months[10])))) {
                currentMonth = months[monthIndex + 1];
                numDays = 1;
            } /*February - not Leap Year*/
            else if ((numDays > 28) && (!leapYear) && (currentMonth.Contains (months[1]))) {
                currentMonth = months[monthIndex + 1];
                numDays = 1;
            } /*February - Leap Year*/
            else if ((numDays > 29) && (leapYear) && (currentMonth.Contains (months[1]))) {
                currentMonth = months[monthIndex + 1];
                numDays = 1;
            } /* Rest of Months */
            else if (numDays > 31) {
                if ((monthIndex + 1) <= months.Length - 1) {
                    currentMonth = months[monthIndex + 1];
                    numDays = 1;
                } else {
                    currentMonth = months[0];
                    numDays = 1;
                    numYears++;
                }
            }

            yield return new WaitForSeconds (gameSpeed);
        }
    }

    public void assignFile1 () {
        selectedFile = 1;
        Debug.Log ("Assigned File 1.");
    }
    public void assignFile2 () {
        selectedFile = 2;
        Debug.Log ("Assigned File 2.");
    }
    public void assignFile3 () {
        selectedFile = 3;
        Debug.Log ("Assigned File 3.");
    }
    public void assignFile4 () {
        selectedFile = 4;
        Debug.Log ("Assigned File 4.");
    }
    public void assignFile5 () {
        selectedFile = 5;
        Debug.Log ("Assigned File 5.");
    }
    public void assignFile6 () {
        selectedFile = 6;
        Debug.Log ("Assigned File 6.");
    }

    public void saveFile () {

        paused = true;

        /* -- SAVE Current Scene */
        PlayerPrefs.SetString ("savedScene" + fileNumber.ToString (), currentScene);

        /* -- SAVE Day, Month, Year, and Counters */
        PlayerPrefs.SetString ("currentDay" + fileNumber.ToString (), currentDay);
        PlayerPrefs.SetString ("currentMonth" + fileNumber.ToString (), currentMonth);
        PlayerPrefs.SetString ("clockInfo" + fileNumber.ToString (), clockInfo);
        PlayerPrefs.SetInt ("clockHour" + fileNumber.ToString (), clockHour);
        PlayerPrefs.SetInt ("clockMinute" + fileNumber.ToString (), clockMinute);
        PlayerPrefs.SetInt ("numDays" + fileNumber.ToString (), numDays);
        PlayerPrefs.SetInt ("numYears" + fileNumber.ToString (), numYears);

        /* SAVE File Exists Bool */
        switch (fileNumber) {
            case 1:
                if (!existsFile1) {
                    existsFile1 = true;
                    Debug.Log ("File 1 Created.");
                    PlayerPrefs.SetInt ("existsFile1", existsFile1 ? 1 : 0);
                } else {
                    Debug.Log ("File 1 Overrided.");
                }
                break;
            case 2:
                if (!existsFile2) {
                    existsFile2 = true;
                    Debug.Log ("File 2 Created.");
                    PlayerPrefs.SetInt ("existsFile2", existsFile2 ? 1 : 0);
                } else {
                    Debug.Log ("File 2 Overrided.");
                }
                break;
            case 3:
                if (!existsFile3) {
                    existsFile3 = true;
                    Debug.Log ("File 3 Created.");
                    PlayerPrefs.SetInt ("existsFile3", existsFile3 ? 1 : 0);
                } else {
                    Debug.Log ("File 3 Overrided.");
                }
                break;
            case 4:
                if (!existsFile4) {
                    existsFile4 = true;
                    Debug.Log ("File 4 Created.");
                    PlayerPrefs.SetInt ("existsFile4", existsFile4 ? 1 : 0);
                } else {
                    Debug.Log ("File 4 Overrided.");
                }
                break;
            case 5:
                if (!existsFile5) {
                    existsFile5 = true;
                    Debug.Log ("File 5 Created.");
                    PlayerPrefs.SetInt ("existsFile5", existsFile5 ? 1 : 0);
                } else {
                    Debug.Log ("File 5 Overrided.");
                }
                break;
            case 6:
                if (!existsFile6) {
                    existsFile6 = true;
                    Debug.Log ("File 6 Created.");
                    PlayerPrefs.SetInt ("existsFile6", existsFile6 ? 1 : 0);
                } else {
                    Debug.Log ("File 6 Overrided.");
                }
                break;
        }

        PlayerPrefs.Save ();
        Debug.Log ("FILE " + fileNumber.ToString () + ": SAVED GameController.");
        paused = false;

    }

    public void loadFile () {

        paused = true;

        /* -- LOAD Current Scene */
        savedScene = PlayerPrefs.GetString ("savedScene" + fileNumber.ToString ());

        /* -- LOAD Day, Month, Year, and Counters */
        currentDay = PlayerPrefs.GetString ("currentDay" + fileNumber.ToString ());
        currentMonth = PlayerPrefs.GetString ("currentMonth" + fileNumber.ToString ());
        clockInfo = PlayerPrefs.GetString ("clockInfo" + fileNumber.ToString ());
        clockHour = PlayerPrefs.GetInt ("clockHour" + fileNumber.ToString ());
        clockMinute = PlayerPrefs.GetInt ("clockMinute" + fileNumber.ToString ());
        numDays = PlayerPrefs.GetInt ("numDays" + fileNumber.ToString ());
        numYears = PlayerPrefs.GetInt ("numYears" + fileNumber.ToString ());

        SceneManager.LoadScene (savedScene);

        Debug.Log ("FILE " + fileNumber.ToString () + ": LOADED GameController.");
        paused = false;

    }

    public void deleteFile () {

        /* DELETE Current Scene */
        PlayerPrefs.DeleteKey ("currentScene" + fileNumber.ToString ());

        /* DELETE Day, Month, Year, and Counters */
        PlayerPrefs.DeleteKey ("currentDay" + fileNumber.ToString ());
        PlayerPrefs.DeleteKey ("currentMonth" + fileNumber.ToString ());
        PlayerPrefs.DeleteKey ("clockInfo" + fileNumber.ToString ());
        PlayerPrefs.DeleteKey ("clockHour" + fileNumber.ToString ());
        PlayerPrefs.DeleteKey ("clockMinute" + fileNumber.ToString ());
        PlayerPrefs.DeleteKey ("numDays" + fileNumber.ToString ());
        PlayerPrefs.DeleteKey ("numYears" + fileNumber.ToString ());

        /* Delete File Exists Bool */
        switch (fileNumber) {
            case 1:
                existsFile1 = false;
                PlayerPrefs.SetInt ("existsFile1", existsFile1 ? 1 : 0);
                break;
            case 2:
                existsFile2 = false;
                PlayerPrefs.SetInt ("existsFile2", existsFile2 ? 1 : 0);
                break;
            case 3:
                existsFile3 = false;
                PlayerPrefs.SetInt ("existsFile3", existsFile3 ? 1 : 0);
                break;
            case 4:
                existsFile4 = false;
                PlayerPrefs.SetInt ("existsFile4", existsFile4 ? 1 : 0);
                break;
            case 5:
                existsFile5 = false;
                PlayerPrefs.SetInt ("existsFile5", existsFile5 ? 1 : 0);
                break;
            case 6:
                existsFile6 = false;
                PlayerPrefs.SetInt ("existsFile6", existsFile6 ? 1 : 0);
                break;
        }

        Debug.Log ("DELETED File " + fileNumber.ToString () + ".");

        if (!existsFile1 && !existsFile2 && !existsFile3 && !existsFile4 && !existsFile5 && !existsFile6) {
            deleteAll ();
        }

        UserI.closeDelete();

    }

    public void deleteAll () {

        PlayerPrefs.DeleteAll ();
        Debug.Log ("No more files exist, DeleteAll PPs has been run.");

    }

}
