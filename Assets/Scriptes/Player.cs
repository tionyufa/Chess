using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private Game GameContor;

    [Header("GameSetting")] 
    private bool isDiagonalStepGame;
    private bool isVecticalStepGame;
    private bool AllStepGame;
    
    [Header("Other")]
    public int ID;
    private int xBoard = -1;
    private int yBoard = -1;
    private bool isStep;
    public bool isCheck;
    private int indexPlates ;
    public SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        GameContor = Game.Singleton;
        if (PlayerPrefs.HasKey("SettingGame"))
        {
            if (PlayerPrefs.GetInt("SettingGame") == 0)
            {
                AllStepGame = true;
            }
            if (PlayerPrefs.GetInt("SettingGame") == 1)
            {
                isVecticalStepGame = true;
            }
            if (PlayerPrefs.GetInt("SettingGame") == 2)
            {
                isDiagonalStepGame = true;
            }
        }
    }
    
    public void setGambit (bool _gambit)
    {
        isStep = _gambit;
    }

    public void SetCoords( )
    {
        float x = xBoard;
        float y = yBoard;
        x *= 1;
        y *= 1;
       
        this.transform.position = new Vector3(x,y,-1.0f);
    }
    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (isStep)
        {
            SpawnPlate();
            GameContor.colorWhite();
            sr.color = Color.green;
        }
       
       
    }
    private void spawnPlate(int x, int y)
    {
        GameContor.Plates[indexPlates].setTranform(x,y);
        GameContor.Plates[indexPlates].gameObject.SetActive(true);
        GameContor.Plates[indexPlates].Cell(this);
        indexPlates++;
        if (indexPlates >= GameContor.Plates.Count) indexPlates = 0;

    }

   
    private void SpawnPlate()
    {
        for (int i = 0; i < GameContor.Plates.Count; i++)
        {
            GameContor.Plates[i].gameObject.SetActive(false);
        }
        
        upDown(0,1);
        rightLeft(1,0);
        Diagonal_x(1,1);
        Diagonal_y(-1,1);
        
    }

    private void upDown(int x,int y)
    {
        
        for (int i = 0; i < 2; i++)  // проверка на пустоту сверху и внизу
        {
            if (GameContor.PositionOnBoard(xBoard , yBoard + y) 
                && GameContor.GetPosition(xBoard , yBoard + y) == null)
            {
                if (AllStepGame || isVecticalStepGame)  //проверка какая версия игры
                {
                    spawnPlate(xBoard  , yBoard + y);
                }
               
            }
            
            else if (GameContor.PositionOnBoard(xBoard + x, yBoard + y) &&
                     GameContor.GetPosition(xBoard + x, yBoard + y) != null && isVecticalStepGame)
            {
                if (GameContor.PositionOnBoard(xBoard + x * 2, yBoard + y * 2) &&
                    GameContor.GetPosition(xBoard + x * 2, yBoard + y * 2) == null )
                {
                     spawnPlate(xBoard + x * 2,yBoard + y * 2);
                }
                
            }

            y = -1;
        }
    }

    private void rightLeft(int x, int y)
    {
        for (int i = 0; i < 2; i++) // проверка на пустоту в  боках
        {
            if (GameContor.PositionOnBoard(xBoard + x , yBoard ) 
                && GameContor.GetPosition(xBoard  + x, yBoard ) == null)
            {
                if (AllStepGame || isVecticalStepGame)  //проверка какая версия игры
                {
                    spawnPlate(xBoard + x , yBoard);
                }
            }
            
            else if (GameContor.PositionOnBoard(xBoard + x, yBoard + y) &&
                     GameContor.GetPosition(xBoard + x, yBoard + y) != null && isVecticalStepGame)
            {
                if (GameContor.PositionOnBoard(xBoard + x * 2, yBoard + y * 2) &&
                    GameContor.GetPosition(xBoard + x * 2, yBoard + y * 2) == null )
                {
                    spawnPlate(xBoard + x * 2,yBoard + y * 2);
                }
                
            }

            x = -1;
        }
    }

    private void Diagonal_x (int x, int y)
    {
        for (int i = 0; i < 2; i++) // проверка на пустоту в  по диагонали
        {
            if (GameContor.PositionOnBoard(xBoard + x , yBoard + y) 
            && GameContor.GetPosition(xBoard  + x, yBoard + y) == null)
            {
                if (AllStepGame || isDiagonalStepGame) //проверка какая версия игры
                {
                    spawnPlate(xBoard + x, yBoard + y);
                }
            }
            
            else if (GameContor.PositionOnBoard(xBoard + x, yBoard + y) &&
                     GameContor.GetPosition(xBoard + x, yBoard + y) != null && isDiagonalStepGame)
            {
                if (GameContor.PositionOnBoard(xBoard + x * 2, yBoard + y * 2) &&
                    GameContor.GetPosition(xBoard + x * 2, yBoard + y * 2) == null )
                {
                    spawnPlate(xBoard + x * 2,yBoard + y * 2);
                }
                
            }
        
            y = -1;
        
        }
    }

    private void Diagonal_y(int x, int y)
    {
        for (int i = 0; i < 2; i++) // проверка на пустоту в  по диагонали
        {
            if (GameContor.PositionOnBoard(xBoard + x , yBoard + y) 
                && GameContor.GetPosition(xBoard  + x, yBoard + y) == null)
            {
                if (AllStepGame || isDiagonalStepGame) //проверка какая версия игры
                {
                    spawnPlate(xBoard + x, yBoard + y);
                }
            }
            
            else if (GameContor.PositionOnBoard(xBoard + x, yBoard + y) &&
                     GameContor.GetPosition(xBoard + x, yBoard + y) != null && isDiagonalStepGame) 
            {
                if (GameContor.PositionOnBoard(xBoard + x * 2, yBoard + y * 2) &&
                    GameContor.GetPosition(xBoard + x * 2, yBoard + y * 2) == null )
                {
                    spawnPlate(xBoard + x * 2,yBoard + y * 2);
                }
                
            }
            y = -1;
        
        }
    }
     
  
   
}
