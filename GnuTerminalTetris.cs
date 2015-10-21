using System;
using System.Timers;
// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


class MonoConsolelGame{

    // =====================================================================
    static Timer aTimer;
    static ConsoleKeyInfo CKI;
    static MainWorld MW;
    //static int worldUpdateSpeed = 1000/18;

    static bool DEBUG;
    static bool SHOW_MENU = true;

    // =====================================================================
    static void Main(string[] argv){

        // -----------------------------------------------------------------
        //File FS = new File(); // Red config from user.cnf

        // -----------------------------------------------------------------
        bool allowToRun = false;
        string answ = "";

        while(SHOW_MENU){

            Console.Clear();
            _WriteLine("");
            _WriteLine("\t\t|---------------------------------------------------------|");
            _WriteLine("\t\t|               Welcome to Terminal Battle                |");
            _WriteLine("\t\t|---------------------------------------------------------|");
            _WriteLine("\t\t| 1)  Init new level (DEBUG ENABLE)                       |");
            _WriteLine("\t\t| 2)  Init new level (DEBUG DISABLE)                      |");
            _WriteLine("\t\t| 3)  Continue level                                      |");
            _WriteLine("\t\t|                                                         |");
            _WriteLine("\t\t| 4)  Exit                                                |");
            _WriteLine("\t\t|---------------------------------------------------------|\n");

            answ = _ReadLine().ToLower();
            // ------------------------------------
            switch(answ){
                // ________________________________
                case "1":  
                    DEBUG = true; allowToRun = true; SHOW_MENU = false; break;
                // ________________________________
                case "2":  
                    DEBUG = false; allowToRun = true; SHOW_MENU = false; break;
                // ________________________________
                case "3":  
                    allowToRun = true; SHOW_MENU = false; break;
                // ________________________________
                case "4":  
                    allowToRun = false; SHOW_MENU = false; break;
                // ________________________________
            }
            // ------------------------------------
        }


        // -----------------------------------------------------------------
        if(allowToRun){
            Updater();
        }
        // -----------------------------------------------------------------

    }
    // =====================================================================
    private static void Updater(){

        // -----------------------------------------------------------------
        int[] fieldSize = {90, 34};
        // -----------------------------------------------------------------
        MW = new MainWorld(fieldSize);
        MW.Init();

        aTimer = new Timer();
        InitTimer();
        // -----------------------------------------------------------------
        while(true){

            //CKI = Console.ReadKey();
            if(!MW.isAlife){
                aTimer.AutoReset = false;
                aTimer.Enabled = false;
                aTimer.Dispose();
                Console.Clear();
                YouLose();
                break;
            }else{
            
                if(!DEBUG)
                    MW.UpdateUserPos(Console.ReadKey().Key);
            }
        }
        // -----------------------------------------------------------------
    }

    // =====================================================================
    private static void InitTimer(){

        // -----------------------------------------------------------------
        aTimer = new System.Timers.Timer(MW.worldUpdateSpeed); 
        aTimer.Elapsed += UpdateWorldEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
        // -----------------------------------------------------------------

    }

    // =====================================================================
    private static void UpdateWorldEvent(Object source, ElapsedEventArgs e){

        // -----------------------------------------------------------------
        if(!MW.isAlife){
            aTimer.AutoReset = false;
            aTimer.Enabled = false;
            aTimer.Dispose();
            Console.Clear();
            YouLose();
            return;

        }else{

            if(DEBUG){
                MW.UpdateUserPos(Console.ReadKey().Key);
            }
            
            MW.UpdateAllEntitys();
            MW.RedrawWord();

        }
        // -----------------------------------------------------------------
    }

    // =====================================================================
    private static void YouLose(){

        // -----------------------------------------------------------------
        _WriteLine("");
        _WriteLine("\t\t|---------------------------------------------------------|");
        _WriteLine("\t\t|---------------------------------------------------------|");
        _WriteLine("\t\t|                       You lose !!!                      |");
        _WriteLine("\t\t|---------------------------------------------------------|");
        _WriteLine("\t\t|---------------------------------------------------------|\n");

        // -----------------------------------------------------------------

    }
    // =====================================================================
    public static int _GetRandom(int A, int B){ 
        Random RND = new Random(); return RND.Next(A, B);
    }
    // =====================================================================
    public static int _ReadInt(){ return int.Parse(Console.ReadLine()); }
    // =====================================================================
    public static string _ReadLine(){ return Console.ReadLine(); }
    // =====================================================================
    public static void _WriteLine(string str){ Console.WriteLine(str); }
    // =====================================================================

}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::