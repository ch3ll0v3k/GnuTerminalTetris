using System;

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

public class MainWorld{

    // =====================================================================
    // User
    public int fieldSizeX;
    public int fieldSizeY;

    public int platformPosX;
    public int platformPosY;

    public float ballPosX;
    public float ballPosY;

    
    public int posXCorrection = 9;
    public int posYCorrection = 1;

    //----------------------------------------
    // Filed
    public string SOLID_ROW = ""; 
    public string DRAWER;

    public string PLATFORT_CHR = "##########"; 
    public int PLATFORT_TOP_OFFSET = 5; 

    public string BALL_CHR = "O"; 
    public float BALL_SPEED = 1.0F; //0.25F;
    public string EMPTY_CHR = " "; 
    public string SOLID_CHR = "="; 
    public string COL_SHR = "|"; 


    //----------------------------------------
    public bool isAlife = true;
    public bool XIs200 = false;
    public bool YIs200 = false;


    public string MESSAGE = "";
    public string MESSAGE_PAUSE = "";
    //----------------------------------------
    // 0 == NO MOVEMENT in eny direction of 'X' or 'Y'
    // '-1' == UP for 'Y' or LEFT for 'X'
    // '+1' == DOWN for 'Y' or RIGHT for 'X'

    private int _VecX = 0; 
    private int _VecY = -1; 

    //----------------------------------------
    // OPTIONS
    public bool PAUSED = false;
    //----------------------------------------
    public int worldUpdateSpeed = 1000/20;
    //----------------------------------------

    // =====================================================================
    public MainWorld(int[] fieldSize){

        // -----------------------------------------------------------------
        fieldSizeX = fieldSize[0];
        fieldSizeY = fieldSize[1];

        platformPosX = ( (fieldSizeX/2)-(PLATFORT_CHR.Length/2) );
        platformPosY = ( (fieldSizeY - PLATFORT_TOP_OFFSET) );

        ballPosX = ( (fieldSizeX/2)-(PLATFORT_CHR.Length/2) );
        ballPosY = platformPosY + 2;
        // -----------------------------------------------------------------
        while(_VecX == 0)
            _VecX = _GetRandom(-1, 1);

        // -----------------------------------------------------------------

    }

    // =====================================================================
    public void Init(){

        // -----------------------------------------------------------------
        CreateSolidRow();
        // -----------------------------------------------------------------

    }
    // =====================================================================
    public void UpdateAllEntitys(){

        // -----------------------------------------------------------------            
        XIs200 = false;
        YIs200 = false;
        MESSAGE = "";
        // -----------------------------------------------------------------            
        // _Vector X movement
        if(_VecX < 0){
            ballPosX--; if(ballPosX < 1) _VecX = 1;
        }else if(_VecX > 0){
            ballPosX++; if(ballPosX > fieldSizeX-1) _VecX = -1; 
        } 
        //else{ // No movement increase or decrease }
        // --------------------------
        // _Vector Y movement
        if(_VecY < 0){ 
            ballPosY--; if(ballPosY < 1) _VecY = 1;
        }else if(_VecY > 0){ 

            ballPosY++; 

            if( 
                (ballPosY == (platformPosY-posYCorrection) ) 
                && 
                (ballPosX >= (platformPosX-posXCorrection) ) 
                && 
                (ballPosX <= ((platformPosX-posXCorrection) + PLATFORT_CHR.Length) ) 
            ){
            
                _VecY = -1;

            }else if(
                (ballPosY >= (platformPosY-posYCorrection) ) 
                && (
                    (ballPosX < (platformPosX-posXCorrection) ) 
                    || 
                    (ballPosX > ((platformPosX-posXCorrection) + PLATFORT_CHR.Length) ) 
                )
            ){
                isAlife = false;
                MESSAGE = " YOU LOSE ";
            }else{
                //isAlife = false;
            }
        } 
        //else{ // No movement increase or decrease }
        // -----------------------------------------------------------------            

    }
    // =====================================================================
    public void UpdateUserPos(ConsoleKey key){

        // -----------------------------------------------------------------            
        switch(key){
            // -----------------------------
            // Movement controls
            case ConsoleKey.LeftArrow: if(platformPosX < PLATFORT_CHR.Length+1) break; platformPosX -= 2; break;
            //case ConsoleKey.UpArrow: if(platformPosY < 1) break; platformPosY--; break;
            case ConsoleKey.RightArrow: if(platformPosX > fieldSizeX-3) break; platformPosX += 2; break;
            //case ConsoleKey.DownArrow: if(platformPosY > fieldSizeY-2) break; platformPosY++; break;
            // -----------------------------
            case ConsoleKey.P:

                PAUSED = !PAUSED; MESSAGE_PAUSE = " "+((PAUSED) ? "PAUSED = true" : "PAUSED = false");

            break;
            
            // -----------------------------
        }
        // -----------------------------------------------------------------

    }
    // =====================================================================
    public void RedrawWord(){

        // -----------------------------------------------------------------
        Console.Clear();

        Console.WriteLine(COL_SHR + SOLID_ROW + COL_SHR);
        for(int _Y=0; _Y < fieldSizeY; _Y++){

            DRAWER = "";

            for(int _X=0; _X < fieldSizeX; _X++){

                // -----------------------------------------------            
                if(_Y == platformPosY && _X == platformPosX){
                    DRAWER = DRAWER.Substring(0, DRAWER.Length - PLATFORT_CHR.Length+1);
                    DRAWER += PLATFORT_CHR;
                }else
                    DRAWER += EMPTY_CHR;

                // -----------------------------------------------            
                if(_Y == ballPosY && _X == ballPosX){ 
                    DRAWER = DRAWER.Substring(0, DRAWER.Length - 1);
                    DRAWER += BALL_CHR;
                }
                // -----------------------------------------------            
            }
            Console.WriteLine(COL_SHR + DRAWER + COL_SHR);

        }
        Console.WriteLine(COL_SHR+SOLID_ROW+COL_SHR);
        Console.WriteLine(COL_SHR + " PposX_L: " + (platformPosX-9).ToString() + " PposX_LR: " + ( platformPosX - posXCorrection + PLATFORT_CHR.Length ).ToString() + COL_SHR);
        Console.WriteLine(COL_SHR + " P: " + platformPosX.ToString() + " | B: "+ ballPosX.ToString() + COL_SHR);
        Console.WriteLine(COL_SHR + MESSAGE + COL_SHR);
        Console.WriteLine(COL_SHR + MESSAGE_PAUSE + COL_SHR);
        Console.WriteLine(COL_SHR+SOLID_ROW+COL_SHR);
        // -----------------------------------------------------------------
    
    }
    // =====================================================================
    public void CreateSolidRow(){
        
        // -----------------------------------------------------------------
        for(int i=0; i < fieldSizeX; i++){ SOLID_ROW += SOLID_CHR; }
        // -----------------------------------------------------------------

    }

    // =====================================================================
    public int _GetRandom(int A, int B){ 
        Random RND = new Random(); return RND.Next(A, B);
    }
    // =====================================================================
    public int _ReadInt(){ return int.Parse(Console.ReadLine()); }
    // =====================================================================
    public string _ReadLine(){ return Console.ReadLine(); }
    // =====================================================================
    public void _WriteLine(string str){ Console.WriteLine(str); }
    // =====================================================================

}

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::