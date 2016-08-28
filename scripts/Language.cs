using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Language : MonoBehaviour {

    public GameObject eng, gre,info,exit,info1,info2,close;
    public int lang;
    private Color col_off,col_on;
	// Use this for initialization
	void Start () {
        lang = 0;
        col_off = new Vector4(1f, 1f, 1f, 0.16f);
        col_on = new Vector4(1f,1f,1f,1f);
        //DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void change_lang()
    {
        if(lang==0)
        {
            lang = 1;
            eng.GetComponent<Image>().color = col_off;
            gre.GetComponent<Image>().color = col_on;
            info.GetComponent<Text>().text = "Πληροφορίες";
            exit.GetComponent<Text>().text = "Έξοδος";
            info1.GetComponent<Text>().text = "Ανάπτυξη: Ioannis A. Raptis\nΕπόπτευση: Dimitrios Kehagias\nPowered by Unity Engine";
            info2.GetComponent<Text>().text = "Οι εικόνες που χρησιμοποιήθηκαν στην εφαρμογή προήλθαν από το pixabay με άδεια: CC0 Public Domain License";
            close.GetComponent<Text>().text = "Κλείσιμο";

        }
        else if(lang==1)
        {
            lang = 0;
            eng.GetComponent<Image>().color = col_on;
            gre.GetComponent<Image>().color = col_off;
            info.GetComponent<Text>().text = "Info";
            exit.GetComponent<Text>().text = "Exit";
            info1.GetComponent<Text>().text = "Developed by Ioannis A. Raptis\nSupervised by Dimitrios Kehagias\nPowered by Unity Engine";
            info2.GetComponent<Text>().text = "Image sources from pixabay. Used under CC0 Public Domain License";
            close.GetComponent<Text>().text = "Close";

        }
    }
}
