using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class data : MonoBehaviour
{


   // string[,] cache, core1, core2, core3, core4, core5, core6, core7, core8;
   // public int core_num, block_num;
   // public string[] expr, val;

    // Use this for initialization
    void Start()
    {


      //  cache = new string[16, 5];


    }

    // Update is called once per frame
    void Update()
    {

    }

}
    
/*
    // METHODS //
    //M1: EXECUTE COMMAND FROM TERMINAL
    public void terminal_run(int core, string command)
    {
        if (expr_to_array(command).Length == 1)  //bad statement
        {
            logwrite("- Core " + core + ": Instruction '" + command + "' is INVALID",true);
            return;         
        } else
        {
            logwrite("- Core " + core + ": Instruction '" + command + "' is VALID", true);
        }

        if (expr_to_array(command).Length == 2)   //if simple var value declaration
        {
            if (find_var(expr_to_array(command)[0])[0] == "true") {  //IF simple var value declaration  + EXISTS IN MAIN MEM
                GameObject.Find("A" + int.Parse(find_var(expr_to_array(command)[0])[1])).GetComponent<Text>().text = command;
                logwrite("ola cool",false);                
            } else {                                 //if simple var value declaration  + doesn't exist in main mem
                Debug.Log("den yparxei sto main mem"); 
            }
        }
        else if (expr_to_array(command).Length == 6) //if simple proccess
        {


        }
            Debug.Log(command);
    }


    //M2: COMMAND SPLITTED (return: 1values: invalid(false), 2values:c1=5(c1,5), 6values:c1=c5+6(c1,+,var,c5,int,6) )
    public string[] expr_to_array(string ar)
    {
        string[] EQ = new string[4] { "+", "-", "*", "/"};
          string[] tmpa = ar.Split('=');
        //INSERT VALUE
        if (tmpa.Length == 2)
        {
            return tmpa;
        }
        else return new string[1] { "false" };
    }

    //M3: EXTRACT simple GIVE VALUE command
    public string[] extract(int n)
    {      
        return GameObject.Find("A" + n).GetComponent<Text>().text.Split('=');        
    }

    //M4: SEARCH FOR VARIABLE IN MAIN MEM (return0: EXIST?, return2: number of finding)
    public string[] find_var(string vname)
    {
        int i;
        for (i = 1; i <= 64; i++)
        {
            if(extract(i)[0]== vname) return new string[2] { "true", i.ToString()};
        }
        return new string[2] { "false", "false" };
    }

    //M5: Find state number in main memory
    public int find_state_main(int n)
    {
        int i;
        for(i=1;i<=16;i++)
        {
            if (n <= i * 4) return i;
        }
        return 0;
    }

    //M6: Write to LOG
    public void logwrite(string st,bool newlog)
    {
        if(newlog) GameObject.Find("Text_log").GetComponent<Text>().text = st;
          else GameObject.Find("Text_log").GetComponent<Text>().text = GameObject.Find("Text_log").GetComponent<Text>().text + "\n" + st;
    }
}
*/