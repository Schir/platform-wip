using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSS {
	//tileType can have n states.
	//0 = null, 1 = grass, 2 = water, 3 = wall
	public int tileType;
	private int x;
	private int y;

	public void setX(int n)
	{
		this.x = n;
	}
	public int getX()
	{
		return this.x;
	}
	public void setY(int n)
	{
		this.y = n;
	}
	public int getY()
	{
		return this.y;
	}
	public void SetTileType(int o)
	{
		this.tileType = o;
	}
	public int getTileType()
	{
		return this.tileType;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class TileMaker : MonoBehaviour {
	public TileSS[,] levelGrid = new TileSS[5,5];

	
	// Use this for initialization
	void Start () {
		for(int w = 0; w < levelGrid.GetLength(0); w++)
		{
			for(int h = 0; h < levelGrid.GetLength(1); h++)
			{
				TileSS basicTile = new TileSS();
				basicTile.SetTileType(w % 4);
				basicTile.setX(w);
				basicTile.setY(h);
				levelGrid.SetValue(basicTile, w, h);
			}
		}
		for(int w = 0; w < levelGrid.GetLength(0); w++)
		{
			for(int h = 0; h < levelGrid.GetLength(1); h++)
			{
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        		cube.transform.position = new Vector3(w, 0, h);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
