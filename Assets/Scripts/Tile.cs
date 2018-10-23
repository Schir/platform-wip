using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	//tileType can have n states.
	//0 = null, 1 = grass, 2 = water, 3 = wall
	public int tileType;
	public int x;
	public int y;

	void setX(int n)
	{
		this.x = n;
	}
	int getX()
	{
		return this.x;
	}
	void setY(int n)
	{
		this.y = y;
	}
	int getY()
	{
		return this.y;
	}
	void SetTileType(int n)
	{
		this.tileType = n;
	}
	int getTileType()
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
