using System;

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

public class TetrisClass{

    // =====================================================================
    // User
    public int fieldSizeX;
    public int fieldSizeY;

    //----------------------------------------
    // Filed 
    // int[] fieldSize = {40, 36}; // 38_x 34_y
    public string DRAWER = "";
    public string SOLID_CHR = "=";
    public string SOLID_ROW = "";
    public string FIELD_OFFSET = "";
    public string COL_SHR = "|";
    public string EMPTY_CHR = " ";
    public string OBJECT_CHR = "#";


    //----------------------------------------
    // OPTIONS
    public bool PAUSED = false;
    public bool STOP_GAME = false;
    public int worldUpdateSpeed = 1000/4;
    

    private int[ , , ] ObjectMatrix; 
    //----------------------------------------

    // =====================================================================
    public TetrisClass(int[] fieldSize){

        // -----------------------------------------------------------------
        fieldSizeX = fieldSize[0];
        fieldSizeY = fieldSize[1];
        // -----------------------------------------------------------------
        // int[] fieldSize = {40, 36}; // 38_x 34_y

        ObjectMatrix = new int[ , , ]{

            {
                { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }
            },{
                { 0,0 }, { 0,0 }, {15,8 }, {16,8 }, {17,8 }, {18,8 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }
            },{
                { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, {18,9 }, {19,9 }, {20,9 }, {21,9 }, { 0,0 }
            },{
                { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }
            },{
                { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }, { 0,0 }
            }

        };
        // -----------------------------------------------------------------

    }

    // =====================================================================
    public void Init(){

        // -----------------------------------------------------------------
        SetCenterOfTerminal();
        CreateSolidRow();
        // -----------------------------------------------------------------

    }
    // =====================================================================
    public void UpdateAllEntitys(){
        
        // -----------------------------------------------------------------            
        for(int _Y=0; _Y< 5; _Y++){
            for(int _X=0; _X< 10; _X++){
                if(ObjectMatrix[_Y, _X, 1] != 0) ObjectMatrix[_Y, _X, 1]++; 
            }
        }    
        // -----------------------------------------------------------------            

    }
    // =====================================================================
    public void UpdateObjectPos(ConsoleKey key){

        // -----------------------------------------------------------------            
        switch(key){
            case ConsoleKey.LeftArrow:
                MoveObject("L");
                break;
            case ConsoleKey.RightArrow:
                MoveObject("R");
                break;
            case ConsoleKey.Spacebar:
                RotateObject();
                break;

            case ConsoleKey.P: PAUSED = !PAUSED;                        break;            
            case ConsoleKey.Escape: STOP_GAME = true;                   break;            
        }
        // -----------------------------------------------------------------

    }
    // =====================================================================
    public void RedrawWord(){

        // -----------------------------------------------------------------
        Console.Clear();
        Console.WriteLine(FIELD_OFFSET);
        Console.WriteLine(FIELD_OFFSET);
        Console.WriteLine(FIELD_OFFSET + COL_SHR + SOLID_ROW + COL_SHR);
        // -----------------------------------------------------------------
        bool found = false;

        for(int Y=0; Y < fieldSizeY; Y++){

            DRAWER = "";
            for(int X=0; X < fieldSizeX; X++){


                for(int _Y=0; _Y< 5; _Y++){
                    if(!found){
                        for(int _X=0; _X< 10; _X++){

                            // -----------------------------------------------
                            if(ObjectMatrix[_Y, _X, 0] == X && ObjectMatrix[_Y, _X, 1] == Y){
                                found = true;
                                break;                                
                            }
                            // -----------------------------------------------
                        } // for 4
                    }else{ break; } // if(!found)
                } // for 3
            
                if(found){
                    DRAWER += OBJECT_CHR;
                    found = false;
                }else{
                    DRAWER += EMPTY_CHR;
                }


            } // for 2
            Console.WriteLine(FIELD_OFFSET + COL_SHR + DRAWER + COL_SHR);
        } // for 1

        // -----------------------------------------------------------------
        Console.WriteLine(FIELD_OFFSET + COL_SHR + SOLID_ROW + COL_SHR);
        // -----------------------------------------------------------------
        /*
        Console.WriteLine(FIELD_OFFSET + " " + " PposX_L: " + (platformPosX-9).ToString() + " PposX_LR: " + ( platformPosX - posXCorrection + PLATFORT_CHR.Length ).ToString());
        Console.WriteLine(FIELD_OFFSET + " " + " P: " + platformPosX.ToString() + " | B: "+ ballPosX.ToString());
        Console.WriteLine(FIELD_OFFSET + " " + MESSAGE );
        Console.WriteLine(FIELD_OFFSET + " " + MESSAGE_PAUSE );
        Console.WriteLine(FIELD_OFFSET + " " + SOLID_ROW );
        */
        // -----------------------------------------------------------------
    
    }
    // =====================================================================
    public void MoveObject(string _side){
        
        // -----------------------------------------------------------------
        if(_side == "L"){

            // -----------------------------------------------
            // Prevent to go ofside LEFT
            for(int _Y=0; _Y< 5; _Y++){
                for(int _X=0; _X< 10; _X++){
                    if(ObjectMatrix[_Y, _X, 0] <= 1 && ObjectMatrix[_Y, _X, 0] != 0) return; 
                }
            }
            // -----------------------------------------------
            for(int _Y=0; _Y< 5; _Y++){
                for(int _X=0; _X< 10; _X++){

                    if(ObjectMatrix[_Y, _X, 0] != 0) ObjectMatrix[_Y, _X, 0]--; 
                    //if(ObjectMatrix[_Y, _X, 1] != 0) ObjectMatrix[_Y, _X, 1]--; 
                }
            }
            // -----------------------------------------------
        }
        // -----------------------------------------------------------------
        else{
            // -----------------------------------------------
             for(int _Y=0; _Y< 5; _Y++){
                for(int _X=0; _X< 10; _X++){
                    if(ObjectMatrix[_Y, _X, 0] >= fieldSizeX-2 && ObjectMatrix[_Y, _X, 0] != 0) return; 
                }
            }
            // -----------------------------------------------
            for(int _Y=0; _Y< 5; _Y++){
                for(int _X=0; _X< 10; _X++){

                    if(ObjectMatrix[_Y, _X, 0] != 0) ObjectMatrix[_Y, _X, 0]++; 
                    //if(ObjectMatrix[_Y, _X, 1] != 0) ObjectMatrix[_Y, _X, 1]++; 
                }
            }
            // -----------------------------------------------
        }        
        // -----------------------------------------------------------------

    }

    // =====================================================================
    public void RotateObject(){
        
        // -----------------------------------------------------------------
        // -----------------------------------------------------------------

    }
    // =====================================================================
    public void CreateSolidRow(){
        
        // -----------------------------------------------------------------
        for(int i=0; i < fieldSizeX; i++){ SOLID_ROW += SOLID_CHR; }
        // -----------------------------------------------------------------

    }
    // =====================================================================
    public void SetCenterOfTerminal(){
        
        // -----------------------------------------------------------------
        int mawWidth = Console.WindowWidth;
        int mawHeight = Console.WindowHeight;
        // -----------------------------------------------------------------
        int offset_X = (mawWidth/2) - (fieldSizeX/2);

        for(int i=0; i<offset_X; i++){ 
            FIELD_OFFSET += " "; 
        }
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