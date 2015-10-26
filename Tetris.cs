using System;

// :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

public class TetrisClass{

    // =====================================================================
    // User
    public int[ , ] FIELD_MATRIX;

    public int fieldSizeX;
    public int fieldSizeY;

    //----------------------------------------
    // int[] fieldSize = {40, 36}; // 38_x 34_y
    public string ROW_CHR               = "=";
    public string COL_SHR               = "|";
    public string FIELD_OFFSET          = "";
    public string EMPTY_CHR             = " ";
    public string DINAMYC_OBJECT_CHR    = "$";
    public string STATIC_OBJECT_CHR     = "#";
    public string NEW_ROW               = "";
    public string DRAWER                = "";

    public bool drawDinamycObj          = false;
    public bool drawStaticObj           = false;
    public bool createNewObject         = false;



    public string[] OBJECT_SIDES        = { "UP", "RIGTH", "DOWN", "LEFT" };
    public string[] OBJECT_TYPES        = { "T", "Z", "S", "I", "L", "J", "CUBE" };

    public int OBJECT_MATRIX_H          = 4;
    public int OBJECT_MATRIX_W          = 4;
    //----------------------------------------
    // OPTIONS
    public bool PAUSED                  = false;
    public bool STOP_GAME               = false;
    public int worldUpdateSpeed         = 1000/8;
    public int[ , , ] ObjMatrix; 
    public ObjectsCreater _Object;
    //----------------------------------------
    private float SPEED_DEVIDER_C       = 0.0F; //0.0F;

    private float SPEED_STEP            = 1.0F;
                  //SPEED_STEP            = 0.5F;
                  //SPEED_STEP            = 1.0F;

    private float SPEED_GOAL            = 1.0F; 

    private int SIDE_STEP               = 1;

    //private int SPEED_CURRENT           = 1;
    //----------------------------------------
    // Dev-Tools
    string MESSAGE                      = "";
    //----------------------------------------

    // =====================================================================
    public TetrisClass(int[] fieldSize){

        // -----------------------------------------------------------------
        fieldSizeX                              = fieldSize[0];
        fieldSizeY                              = fieldSize[1];
        FIELD_MATRIX                            = new int[ fieldSizeX , fieldSizeY ];
        _Object                                 = new ObjectsCreater(); 
        // -----------------------------------------------------------------
        NewObjectMatrix();
        // -----------------------------------------------------------------

    }

