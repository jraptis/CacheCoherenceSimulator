using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class terminal : MonoBehaviour {

    public int core;
    public string todo;
    public GameObject redalarm,greenalarm,signal1,signal1f,signal1f2,signal1b,signal2_1,signal2_2,signal2_3,signal2_1b,signal2_2b,signal2_3b,hinge,hingeb,hinge_c1,hinge_c2,hinge_c3,sf11,sf12,sf21,sf22,sf31,sf32,hinge_c1b,hinge_c2b,hinge_c3b,signal3_m, signal3_mb,signal3_mf,signal3_mf2,hingebb,hingebm;
    public GameObject cnv_block;
    public GameObject[] g1, g2, g3, gm;
    bool again;
    public int lang;
    // Use this for initialization
	void Start () {
        todo = "nothing";
        again = false;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "mesi_snoopy_en") lang = 0; else if (scene.name == "mesi_snoopy") lang = 1;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void glowme(string com, bool ss)
    {
        int i;
        for(i=1;i<=16;i++)
        {
           // Debug.Log(g1[(int)Mathf.Ceil(i / 4f)]);
            if (com==GameObject.Find("Canvas_core1/C1_"+i).GetComponent<Text>().text) g1[(int)Mathf.Ceil(i/4f)-1].SetActive(ss);
            if (com == GameObject.Find("Canvas_core2/C2_" + i).GetComponent<Text>().text) g2[(int)Mathf.Ceil(i / 4f)-1].SetActive(ss);
            if (com == GameObject.Find("Canvas_core3/C3_" + i).GetComponent<Text>().text) g3[(int)Mathf.Ceil(i / 4f)-1].SetActive(ss);
        }

        for (i = 1; i <= 64; i++)
        {
            if (com == GameObject.Find("Canvas_mem/A" + i).GetComponent<Text>().text) gm[(int)Mathf.Ceil(i / 4f)-1].SetActive(ss);
        }

    }

    

    IEnumerator runme()
    {       
        float speed = float.Parse(GameObject.Find("InputField_speed").GetComponent<InputField>().text);
        if (speed < 5f || speed > 100f) speed = 30f;
        speed = 100f / speed;    
        string command = GameObject.Find("Canvas_core" + core + "/InputField_core").GetComponent<InputField>().text;
        command = command.ToUpper();
        if (!check_command(command))
        {
            if(lang==0) logwrite("-- Core " + core + ": Bad Statement or '"+command+"' does not exist in the context", true);
            else logwrite("-- Core " + core + ": Λάθος δήλωση ή η λέξη '" + command + "' δεν υπάρχει στην κεντρική μνήμη", true);
            cnv_block.SetActive(false);
            yield break;
        }

        if (todo == "read" || todo == "write")
        {
            glowme(command, true);
            logwrite("", true);
            yield return StartCoroutine(wb(core, speed, command));
            if (todo == "read")
            {
                if (lang == 0) logwrite("- Core " + core + " makes PrRd request", false);
                else logwrite("- Ο Core " + core + " στέλνει αίτημα ανάγνωσης 'PrRd'", false);
            }
            else
            {
                if (lang == 0) logwrite("- Core " + core + " makes 'PrWr' request", false);
                else logwrite("- Ο Core " + core + " στέλνει αίτημα εγγραφής 'PrWr'", false);
            }
            int i;
            bool result = false;
            string tst = "";
            for (i = 1; i <= 16; i++)
            {
                if (GameObject.Find("C" + core + "_" + i).GetComponent<Text>().text == command)
                {
                    tst = GameObject.Find("S" + core + "_" + (Mathf.Floor((i - 1) / 4) + 1)).GetComponent<Text>().text;
                    if (String.Compare(tst, "S") == 0 || String.Compare(tst, "E") == 0 || String.Compare(tst, "M") == 0)
                        result = true;
                    break;
                }
            }
            yield return new WaitForSeconds(speed);
            if (result)
            {
                if (todo == "read")
                {
                    if (lang == 0) logwrite("- Cache controller of Core " + core + " finds '" + command + "' in local cache (HIT)", false);
                    else logwrite("- Ο Cache controller του Core " + core + " βρίσκει την λέξη '" + command + "' στην τοπική cache (HIT)", false);
                }
                greenalarm_en(true);
                if (todo == "read")
                {
                    yield return new WaitForSeconds(speed);
                    greenalarm_en(false);
                }
                if (todo == "write")
                {
                    if (String.Compare(tst, "E") == 0)
                    {
                        GameObject.Find("S" + core + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "M";
                        if (lang == 0) logwrite("- Cache controller of Core " + core + " finds '" + command + "' in local cache (HIT) in state 'E', updates its value and changes its state from 'E' to 'M'", false);
                        else logwrite("- Ο Cache controller του Core " + core + " βρίσκει την λέξη '" + command + "' στην τοπική cache (HIT) στην κατάσταση 'E', ενημερώνει την τιμή της και αλλάζει την κατάστασή της από 'E' σε 'M'", false);
                        yield return new WaitForSeconds(speed);
                        greenalarm_en(false);

                        //logwrite("- Cache controller of Core " + core + " posts the bus transaction 'BusWB'", false);
                        //StartCoroutine(busrd(speed, true));
                        // yield return new WaitForSeconds(speed);
                        // StartCoroutine(busrdx(speed, command, false));
                    }
                    else if (String.Compare(tst, "M") == 0)
                    {
                        if (lang == 0) logwrite("- Cache controller of Core " + core + " finds '" + command + "' in local cache (HIT) in state 'M', updates its value with no state change", false);
                        else logwrite("- Ο Cache controller του Core " + core + " βρίσκει την λέξη '" + command + "' στην τοπική cache (HIT) στην κατάσταση 'M' και ενημερώνει την τιμή χωρίς να αλλάξει την κατάστασή της", false);
                        yield return new WaitForSeconds(speed);
                        greenalarm_en(false);
                    }
                    else if (String.Compare(tst, "S") == 0)
                    {
                        GameObject.Find("S" + core + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "M";
                        if (lang == 0) logwrite("- Cache controller of Core " + core + " finds '" + command + "' in local cache (HIT) in state 'S', updates its value, changes its state from 'S' to 'M'", false);
                        else logwrite("- Ο Cache controller του Core " + core + " βρίσκει την λέξη '" + command + "' στην τοπική cache (HIT) στην κατάσταση 'S', ενημερώνει την τιμή της, αλλάζει την κατάστασή της από 'S' σε 'M'", false);
                        yield return new WaitForSeconds(speed);
                        greenalarm_en(false);

                        if (lang == 0) logwrite("- Cache controller of Core " + core + " posts the bus transaction 'Invalid'", false);
                        else logwrite("- Ο Cache controller του Core " + core + " ενεργοποιεί την δραστηριότητα διαύλου 'Invalid'", false);
                        StartCoroutine(busrd(speed, true));
                        yield return new WaitForSeconds(speed);
                        StartCoroutine(busrdx(speed, command, true));
                    }
                }
            }
            else
            {
                if (String.Compare(tst, "I") == 0)
                {
                    if (lang == 0) logwrite("- Cache controller of Core " + core + " finds '" + command + "' in local cache state 'I' (MISS)", false);
                    else logwrite("- Ο Cache controller του Core " + core + " βρίσκει την λέξη '" + command + "' στην τοπική cache στην κατάσταση 'I' (MISS)", false);
                }
                else
                {
                    if (lang == 0) logwrite("- Cache controller of Core " + core + " doesn't find '" + command + "' in local cache (MISS)", false);
                    else logwrite("- Ο Cache controller του Core " + core + " δεν βρίσκει την λέξη '" + command + "' στην τοπική cache (MISS)", false);
                }
                redalarm_en(true);
                yield return new WaitForSeconds(speed);
                redalarm_en(false);
                if (todo == "write")
                {
                    if (lang == 0) logwrite("- Cache controller of Core " + core + " posts the bus transaction 'BusRdX'", false);
                    else logwrite("- Ο Cache controller του Core " + core + " ενεργοποιεί την δραστηριότητα διαύλου 'BusRdX'", false);
                }
                else if (todo == "read")
                {
                    if (lang == 0) logwrite("- Cache controller of Core " + core + " posts the bus transaction 'BusRd'", false);
                    else logwrite("- Ο Cache controller του Core " + core + " ενεργοποιεί την δραστηριότητα διαύλου 'BusRd'", false);
                }
                StartCoroutine(busrd(speed, true));
                yield return new WaitForSeconds(speed);
                yield return StartCoroutine(bus(speed, command));
            }
            // yield return new WaitForSeconds(speed);
            // logwrite("-- End --", false);
        }
        else
        {
            if (lang == 0) logwrite("-- Core " + core + ": Something is not right(no signal is chosen)", true);
            else logwrite("-- Core " + core + ": Κάτι δεν λειτούργησε σωστά (κανένα σήμα δεν επιλέχθηκε)", true);
        }

        cnv_block.SetActive(false);
        glowme(command, false);


    }

    public void todoread()
    {
        todo = "read";
        cnv_block.SetActive(true);
        StartCoroutine(runme());        
    }

    public void todowrite()
    {
        todo = "write";
        cnv_block.SetActive(true);
        StartCoroutine(runme());
    }

    void logwrite(string st, bool newlog)
    {
        if (newlog) GameObject.Find("Text_log").GetComponent<Text>().text = st;
        else GameObject.Find("Text_log").GetComponent<Text>().text = GameObject.Find("Text_log").GetComponent<Text>().text + "\n" + st;
    }

    bool check_command(string c)
    {
        char[] Alphabet = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        char[] Nums = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        bool a = false, b = false;
        int i;
        if (c.Length==2)
        {
            for (i=0;i<=25;i++) { if (c[0] == Alphabet[i]) a = true; }
            for (i=0;i<=9;i++) { if (c[1] == Nums[i]) b = true; }
        }
        if (!a || !b) return false;

        for (i = 1; i <= 64; i++)
        {
            if (GameObject.Find("A" + i).GetComponent<Text>().text == c) return true;
        }
        return false;
    }

    void redalarm_en(bool state)
    {
        if (state == true)
        {
            redalarm.SetActive(true);
            GameObject.Find("Canvas_core" + core + "/Image_core_light/Image_red/Image_alarm").GetComponent<Rigidbody2D>().AddTorque(100);
        }else if(state== false)
        {
            redalarm.SetActive(false);
        }
    }

    void greenalarm_en(bool state)
    {
        if (state == true)
        {
            greenalarm.SetActive(true);
            GameObject.Find("Canvas_core" + core + "/Image_core_light/Image_green/Image_alarm").GetComponent<Rigidbody2D>().AddTorque(100);
        }
        else if (state == false)
        {
            greenalarm.SetActive(false);
        }
    }

    IEnumerator busrd(float speed,bool tobus)
    {        
        if(tobus) Instantiate(signal1, hinge.transform.position, this.transform.rotation);
           else Instantiate(signal1b, hingeb.transform.position, this.transform.rotation);

        int i;
        for (i = 0; i < 33; i++)
        {
            Instantiate(signal1f, hinge.transform.position, this.transform.rotation);
            Instantiate(signal1f2, hingeb.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(speed / 33f);
        }

    }

    IEnumerator busrdm(float speed, bool tobus)
    {
        if (tobus) Instantiate(signal3_m, hingebb.transform.position, this.transform.rotation);
           else Instantiate(signal3_mb, hingebm.transform.position, this.transform.rotation);

        int i;
        for (i = 0; i < 33; i++)
        {
            Instantiate(signal3_mf, hingebb.transform.position, this.transform.rotation);
            Instantiate(signal3_mf2, hingebm.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(speed / 33f);
        }

    }

    IEnumerator busrd_fromc_tobus(float speed,int c)
    {
        if(c==1)  Instantiate(signal2_1b, hinge_c1 .transform.position, this.transform.rotation);
        else if (c == 2) Instantiate(signal2_2b, hinge_c2.transform.position, this.transform.rotation);
        else if (c == 3) Instantiate(signal2_3b, hinge_c3.transform.position, this.transform.rotation);

        int i;
        if (c == 1)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf11, hinge_c1.transform.position, this.transform.rotation);
                Instantiate(sf12, hinge_c1b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
        else if (c == 2)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf21, hinge_c2.transform.position, this.transform.rotation);
                Instantiate(sf22, hinge_c2b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
        else if (c == 3)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf31, hinge_c3.transform.position, this.transform.rotation);
                Instantiate(sf32, hinge_c3b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
    }

    IEnumerator busrd_to_others(float speed)
    {
        if (core == 1)
        {
            Instantiate(signal2_2, hinge_c2b.transform.position, this.transform.rotation);
            Instantiate(signal2_3, hinge_c3b.transform.position, this.transform.rotation);
        }
        else if (core == 2)
        {
            Instantiate(signal2_1, hinge_c1b.transform.position, this.transform.rotation);
            Instantiate(signal2_3, hinge_c3b.transform.position, this.transform.rotation);
        }
        else if (core== 3)
        {
            Instantiate(signal2_1, hinge_c1b.transform.position, this.transform.rotation);
            Instantiate(signal2_2, hinge_c2b.transform.position, this.transform.rotation);
        }


        int i;
        if (core == 1)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf21, hinge_c2.transform.position, this.transform.rotation);
                Instantiate(sf22, hinge_c2b.transform.position, this.transform.rotation);
                Instantiate(sf31, hinge_c3.transform.position, this.transform.rotation);
                Instantiate(sf32, hinge_c3b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
        else if (core == 2)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf11, hinge_c1.transform.position, this.transform.rotation);
                Instantiate(sf12, hinge_c1b.transform.position, this.transform.rotation);
                Instantiate(sf31, hinge_c3.transform.position, this.transform.rotation);
                Instantiate(sf32, hinge_c3b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
        else if (core == 3)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf21, hinge_c2.transform.position, this.transform.rotation);
                Instantiate(sf22, hinge_c2b.transform.position, this.transform.rotation);
                Instantiate(sf11, hinge_c1.transform.position, this.transform.rotation);
                Instantiate(sf12, hinge_c1b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
    }

    IEnumerator busrd_to_other(float speed,int c)
    {
        if (c == 1) Instantiate(signal2_1, hinge_c1b.transform.position, this.transform.rotation);      
        else if (c == 2) Instantiate(signal2_2, hinge_c2b.transform.position, this.transform.rotation);      
        else if (c == 3) Instantiate(signal2_3, hinge_c3b.transform.position, this.transform.rotation);       


        int i;
        if (c == 1)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf11, hinge_c1.transform.position, this.transform.rotation);
                Instantiate(sf12, hinge_c1b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
        else if (c == 2)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf21, hinge_c2.transform.position, this.transform.rotation);
                Instantiate(sf22, hinge_c2b.transform.position, this.transform.rotation);                
                yield return new WaitForSeconds(speed / 33f);
            }
        }
        else if (c == 3)
        {
            for (i = 0; i < 33; i++)
            {
                Instantiate(sf31, hinge_c3.transform.position, this.transform.rotation);
                Instantiate(sf32, hinge_c3b.transform.position, this.transform.rotation);
                yield return new WaitForSeconds(speed / 33f);
            }
        }
    }

    IEnumerator busrdx(float speed, string command,bool hit)
    {
        int c, i;
        //string tmp_state = "";

        if (hit)
        {
            for (c = 1; c <= 3; c++)
            {
                if (core != c)
                    for (i = 1; i <= 16; i++)
                    {
                        if (String.Compare(GameObject.Find("C" + c + "_" + i).GetComponent<Text>().text, command) == 0)
                        {
                            //tmp_state = GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text;
                            if (lang == 0) logwrite("- Because of 'Invalid' cache of Core "+c+" with a copy of '" + command + "' in 'S' state, changes the state to 'I'",false); //found in Core" + c + " cache, state '" + tmp_state + "'", false);
                            else logwrite("- Εξαιτίας της δραστηριότητας 'Invalid' η cache του Core " + c + " που διαθέτει ένα αντίγραφο της λέξης '" + command + "' σε κατάσταση 'S', αλλάζει την κατάστασή της σε 'I'", false);
                            StartCoroutine(busrd_to_other(speed, c));
                            //yield return new WaitForSeconds(speed);
                            //GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "I";
                            //GameObject.Find("S" + core + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "M";
                        }
                    }
            }
            yield return new WaitForSeconds(speed);
            for (c = 1; c <= 3; c++)
            {
                if (core != c)
                    for (i = 1; i <= 16; i++)
                    {
                        if (String.Compare(GameObject.Find("C" + c + "_" + i).GetComponent<Text>().text, command) == 0)
                        {
                            GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "I";
                            //GameObject.Find("S" + core + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "M";
                        }
                    }
            }                           
        }
        else
        {
            yield return StartCoroutine(glowbusme(speed, core != 1, core != 2, core != 3, true));
            if (lang == 0) logwrite("- The corresponding copy in memory changes state from 'V' to 'I'", false);
            else logwrite("- Το αντίστοιχο αντίγραφο στη μνήμη αλλάζει κατάσταση από 'V' σε 'I'", false);
            StartCoroutine(busrdm(speed,true));
            yield return new WaitForSeconds(speed);
            //for (i = 1; i <= 64; i++)
           // {
           //     if (command == GameObject.Find("Canvas_mem/A" + i).GetComponent<Text>().text)  GameObject.Find("S"+ (Mathf.Ceil(i / 4f))).GetComponent<Text>().text="I";
           // }
        }
        yield return new WaitForSeconds(speed);

    }

    IEnumerator change_helper(float speed,int i,int c)
    {
        yield return new WaitForSeconds(speed);
        GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "I";
    }

    IEnumerator glowbusme(float speed, bool cr1, bool cr2, bool cr3, bool me)
    {
        int i;
        GameObject b = GameObject.Find("Canvas_Bus/Image");
        GameObject w1 = GameObject.Find("Canvas_Bus/Image_toc1");
        GameObject w12 = GameObject.Find("Canvas_Bus/Image_toc12");
        GameObject w2 = GameObject.Find("Canvas_Bus/Image_toc2");
        GameObject w22 = GameObject.Find("Canvas_Bus/Image_toc22");
        GameObject w3 = GameObject.Find("Canvas_Bus/Image_toc3");
        GameObject w32 = GameObject.Find("Canvas_Bus/Image_toc32");
        GameObject wm1 = GameObject.Find("Canvas_Bus/Image_tom1");
        GameObject wm2 = GameObject.Find("Canvas_Bus/Image_tom2");
        GameObject wm3 = GameObject.Find("Canvas_Bus/Image_tom3");

        for (i = 0; i < 20; i++)
        {
            b.GetComponent<Image>().color= b.GetComponent<Image>().color*1.05f;
            if (me)
            {
                wm1.GetComponent<Image>().color = wm1.GetComponent<Image>().color * 1.05f;
                wm2.GetComponent<Image>().color = wm2.GetComponent<Image>().color * 1.05f;
                wm3.GetComponent<Image>().color = wm3.GetComponent<Image>().color * 1.05f;
            }
            if (cr1)
            {
                w1.GetComponent<Image>().color = w1.GetComponent<Image>().color * 1.05f;
                w12.GetComponent<Image>().color = w12.GetComponent<Image>().color * 1.05f;
            }
            if (cr2)
            {
                w2.GetComponent<Image>().color = w2.GetComponent<Image>().color * 1.05f;
                w22.GetComponent<Image>().color = w22.GetComponent<Image>().color * 1.05f;
            }
            if (cr3)
            {
                w3.GetComponent<Image>().color = w3.GetComponent<Image>().color * 1.05f;
                w32.GetComponent<Image>().color = w32.GetComponent<Image>().color * 1.05f;
            }
            yield return new WaitForSeconds(speed / 100f);
        }
        for (i = 0; i < 20; i++)
        {
            b.GetComponent<Image>().color = b.GetComponent<Image>().color / 1.05f;
            if (me)
            {
                wm1.GetComponent<Image>().color = wm1.GetComponent<Image>().color / 1.05f;
                wm2.GetComponent<Image>().color = wm2.GetComponent<Image>().color / 1.05f;
                wm3.GetComponent<Image>().color = wm3.GetComponent<Image>().color / 1.05f;
            }
            if (cr1)
            {
                w1.GetComponent<Image>().color = w1.GetComponent<Image>().color / 1.05f;
                w12.GetComponent<Image>().color = w12.GetComponent<Image>().color / 1.05f;
            }
            if (cr2)
            {
                w2.GetComponent<Image>().color = w2.GetComponent<Image>().color / 1.05f;
                w22.GetComponent<Image>().color = w22.GetComponent<Image>().color / 1.05f;
            }
            if (cr3)
            {
                w3.GetComponent<Image>().color = w3.GetComponent<Image>().color / 1.05f;
                w32.GetComponent<Image>().color = w32.GetComponent<Image>().color / 1.05f;
            }
            yield return new WaitForSeconds(speed / 40f);
        }
    }


    IEnumerator wb(int cr, float speed, string command)
    {
        int i, num=0;
        bool go = false;
        for (i = 1; i <= 64; i++)
        {
            if (GameObject.Find("A" + i).GetComponent<Text>().text == command) { num = (int)Mathf.Ceil(i / 4f)%4; if (num == 0) num = 4; break; }           
        }

        if (num != 0)
            if (GameObject.Find("S" + cr + "_" + num).GetComponent<Text>().text == "M")
                if (GameObject.Find("C" + cr + "_" + (4 * num)).GetComponent<Text>().text != command)
                    if (GameObject.Find("C" + cr + "_" + (4 * num -1)).GetComponent<Text>().text != command)
                        if (GameObject.Find("C" + cr + "_" + (4 * num -2)).GetComponent<Text>().text != command)
                            if (GameObject.Find("C" + cr + "_" + (4 * num -3)).GetComponent<Text>().text != command)
                            {                                
                                go = true;
                            }
        yield return new WaitForSeconds(0f);

        if (go)
        {
            if (lang == 0) logwrite("Because of cache controller's direct mapping approach cache controller of Core "+cr+" must update main memory before replacing LN"+(num-1)+" \n", false);    
            else logwrite("Εξαιτίας της direct mapping προσέγγισης των cache controller, ο cache controller του Core " + cr + " πρέπει να ενημερώσει την μνήμη πριν αντικαταστήσει την LN" + (num - 1) + " \n", false);
            yield return new WaitForSeconds(speed);
            GameObject.Find("S" + cr + "_" + num).GetComponent<Text>().text = "E";
            if (lang == 0) logwrite("- Cache controller of Core "+cr+" posts the bus transaction 'BusWB', puts its copy on the bus and changes its local copy from 'M' to 'E'", false);
            else logwrite("- Ο Cache controller του Core " + cr + " ενεργοποιεί την δραστηριότητα διαύλου 'BusWB', τοποθετεί το αντίγραφο στον δίαυλο και αλλάζει την κατάσταση του τοπικού αντιγράφου από 'M' σε 'E'", false);
            StartCoroutine(busrd_fromc_tobus(speed, cr));
            yield return new WaitForSeconds(speed);
            yield return StartCoroutine(glowbusme(speed, cr != 1, cr != 2, cr != 3, true));

            if (lang == 0) logwrite("- Memory controller updates its corresponding block \n", false);
            else logwrite("- Ο ελεγκτής μνήμης ενημερώνει το αντίστοιχο block \n", false);
            StartCoroutine(busrdm(speed, true));
            yield return new WaitForSeconds(speed);
        }
    }


    IEnumerator bus(float speed,string command)
    {       
        int i,c,where1=0,where2=0;
        string tmp_state="";
        bool found_e = false,found_s=false,found_m=false,hlp1=false;

        //cnv_block.SetActive(true);
        //glowme(command, true);

        yield return StartCoroutine(glowbusme(speed,core!=1,core!=2,core!=3,true));
        
        //yield return 0;
        // yield return new WaitForSeconds(speed);
        //StartCoroutine(busrd_to_others(speed));
        //yield return new WaitForSeconds(speed);

        for (c = 1; c <= 3; c++)
        {
            if(core!= c)
            for (i = 1; i <= 16; i++)
            {                
                if (String.Compare(GameObject.Find("C" + c + "_" + i).GetComponent<Text>().text, command)==0)
                {                    
                    tmp_state = GameObject.Find("S"+c+"_"+Mathf.Ceil(i / 4f)).GetComponent<Text>().text;
                        if (tmp_state == "E")
                        {
                            found_e = true;
                            if (todo == "read")
                            {
                                where1 = c; where2 = i;
                                if (lang == 0) logwrite("- Cache controller of Core " + c + " reads the bus", false);
                                else logwrite("- Ο Cache controller του Core " + c + " διαβάζει τον δίαυλο", false);
                                StartCoroutine(busrd_to_other(speed, c));
                                yield return new WaitForSeconds(speed);

                                GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "S";
                                if (lang == 0) logwrite("- Cache controller of Core " + c + " puts the requested '" + command + "' word on the bus and changes the state of the corresponding cache line from 'E' to 'S'", false);
                                else logwrite("- Ο Cache controller του Core " + c + " τοποθετεί την αιτηθείσα λέξη '" + command + "' στο δίαυλο και αλλάζει την κατάσταση της αντίστοιχης cache line από 'E' σε 'S'", false);
                                StartCoroutine(busrd_fromc_tobus(speed, c));
                                yield return new WaitForSeconds(speed);
                                yield return StartCoroutine(glowbusme(speed, c != 1, c != 2, c != 3, true));

                                if (lang == 0) logwrite("- Cache controller of Core " + core + " reads '" + command + "' from the bus marked 'S'", false);
                                else logwrite("- Ο Cache controller του Core " + core + " διαβάζει την λέξη '" + command + "' από τον δίαυλο σε κατάσταση 'S'", false);
                                StartCoroutine(busrd(speed, false));
                                yield return new WaitForSeconds(speed);
                                GameObject.Find("S" + core + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "S";
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 1)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 1)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 2)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 2)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 3)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 3)).GetComponent<Text>().text;
                            }
                            else if (todo == "write")
                            {
                                if (lang == 0) logwrite("- Because of 'BusRdX' cache of Core " + c + " with a copy of '"+command+"' in '"+tmp_state+"' state, changes the state to 'I'", false);
                                else logwrite("- Εξαιτίας της δραστηριότητας 'BusRdX' η cache του Core " + c + " που διαθέτει ένα αντίγραφο της λέξης '" + command + "' σε κατάσταση '" + tmp_state + "', αλλάζει την κατάστασή του σε 'I'", false);
                                //StartCoroutine(busrdm(speed, true));
                                StartCoroutine(busrd_to_other(speed, c));
                                yield return new WaitForSeconds(speed);

                                GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "I";
                                if (lang == 0) logwrite("- Memory controller puts the requested '" + command + "' word on the bus", false);
                                else logwrite("- Ο ελεγκτής μνήμης τοποθετεί την αιτηθείσα λέξη '" + command + "' στο δίαυλο", false);
                                StartCoroutine(busrdm(speed, false));
                                yield return new WaitForSeconds(speed);

                                if (lang == 0) logwrite("- Cache controller of Core "+core+" reads '" + command + "' from the bus, marked 'M'", false);
                                else logwrite("- Ο Cache controller του Core " + core + " διαβάζει την λέξη '" + command + "' από τον δίαυλο σε κατάσταση 'M'", false);
                                StartCoroutine(busrd(speed, false));
                                yield return new WaitForSeconds(speed);

                                GameObject.Find("S" + core + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "M";
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 1)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 1)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 2)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 2)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 3)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 3)).GetComponent<Text>().text;

                            }
                        }
                        else if (tmp_state == "S")
                        {
                            if (!hlp1)
                            {
                                where1 = c; where2 = i;
                                found_s = true;
                                if (todo == "read")
                                {
                                    if (lang == 0) logwrite("- Cache controller of Core " + c + " (arbitrated) reads the BUS", false);
                                    else logwrite("- Ο Cache controller του Core " + c + " (αυθαίρετα) διαβάζει τον δίαυλο", false);
                                }
                                else if (todo == "write")
                                {
                                    if (lang == 0) logwrite("- Because of 'BusRdx' cache of Core "+c+" with a copy of '"+command+"' in 'S' state, changes the state to 'I'", false);
                                    else logwrite("- Εξαιτίας της δραστηριότητας 'BusRdx' η cache του Core " + c + " που διαθέτει ένα αντίγραφο της λέξης '" + command + "' σε κατάσταση 'S', αλλάζει την κατάστασή του σε 'I'", false);
                                }
                                StartCoroutine(busrd_to_other(speed, c));
                            }
                            if (todo == "read") hlp1=true;
                            if(todo=="write") StartCoroutine(change_helper(speed, i,c));
                        }
                    else if (tmp_state == "M")
                        {
                            where1 = c; where2 = i;                            
                            found_m = true;
                            if (todo == "read")
                            {
                                if (lang == 0) logwrite("- Cache controller of Core " + c + " reads the BUS", false);
                                else logwrite("- Ο Cache controller του Core " + c + " διαβάζει τον δίαυλο", false);
                                StartCoroutine(busrd_to_other(speed, c));
                                yield return new WaitForSeconds(speed);
                            }
                            else if(todo=="write")
                            {
                                GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "I";
                                if (lang == 0) logwrite("- Because of 'BusRdX', cache controller of core " + c + " posts the bus transaction 'BusWB', puts its copy on the bus and changes its local copy from 'M' to 'I'", false);
                                else logwrite("- Εξαιτίας της δραστηριότητας 'BusRdX', ο cache controller του core " + c + " ενεργοποιεί την δραστηριότητα διαύλου 'BusWB', τοποθετεί το αντίγραφό του στο δίαυλο και αλλάζει το τοπικό του αντίγραφο από 'M' σε 'I'", false);
                                again = true;
                            }

                            if (todo == "read")
                            {
                                GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "S";
                                if (lang == 0) logwrite("- Cache controller of Core" + c + " puts the requested '" + command + "' word on the bus, changes the corresponding cache line from 'M' to 'S' and posts the bus transaction 'BusWB'", false);
                                else logwrite("- Ο Cache controller του Core" + c + " τοποθετεί την αιτηθείσα λέξη '" + command + "' στο δίαυλο, αλλάζει την κατάσταση της αντίστοιχης cache line από 'M' σε 'S' και ενεργοποιεί την δραστηριότητα διαύλου 'BusWB'", false);
                            }
                            StartCoroutine(busrd_fromc_tobus(speed, c));
                            yield return new WaitForSeconds(speed);
                            yield return StartCoroutine(glowbusme(speed, c != 1, c != 2, c != 3, true));

                            if (todo == "read")
                            {
                                if (lang == 0) logwrite("- Cache controller of Core "+core+" reads '"  + command + "' from the BUS marked 'S'", false);
                                else logwrite("- Ο Cache controller του Core " + core + " διαβάζει την λέξη '" + command + "' από το δίαυλο σε κατάσταση 'S'", false);

                                if (lang == 0) logwrite("- Memory controller reads '" + command + "' from the BUS and updates the corresponding block", false);//, updates the corresponding block changing its state from 'I' to 'V'", false);
                                else logwrite("- Ο ελεγκτής μνήμης διαβάζει την λέξη '" + command + "' από το δίαυλο και ενημερώνει το αντίστοιχο block", false);
                                StartCoroutine(busrd(speed, false));
                            }
                            else if (todo == "write")
                            {
                                if (lang == 0) logwrite("- Memory controller updates the corresponding block in memory", false);
                                else logwrite("- Ο ελεγκτής μνήμης ενημερώνει το αντίστοιχο block στη μνήμη", false);
                            }

                            StartCoroutine(busrdm(speed, true));
                            yield return new WaitForSeconds(speed);
                            if (todo == "read")
                            {
                                GameObject.Find("S" + core + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "S";
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 1)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 1)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 2)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 2)).GetComponent<Text>().text;
                                GameObject.Find("C" + core + "_" + (Mathf.Ceil(i / 4f) * 4 - 3)).GetComponent<Text>().text = GameObject.Find("C" + c + "_" + (Mathf.Ceil(i / 4f) * 4 - 3)).GetComponent<Text>().text;
                            }

                           // int tcore=0;
                           // if (todo == "read") tcore = core;
                           // else if (todo == "write") tcore = c;
                           // for (j = 1; j <= 16; j++)
                           //     {
                           //         if (GameObject.Find("S" + j).GetComponent<Text>().text == "I")
                           //         {
                           //             if (GameObject.Find("A" + (j * 4)).GetComponent<Text>().text == GameObject.Find("C" + tcore + "_" + (Mathf.Ceil(i / 4f) * 4)).GetComponent<Text>().text)
                           //             {
                           //                 if (GameObject.Find("A" + (j * 4 - 1)).GetComponent<Text>().text == GameObject.Find("C" + tcore + "_" + (Mathf.Ceil(i / 4f) * 4 - 1)).GetComponent<Text>().text)
                           //                 {
                           //                     if (GameObject.Find("A" + (j * 4 - 2)).GetComponent<Text>().text == GameObject.Find("C" + tcore + "_" + (Mathf.Ceil(i / 4f) * 4 - 2)).GetComponent<Text>().text)
                           //                     {
                           //                         if (GameObject.Find("A" + (j * 4 - 3)).GetComponent<Text>().text == GameObject.Find("C" + tcore + "_" + (Mathf.Ceil(i / 4f) * 4 - 3)).GetComponent<Text>().text)
                           //                         {
                           //                             GameObject.Find("S" + j).GetComponent<Text>().text = "V";
                           //                         }
                           //                     }
                           //                 }
                           //             }
                           //         }
                           //     }

                            if (todo == "write")
                            {
                                GameObject.Find("S" + c + "_" + Mathf.Ceil(i / 4f)).GetComponent<Text>().text = "I";
                                if (lang == 0) logwrite("- Cache controller of core "+core+" posts again the bus transaction 'BusRdx'", false);
                                else logwrite("- Ο Cache controller του core " + core + " ενεργοποιεί πάλι τη δραστηριότητα διαύλου 'BusRdx'", false);
                                StartCoroutine(busrd(speed, true));
                                yield return new WaitForSeconds(speed);
                                StartCoroutine(bus(speed, command));
                            }

                        }
                    }
            }
        }

        if (found_e || found_m) { cnv_block.SetActive(false); glowme(command, false); yield break; }
        else if (found_s)
        {
            if (todo == "read")
            {
                yield return new WaitForSeconds(speed);
                if (lang == 0) logwrite("- Cache controller of Core "+where1+" puts the requested '"+command+"' word on the bus", false);
                else logwrite("- Ο Cache controller του Core " + where1 + " τοποθετεί την αιτηθείσα λέξη '" + command + "' στο δίαυλο", false);
                StartCoroutine(busrd_fromc_tobus(speed, where1));
                yield return new WaitForSeconds(speed);
                yield return StartCoroutine(glowbusme(speed, where1 != 1, where1 != 2, where1 != 3, true));
            }
            else if (todo == "write")
            {
                StartCoroutine(busrdm(speed, true));
                yield return new WaitForSeconds(speed);

                if (lang == 0) logwrite("- Memory controller puts the requested '" + command + "' on the bus", false);
                else logwrite("- Ο ελεγκτής μνήμης τοποθετεί την αιτηθείσα λέξη '" + command + "' στο δίαυλο", false);
                StartCoroutine(busrdm(speed, false));
                yield return new WaitForSeconds(speed);
            }

            if (todo == "read")
            {
                if (lang == 0) logwrite("- Cache controller of Core "+core+" reads '" + command + "' word from the BUS marked 'S'", false);
                else logwrite("- Ο Cache controller του Core " + core + " διαβάζει τη λέξη '" + command + "' από το δίαυλο σε κατάσταση 'S'", false);
            }
            if (todo == "write")
            {
                if (lang == 0) logwrite("- Cache controller of Core " + core + " reads '" + command + "' word from the BUS marked 'M'", false);
                else logwrite("- Ο Cache controller του Core " + core + " διαβάζει την λέξη '" + command + "' από το δίαυλο σε κατάσταση 'M'", false);
            }
            StartCoroutine(busrd(speed, false));
            yield return new WaitForSeconds(speed);
            if (todo == "read") GameObject.Find("S" + core + "_" + Mathf.Ceil(where2 / 4f)).GetComponent<Text>().text = "S";
            else if (todo == "write") GameObject.Find("S" + core + "_" + Mathf.Ceil(where2 / 4f)).GetComponent<Text>().text = "M";
            GameObject.Find("C" + core + "_" + (Mathf.Ceil(where2 / 4f) * 4)).GetComponent<Text>().text = GameObject.Find("C" + where1 + "_" + (Mathf.Ceil(where2 / 4f) * 4)).GetComponent<Text>().text;
            GameObject.Find("C" + core + "_" + (Mathf.Ceil(where2 / 4f) * 4 - 1)).GetComponent<Text>().text = GameObject.Find("C" + where1 + "_" + (Mathf.Ceil(where2 / 4f) * 4 - 1)).GetComponent<Text>().text;
            GameObject.Find("C" + core + "_" + (Mathf.Ceil(where2 / 4f) * 4 - 2)).GetComponent<Text>().text = GameObject.Find("C" + where1 + "_" + (Mathf.Ceil(where2 / 4f) * 4 - 2)).GetComponent<Text>().text;
            GameObject.Find("C" + core + "_" + (Mathf.Ceil(where2 / 4f) * 4 - 3)).GetComponent<Text>().text = GameObject.Find("C" + where1 + "_" + (Mathf.Ceil(where2 / 4f) * 4 - 3)).GetComponent<Text>().text;

        }
        else   //get the block from main cache
        {
            if (!again)
            {
                if (lang == 0) logwrite("- '" + command + "' wasn't found in any core cache", false);
                else logwrite("- Η λέξη '" + command + "' δεν βρέθηκε σε καμία cache", false);
            }
            else again = false;
            //StartCoroutine(busrdm(speed, true));
            //yield return new WaitForSeconds(speed);
            if (lang == 0) logwrite("- Memory controller puts the requested '" + command + "' word on the bus", false);
            else logwrite("- Ο ελεγκτής μνήμης τοποθετεί την αιτηθείσα λέξη '" + command + "' στο δίαυλο", false);
            StartCoroutine(busrdm(speed, false));
            yield return new WaitForSeconds(speed);
            yield return StartCoroutine(glowbusme(speed, where1 != 1, where1 != 2, where1 != 3, false));
            if (todo == "read")
            {
                if (lang == 0) logwrite("- Cache controller of Core " + core + " reads '" + command + "' from the bus marked 'E'", false);
                else logwrite("- Ο Cache controller του Core " + core + " διαβάζει τη λέξη '" + command + "' από το δίαυλο σε κατάσταση 'E'", false);
            }
            else if (todo == "write")
            {
                if (lang == 0) logwrite("- Cache controller of Core " + core + " reads '" + command + "' from the bus marked 'M'", false);
                else logwrite("- Ο Cache controller του Core " + core + " διαβάζει τη λέξη '" + command + "' από το δίαυλο σε κατάσταση 'M'", false);
            }
            StartCoroutine(busrd(speed, false));
            yield return new WaitForSeconds(speed);

            for (i = 1; i <= 64; i++)
            {
                if (String.Compare(GameObject.Find("A" + i).GetComponent<Text>().text, command) == 0)
                {
                    where2 = i;

                    int line = (int)Mathf.Ceil(where2 / 4f) % 4;
                    if (line == 0) line = 4;

                    if (todo == "read") GameObject.Find("S" + core + "_" + line).GetComponent<Text>().text = "E";
                    else GameObject.Find("S" + core + "_" + line).GetComponent<Text>().text = "M";

                    GameObject.Find("C" + core + "_" + (line * 4)).GetComponent<Text>().text = GameObject.Find("A" + (Mathf.Ceil(where2 / 4f) * 4)).GetComponent<Text>().text;
                    GameObject.Find("C" + core + "_" + (line * 4 - 1)).GetComponent<Text>().text = GameObject.Find("A" + (Mathf.Ceil(where2 / 4f) * 4 - 1)).GetComponent<Text>().text;
                    GameObject.Find("C" + core + "_" + (line * 4 - 2)).GetComponent<Text>().text = GameObject.Find("A" + (Mathf.Ceil(where2 / 4f) * 4 - 2)).GetComponent<Text>().text;
                    GameObject.Find("C" + core + "_" + (line * 4 - 3)).GetComponent<Text>().text = GameObject.Find("A" + (Mathf.Ceil(where2 / 4f) * 4 - 3)).GetComponent<Text>().text;

                }
            }

        }
        //cnv_block.SetActive(false);
        //glowme(command, false);
    }
}
