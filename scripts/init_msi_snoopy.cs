using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class init_msi_snoopy : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject.Find("InputField_speed").GetComponent<InputField>().text = "30";
        init();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void init()
    {
        GameObject.Find("Canvas_core1/InputField_core").GetComponent<InputField>().text = "";
        GameObject.Find("Canvas_core2/InputField_core").GetComponent<InputField>().text = "";
        GameObject.Find("Canvas_core3/InputField_core").GetComponent<InputField>().text = "";
        int i,j;
        for (i = 1; i <= 64; i++)
        {
            GameObject.Find("A" + i).GetComponent<Text>().text = "-";
        }
        for (i = 1; i <= 16; i++)
        {
            GameObject.Find("S" + i).GetComponent<Text>().text = "V";
        }
        for (i = 1; i <= 16; i++)
            for (j = 1; j <= 3; j++)
            {
            GameObject.Find("C"+j+"_" + i).GetComponent<Text>().text = "-";
            }
        for (i = 1; i <= 4; i++)
            for (j = 1; j <= 3; j++)
            {
            GameObject.Find("S"+j+"_" + i).GetComponent<Text>().text = "I";
            }
    }

    public void random_ins()
    {
        string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int i,j;

        for (i = 1; i <= 64; i++)
        {
            GameObject.Find("A" + i).GetComponent<Text>().text = "-";
        }

        for (i = 0; i <= 25; i++)
        {
            for (j = 0; j <= 9; j++)
            {
                if (Random.Range(0, 100) > 80)
                {
                    GameObject.Find("A" + Random.Range(1, 65)).GetComponent<Text>().text = Alphabet[i] + j;
                }
            }
        }
        for (i = 1; i <= 16; i++)
        {
            GameObject.Find("S" + i).GetComponent<Text>().text = "V";
        }
        for (i = 1; i <= 16; i++)
            for (j = 1; j <= 3; j++)
            {
                GameObject.Find("C" + j + "_" + i).GetComponent<Text>().text = "-";
            }
        for (i = 1; i <= 4; i++)
            for (j = 1; j <= 3; j++)
            {
                GameObject.Find("S" + j + "_" + i).GetComponent<Text>().text = "I";
            }
    }
}
