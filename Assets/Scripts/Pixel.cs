using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel
{
	public int x;
	public int y;
	public int neighboursAlive;
	public Color color;

	public Pixel(int x, int y, Color color)
	{
		this.x = x;
		this.y = y;
		this.color = color;
	}
}