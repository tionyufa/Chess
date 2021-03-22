using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plate : MonoBehaviour 
{
    public Game controller;
    
    private GameObject reference = null;
    private int xMatrix;
    private int yMatrix;
    private Player _player;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseUp()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        controller.SetPositionEmpty(_player.GetXBoard(),_player.GetYBoard());
        _player.SetXBoard(xMatrix);
        _player.SetYBoard(yMatrix);
        _player.SetCoords();
        controller.Gambit();
        controller.SetPosition(_player);
        controller.colorWhite();
        controller.ClearPlate();
        SpriteRenderer.color = Color.black;
        
    }

    public void setTranform(int x , int y )
    {
        xMatrix = x;
        yMatrix = y;
        transform.position = new Vector3(x,y,0);
        
    }

    public void Cell(Player player)
    {
        _player = player;
    }

    private void OnMouseEnter()
    {
        SpriteRenderer.color = Color.red;
    }
    private void OnMouseExit()
    {
        SpriteRenderer.color = Color.black;
    }
}
