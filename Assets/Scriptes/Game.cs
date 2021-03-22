using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public static Game Singleton;
    
    [Header("Player")]
    private Player[] playersBlack = new Player[9];
    private Player[] playersWhite = new Player[9];
    public List<Plate> Plates;
    [SerializeField] private Player whitePrefab;
    [SerializeField] private Player blackPrefab;
    private bool gambitStep = true;
    
    [Header("Game")]
    [SerializeField] private TextMeshProUGUI _text;
    private GameObject[,] positions = new GameObject[8, 8];
    private bool gameOver = false;
    private List<int> _startPositionsWhiteX = new List<int>();
    private List<int> _startPositionsWhiteY = new List<int>();
    private List<int> _startPositionsBlackX = new List<int>();
    private List<int> _startPositionsBlackY = new List<int>();
    private int WinBlack;
    private int WinWhite;
    [SerializeField] private Button Replay;
    
    void Awake()
    {
        Singleton = this;
        playersBlack = new[]
        {
            CreateBlack(0, 7), CreateBlack(1, 7), CreateBlack(2, 7), CreateBlack(0, 6), CreateBlack(1, 6),
            CreateBlack(2, 6), CreateBlack(0, 5),
            CreateBlack(1, 5), CreateBlack(2, 5)
        };
        playersWhite = new[]
        {
            CreateWhite(7, 0), CreateWhite(6, 0), CreateWhite(5, 0), CreateWhite(7, 1), CreateWhite(6, 1),
            CreateWhite(5, 1), CreateWhite(7, 2),
            CreateWhite(6, 2), CreateWhite(5, 2)
        };
        for (int i = 0; i < playersBlack.Length; i++)
        {
            SetPosition(playersBlack[i]);
            SetPosition(playersWhite[i]);
        }

        Gambit();
    }

    private Player CreateBlack(int x, int y)
    {
        var obj = Instantiate(blackPrefab, new Vector3(x, y, -1), Quaternion.identity);
        obj.SetXBoard(x);
        obj.SetYBoard(y);
        obj.SetCoords();
        obj.ID = 1;
        _startPositionsBlackX.Add(x);
        _startPositionsBlackY.Add(y);
        return obj;
    }

    private Player CreateWhite(int x, int y)
    {
        var obj = Instantiate(whitePrefab, new Vector3(0, 0, -1), Quaternion.identity);
        obj.SetXBoard(x);
        obj.SetYBoard(y);
        obj.SetCoords();
        obj.ID = 0;
        _startPositionsWhiteX.Add(x);
        _startPositionsWhiteY.Add(y);
       
        return obj;
    }

    public void SetPosition(Player player)
    {
        positions[player.GetXBoard(), player.GetYBoard()] = player.gameObject;

        for (int i = 0; i < _startPositionsBlackX.Count; i++)
        {
            if (player.GetXBoard() == _startPositionsBlackX[i] && player.GetYBoard() == _startPositionsBlackY[i] && player.ID == 0)
            {
                player.isCheck = true;
                CheckWiner();
                return;
            }
            else player.isCheck = false;
                
            
            
            if (player.GetXBoard() == _startPositionsWhiteX[i] && player.GetYBoard() == _startPositionsWhiteY[i] && player.ID == 1)
            {
                player.isCheck = true;
                CheckWiner();
                return;
            }
            else player.isCheck = false;
            
        }
      
       
    }

    private void CheckWiner()
    {
        WinBlack = 0;
        WinWhite = 0;
        for (int i = 0; i < playersBlack.Length; i++)
        {
            if (playersBlack[i].isCheck)
            {
                WinBlack++;
                if (WinBlack >= 9)
                {
                    Final("Поздравляем! Победили Черные!!!");
                }
            }
            if (playersWhite[i].isCheck)
            {
                WinWhite++;
                if (WinWhite >= 9)
                {
                   Final("Поздравляем! Победили Белые!!!");
                }
                
            }
        }
    }
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))  return false;
        
        return true;
    }

    public void ClearPlate()
    {
        for (int i = 0; i < Plates.Count; i++)
        {
            Plates[i].gameObject.SetActive(false);
        }
    }

    public void Gambit()
    { 
        gambitStep = !gambitStep;
        if (gambitStep) _text.text = String.Format("<color=#000000> {0} </color>","Ход черных");
        else _text.text = String.Format("<color=#FFFFFF> {0} </color>","Ход белых");
        
        for (int i = 0; i < playersBlack.Length; i++)
        {
            playersBlack[i].setGambit(gambitStep);
            playersWhite[i].setGambit(!gambitStep);
        }
    }

    public void Final(string str)
    {
        _text.text = String.Format("<color=#FFFFFF> {0} </color>",str);
        for (int i = 0; i < playersBlack.Length; i++)
        {
            playersBlack[i].setGambit(false);
            playersWhite[i].setGambit(false);
        }
        Replay.gameObject.SetActive(true);
    }

    public void colorWhite()
    {
        for (int i = 0; i < playersWhite.Length; i++)
        {
            playersBlack[i].sr.color = Color.white;
            playersWhite[i].sr.color = Color.white;
        }
    }

   
    
}
