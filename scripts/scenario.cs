using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scenario : MonoBehaviour {

    public GameObject coren,canvs;
    public string sc;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void debin()
    {
        GameObject.Find("init").GetComponent<init_msi_snoopy>().init();
    }

    void check_correct_n()
    {
        string scn = coren.GetComponent<InputField>().text;
        if (scn == "11" || scn == "21" || scn == "31") { coren.GetComponent<InputField>().text = "1"; }
        else if (scn == "12" || scn == "22" || scn == "32") { coren.GetComponent<InputField>().text = "2"; }
        else if (scn == "13" || scn == "23" || scn == "33") { coren.GetComponent<InputField>().text = "3"; }
        else if (scn == "1" || scn == "2" || scn == "3") { } else { coren.GetComponent<InputField>().text = "1"; StartCoroutine(mes_bad()); }
    }

    IEnumerator mes_bad()
    {
        GameObject ms = GameObject.Find("Text_mes_bad");
        ms.GetComponent<Text>().text = "Type '1', '2', or '3'";
        yield return new WaitForSeconds(3);
        ms.GetComponent<Text>().text = "";
    }

    void run_sce()
    {
        string s2 = coren.GetComponent<InputField>().text;
       
        if (s2 != "1" && s2!="2" && s2!="3") s2 = "1";       
        string s = sc;
       
        if (s == "1")  //PrRd hit
        {
            debin();
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "F5";

            GameObject.Find("A9").GetComponent<Text>().text = "F1";
            GameObject.Find("A10").GetComponent<Text>().text = "F5";
            GameObject.Find("A11").GetComponent<Text>().text = "G2";
            GameObject.Find("A12").GetComponent<Text>().text = "G7";
            GameObject.Find("S3").GetComponent<Text>().text = "V";

            GameObject.Find("C" + s2 + "_5").GetComponent<Text>().text = "F1";
            GameObject.Find("C" + s2 + "_6").GetComponent<Text>().text = "F5";
            GameObject.Find("C" + s2 + "_7").GetComponent<Text>().text = "G2";
            GameObject.Find("C" + s2 + "_8").GetComponent<Text>().text = "G7";
            GameObject.Find("S" + s2 + "_2").GetComponent<Text>().text = "E";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todoread();
        }

        else if (s == "2")  //PrRd miss (other core in state E)
        {
            debin();
            string s_temp;
            if (s2 != "1") s_temp = "1"; else s_temp = "2";
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C5";

            GameObject.Find("C" + s_temp + "_9").GetComponent<Text>().text = "F1";
            GameObject.Find("C" + s_temp + "_10").GetComponent<Text>().text = "C5";
            GameObject.Find("C" + s_temp + "_11").GetComponent<Text>().text = "G2";
            GameObject.Find("C" + s_temp + "_12").GetComponent<Text>().text = "H7";
            GameObject.Find("S" + s_temp + "_3").GetComponent<Text>().text = "E";

            GameObject.Find("A25").GetComponent<Text>().text = "F1";
            GameObject.Find("A26").GetComponent<Text>().text = "C5";
            GameObject.Find("A27").GetComponent<Text>().text = "G2";
            GameObject.Find("A28").GetComponent<Text>().text = "H7";
            GameObject.Find("S7").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todoread();
        }        
        else if (s == "3")  //PrRd miss (other cores in state S)
        {
            debin();
            string s_temp="1",s_temp2="2";

            if (s2 == "1") {
                s_temp = "2";
                s_temp2 = "3";
            }
            else if (s2 == "2")
            {
                s_temp = "1";
                s_temp2 = "3";
            }
            else if (s2 == "3")
            {
                s_temp = "1";
                s_temp2 = "2";
            }
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "F5";

            GameObject.Find("C" + s_temp2 + "_13").GetComponent<Text>().text = "F1";
            GameObject.Find("C" + s_temp2 + "_14").GetComponent<Text>().text = "F5";
            GameObject.Find("C" + s_temp2 + "_15").GetComponent<Text>().text = "G2";
            GameObject.Find("C" + s_temp2 + "_16").GetComponent<Text>().text = "G7";
            GameObject.Find("S" + s_temp2 + "_4").GetComponent<Text>().text = "S";

            GameObject.Find("C" + s_temp + "_13").GetComponent<Text>().text = "F1";
            GameObject.Find("C" + s_temp + "_14").GetComponent<Text>().text = "F5";
            GameObject.Find("C" + s_temp + "_15").GetComponent<Text>().text = "G2";
            GameObject.Find("C" + s_temp + "_16").GetComponent<Text>().text = "G7";
            GameObject.Find("S" + s_temp + "_4").GetComponent<Text>().text = "S";

            GameObject.Find("A13").GetComponent<Text>().text = "F1";
            GameObject.Find("A14").GetComponent<Text>().text = "F5";
            GameObject.Find("A15").GetComponent<Text>().text = "G2";
            GameObject.Find("A16").GetComponent<Text>().text = "G7";
            GameObject.Find("S3").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todoread();
        }
        else if (s == "4")  //PrRd miss / not in other caches
        {
            debin();
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "F5";

            GameObject.Find("A9").GetComponent<Text>().text = "F1";
            GameObject.Find("A10").GetComponent<Text>().text = "F5";
            GameObject.Find("A11").GetComponent<Text>().text = "G2";
            GameObject.Find("A12").GetComponent<Text>().text = "G7";
            GameObject.Find("S3").GetComponent<Text>().text = "V";            

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todoread();
        }
        else if (s == "5")  //PrRd miss (other core in state M)
        {
            debin();
            string s_temp;
            if (s2 != "1") s_temp = "1"; else s_temp = "2";
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "F5";

            GameObject.Find("C" + s_temp + "_9").GetComponent<Text>().text = "F1";
            GameObject.Find("C" + s_temp + "_10").GetComponent<Text>().text = "F5";
            GameObject.Find("C" + s_temp + "_11").GetComponent<Text>().text = "G2";
            GameObject.Find("C" + s_temp + "_12").GetComponent<Text>().text = "G7";
            GameObject.Find("S" + s_temp + "_3").GetComponent<Text>().text = "M";

            GameObject.Find("A57").GetComponent<Text>().text = "F1";
            GameObject.Find("A58").GetComponent<Text>().text = "F5";
            GameObject.Find("A59").GetComponent<Text>().text = "G2";
            GameObject.Find("A60").GetComponent<Text>().text = "G7";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todoread();
        }
        else if (s == "6")  //PrWr hit (own core in state M)
        {
            debin();
           
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C2";

            GameObject.Find("C" + s2+ "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s2 + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s2 + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s2 + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s2 + "_3").GetComponent<Text>().text = "M";

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todowrite();
        }
        else if (s == "7")  //PrWr hit (own core in state E)
        {
            debin();

            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C2";

            GameObject.Find("C" + s2 + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s2 + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s2 + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s2 + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s2 + "_3").GetComponent<Text>().text = "E";

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todowrite();
        }
        else if (s == "8")  //PrWr hit (own core in state S)
        {
            debin();
            string s_temp;
            if (s2 != "1") s_temp = "1"; else s_temp = "2";
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C2";

            GameObject.Find("C" + s2 + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s2 + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s2 + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s2 + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s2 + "_3").GetComponent<Text>().text = "S";

            GameObject.Find("C" + s_temp + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s_temp + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s_temp + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s_temp + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s_temp + "_3").GetComponent<Text>().text = "S";

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todowrite();
        }
        else if (s == "9")  //PrWr hit (not in other caches)
        {
            debin();
            
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C2";            

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todowrite();
        }
        else if (s == "10")  //PrWr hit (state E in other cache)
        {
            debin();
            string s_temp;
            if (s2 != "1") s_temp = "1"; else s_temp = "2";
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C2";            

            GameObject.Find("C" + s_temp + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s_temp + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s_temp + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s_temp + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s_temp + "_3").GetComponent<Text>().text = "E";

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todowrite();
        }
        else if (s == "11")  //PrWr hit (state S in other cores)
        {
            debin();
            string s_temp = "", s_temp2 = "";
            if (s2 == "1")
            {
                s_temp = "2";
                s_temp2 = "3";
            }
            else if (s2 == "2")
            {
                s_temp = "1";
                s_temp2 = "3";
            }
            else if (s2 == "3")
            {
                s_temp = "1";
                s_temp2 = "2";
            }
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C2";

            GameObject.Find("C" + s_temp2 + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s_temp2 + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s_temp2 + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s_temp2 + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s_temp2 + "_3").GetComponent<Text>().text = "S";

            GameObject.Find("C" + s_temp + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s_temp + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s_temp + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s_temp + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s_temp + "_3").GetComponent<Text>().text = "S";

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todowrite();
        }
        else if (s == "12")  //PrWr hit (state M in other cache)
        {
            debin();
            string s_temp;
            if (s2 != "1") s_temp = "1"; else s_temp = "2";
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "C2";

            GameObject.Find("C" + s_temp + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s_temp + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s_temp + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s_temp + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s_temp + "_3").GetComponent<Text>().text = "M";

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todowrite();
        }
        else if (s == "13")  //PrRd hit (state M in local cache position)
        {
            debin();            
            GameObject.Find("Canvas_core" + s2 + "/InputField_core").GetComponent<InputField>().text = "K2";

            GameObject.Find("C" + s2 + "_9").GetComponent<Text>().text = "C1";
            GameObject.Find("C" + s2 + "_10").GetComponent<Text>().text = "C2";
            GameObject.Find("C" + s2 + "_11").GetComponent<Text>().text = "B1";
            GameObject.Find("C" + s2 + "_12").GetComponent<Text>().text = "B2";
            GameObject.Find("S" + s2 + "_3").GetComponent<Text>().text = "M";

            GameObject.Find("A57").GetComponent<Text>().text = "C1";
            GameObject.Find("A58").GetComponent<Text>().text = "C2";
            GameObject.Find("A59").GetComponent<Text>().text = "B1";
            GameObject.Find("A60").GetComponent<Text>().text = "B2";
            GameObject.Find("S15").GetComponent<Text>().text = "V";

            GameObject.Find("A9").GetComponent<Text>().text = "K1";
            GameObject.Find("A10").GetComponent<Text>().text = "K2";
            GameObject.Find("A11").GetComponent<Text>().text = "P1";
            GameObject.Find("A12").GetComponent<Text>().text = "P3";
            GameObject.Find("S3").GetComponent<Text>().text = "V";

            GameObject.Find("Canvas_core" + s2 + "/read_b_c" + s2).GetComponent<terminal>().todoread();
        }
        canvs.SetActive(false);
    }
}
