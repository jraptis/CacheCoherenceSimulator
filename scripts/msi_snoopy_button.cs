using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class msi_snoopy_button : MonoBehaviour {

    //Button mesis, mesid;
    
   // InputField inpc, inpb;
   // bool state;
    // Use this for initialization
    void Start () {
       // state = false;
        //mesis = GameObject.Find("Button_mesi_snoopy").GetComponent<Button>();
        //mesid = GameObject.Find("Button_mesi_directory").GetComponent<Button>();
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void runme_msi_snoopy()
    {
         SceneManager.LoadScene("msi_snoopy", LoadSceneMode.Single);
    }

    void runme_mesi_snoopy()
    {
        if(GameObject.Find("Language").GetComponent<Language>().lang==0) SceneManager.LoadScene("mesi_snoopy_en", LoadSceneMode.Single); 
        else if (GameObject.Find("Language").GetComponent<Language>().lang == 1) SceneManager.LoadScene("mesi_snoopy", LoadSceneMode.Single);
    }

    void runme_mesi_directory()
    {
        SceneManager.LoadScene("mesi_directory", LoadSceneMode.Single);
    }
}
