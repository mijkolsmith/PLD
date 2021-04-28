using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
	public Pixel[,] cells;

	private int GetNeighboursAliveCount(int x, int y)
	{
		//https://stackoverflow.com/questions/21925239/game-of-life-c-checking-neighbors
		int count = 0;
		for (int i = -1; i < 1; i++)
		{
			for (int j = -1; j < 1; j++)
			{
				if (!(i == 0 && j == 0) && inBounds(x + i, y + j) && cells[x + i, y + j].color.r > .5f)
					count++;
			}
		}
		return count;
	}

	private bool inBounds(int x, int y)
	{
		return (x >= 0 && x < cells.GetLength(0) && (y >= 0 && y < cells.GetLength(1)));
	}

	public void nextStep()
	{
		for (int i = 0; i < cells.GetLength(0); i++)
		{
			for (int j = 0; j < cells.GetLength(1); j++)
			{
				cells[i, j].neighboursAlive = GetNeighboursAliveCount(i, j);
			}
		}

		for (int i = 0; i < cells.GetLength(0); i++)
		{
			for (int j = 0; j < cells.GetLength(1); j++)
			{
				//rules based on https://generativelandscapes.wordpress.com/2019/04/11/cave-cellular-automaton-algorithm-12-3/
				if (cells[i, j].color.r > .5f && cells[i, j].neighboursAlive < 3)
				{
					cells[i, j].color.r = 0;
					cells[i, j].color.g = 0;
					cells[i, j].color.b = 0;
				}
				else if (cells[i, j].color.r < .5f && cells[i, j].neighboursAlive > 5)
				{
					cells[i, j].color.r = 1;
					cells[i, j].color.g = 1;
					cells[i, j].color.b = 1;
				}
			}
		}
	}
}