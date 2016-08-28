using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class credits : MonoBehaviour {

    public GameObject cnv_cre;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void close()
    {
        cnv_cre.SetActive(false);
    }

    void open()
    {
        cnv_cre.SetActive(true);
    }
}