    // =====================================================================
    public void NewObjectMatrix(){
        
        // -----------------------------------------------------------------
        string _SIDE = OBJECT_SIDES[_GetRandom(0, 3)];
        string _TYPE = OBJECT_TYPES[_GetRandom(0, 7)];

        ObjMatrix = _Object.GetMatrixObject(_TYPE, _SIDE);
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
        if (SPEED_DEVIDER_C >= SPEED_GOAL){
        
            SPEED_DEVIDER_C = 0.0F;

            for(int _Y=0; _Y< OBJECT_MATRIX_H; _Y++){
                for(int _X=0; _X< OBJECT_MATRIX_W; _X++){
                    if(ObjMatrix[_Y, _X, 1] != -1) ObjMatrix[_Y, _X, 1]++;
                }
            }    
        
        }else{ SPEED_DEVIDER_C += SPEED_STEP; }
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
        //Console.WriteLine(FIELD_OFFSET);
        Console.WriteLine(FIELD_OFFSET);
        Console.WriteLine(FIELD_OFFSET + COL_SHR + NEW_ROW + COL_SHR);
        // -----------------------------------------------------------------
        // FIELD MATRIX
        for(int Y=0; Y < fieldSizeY; Y++){
            DRAWER = "";
            for(int X=0; X < fieldSizeX; X++){

                // ---------------------------------------------------------
                if(FIELD_MATRIX[X, Y] == 1) drawStaticObj = true; //break;
                // ---------------------------------------------------------
                // OBJECT MATRIX
                for(int _Y=0; _Y < OBJECT_MATRIX_H; _Y++){
                    if( !drawDinamycObj ){
                        for(int _X=0; _X < OBJECT_MATRIX_W; _X++){
                            // -----------------------------------------------
                            if(ObjMatrix[_Y,_X, 0] != -1) MESSAGE = (ObjMatrix[_Y,_X, 0])+" : "+(ObjMatrix[_Y,_X, 1]);
                            // ---------------------------------------------
                            if( ObjMatrix[_Y, _X, 1] >= fieldSizeY ){ createNewObject = true; }
                            // ---------------------------------------------
                            //if( (FIELD_MATRIX[ObjMatrix[_X,_Y, 0], ObjMatrix[_Y,_Y, 1]] == 1) ){ 
                            if(ObjMatrix[_Y,_X, 0] != -1 && ObjMatrix[_Y,_X, 1] != -1){
                                try{
                                    if( FIELD_MATRIX[ ObjMatrix[_Y,_X, 0] , ObjMatrix[_Y,_X, 1] ] == 1 ){ 
                                        //MESSAGE = ""+ ObjMatrix[_Y,_X, 0]+":"+ObjMatrix[_Y,_X, 1];
                                        createNewObject = true; 
                                    }
                                }catch(Exception ex){}
                            }
                            // -----------------------------------------------
                            if(ObjMatrix[_Y, _X, 0] == X && ObjMatrix[_Y, _X, 1] == Y){
                                drawDinamycObj = true; break;
                            }
                            // ---------------------------------------------
                        } // for 4
                    }else{ break; } // if(!found)
                } // for 3
                // ---------------------------------------------------------
            
                if(drawDinamycObj){

                    DRAWER += DINAMYC_OBJECT_CHR;
                    drawDinamycObj = false;

                }else if(drawStaticObj){

                    DRAWER += STATIC_OBJECT_CHR;
                    drawStaticObj = false;

                }else{
                    DRAWER += EMPTY_CHR;
                }
                // ---------------------------------------------------------
                // _STATIC_
                try{

                    if(FIELD_MATRIX[ (fieldSizeX - X) , ( fieldSizeY - Y+1) ] == 1){

                        //MESSAGE = "COLLIDET - 2";
                        //NewObjectMatrix("Z");

                    }

                }catch(Exception ex){}
                // ---------------------------------------------------------
                /*
                try{
                    if(fieldSizeX){}
                }catch(Exception exp){}
                */
                // ---------------------------------------------------------
            } // for 2
            Console.WriteLine(FIELD_OFFSET + COL_SHR + DRAWER + COL_SHR);
        } // for 1
        // -----------------------------------------------------------------

        // -----------------------------------------------------------------
        Console.WriteLine(FIELD_OFFSET + COL_SHR + NEW_ROW + COL_SHR);
        // -----------------------------------------------------------------
        Console.WriteLine(FIELD_OFFSET + " " + MESSAGE );
        /*
        FIELD_MATRIX[10, 10] = 8;
        Console.WriteLine(FIELD_OFFSET + " " + FIELD_MATRIX[10, 10].ToString() );
        Console.WriteLine(FIELD_OFFSET + " " + " PposX_L: " + (platformPosX-9).ToString() + " PposX_LR: " + ( platformPosX - posXCorrection + PLATFORT_CHR.Length ).ToString());
        Console.WriteLine(FIELD_OFFSET + " " + " P: " + platformPosX.ToString() + " | B: "+ ballPosX.ToString());
        Console.WriteLine(FIELD_OFFSET + " " + MESSAGE_PAUSE );
        Console.WriteLine(FIELD_OFFSET + " " + NEW_ROW );
        */
        // -----------------------------------------------------------------
        Console.WriteLine(FIELD_OFFSET + COL_SHR + NEW_ROW + COL_SHR);
        // -----------------------------------------------------------------
        //return 0;
        if(createNewObject){

            // ------------------------------------------------------
            for(int y=0; y < OBJECT_MATRIX_H; y++){
                for(int x=0; x < OBJECT_MATRIX_W; x++){

                    if(ObjMatrix[y, x, 0] != -1 && ObjMatrix[y, x, 1] != -1 ){
                        FIELD_MATRIX[ (ObjMatrix[y,x, 0]), (ObjMatrix[y,x, 1])-1 ] = 1;

                    }
                }
            }
            // ------------------------------------------------------
            // Remove full filled line if exists

            // FIELD MATRIX
            for(int X=0; X < fieldSizeX; X++){
                
                int sumator = 0;

                for(int Y=0; Y < fieldSizeY; Y++){
            
                    sumator += FIELD_MATRIX[X, Y];

                }
                if(sumator >= fieldSizeX){
                    // Delet this row
                    for(int Y=0; Y < fieldSizeY; Y++){

                        FIELD_MATRIX[X, Y] = 0;

                    }
                }
            }


            // ------------------------------------------------------
            createNewObject = false;
            NewObjectMatrix();
        }
        // -----------------------------------------------------------------
    
    }
    // =====================================================================
    public void MoveObject(string _side){
        
        // -----------------------------------------------------------------
        if(_side == "L"){

            // -----------------------------------------------
            // Prevent to go ofside LEFT
            for(int _Y=0; _Y < OBJECT_MATRIX_H; _Y++){
                for(int _X=0; _X < OBJECT_MATRIX_W; _X++){
                    if(ObjMatrix[_Y, _X, 0] <= 1 && ObjMatrix[_Y, _X, 0] != -1) return; 
                }
            }
            // -----------------------------------------------
            for(int _Y=0; _Y < OBJECT_MATRIX_H; _Y++){
                for(int _X=0; _X < OBJECT_MATRIX_W; _X++){

                    if(ObjMatrix[_Y, _X, 0] != -1) ObjMatrix[_Y, _X, 0] -= SIDE_STEP; 
                }
            }
            // -----------------------------------------------
        }
        // -----------------------------------------------------------------
        else{
            // -----------------------------------------------
            for(int _Y=0; _Y < OBJECT_MATRIX_H; _Y++){
                for(int _X=0; _X < OBJECT_MATRIX_W; _X++){
                    if(ObjMatrix[_Y, _X, 0] >= fieldSizeX-1 && ObjMatrix[_Y, _X, 0] != -1) return; 
                }
            }
            // -----------------------------------------------
            for(int _Y=0; _Y < OBJECT_MATRIX_H; _Y++){
                for(int _X=0; _X < OBJECT_MATRIX_W; _X++){

                    if(ObjMatrix[_Y, _X, 0] != -2) ObjMatrix[_Y, _X, 0] += SIDE_STEP;
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
        for(int i=0; i < fieldSizeX; i++){ NEW_ROW += ROW_CHR; }
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