using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class dme : MonoBehaviour {

    public GameObject canvas_sce,canvas_help;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void debme()
    {
        GameObject.Find("init").GetComponent<init_msi_snoopy>().random_ins();
    }

    void debin()
    {
        GameObject.Find("init").GetComponent<init_msi_snoopy>().init();
    }

    void debdeb()
    {
        canvas_sce.SetActive(true);
    }

    void debdebo()
    {
        canvas_sce.SetActive(false);
    }

    void debhel()
    {
        canvas_help.SetActive(true);
    }

    void debhelo()
    {
        canvas_help.SetActive(false);
    }

    void exitmenu()
    {
        Destroy(GameObject.Find("Language"), 0f);
        SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
    }

    void quit()
    {
        Application.Quit();
    }
}
