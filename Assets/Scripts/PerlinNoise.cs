using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
	public int width = 25;
	public int height = 5;
	public float scaleX = 20;
	public float scaleY = 20;
	public float offsetX;
	public float offsetY;
	[Range(1.00f, 1.20f)] public float powerAbove = 1.07f;
	[Range(0.00f, 1.00f)] public float modifierAbove = .45f;
	[Range(1.00f, 1.20f)] public float powerUnder = 1.05f;
	[Range(0.00f, 1.00f)] public float modifierUnder = .5f;

	[HideInInspector] public int steps = 5;
	[HideInInspector] public Renderer texRenderer;

	public GameObject pixelGO;
	public GameObject lavaGO;
	public GameObject pixelHolder;
	public GameObject lavaHolder;

	Grid grid = new Grid();
	List<GameObject> pixels = new List<GameObject>();
	List<GameObject> lavas = new List<GameObject>();

	private void Start()
    {
		texRenderer = GetComponent<Renderer>();
		offsetX = Random.Range(-99999f, 99999f);
		offsetY = Random.Range(-99999f, 99999f);
    }

	private void Update()
	{
		texRenderer.material.mainTexture = GenerateTexture();
	}

	public Texture2D GenerateTexture()
	{
		Texture2D tex = new Texture2D(width, height);

		grid.cells = new Pixel[width, height];
		int i = 0;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				float value = Mathf.PerlinNoise((float) x / width * scaleX + offsetX, (float) y / height * scaleY + offsetY);

				if (y <= height * .50f)
				{
					if (Mathf.Pow(powerUnder, height * .50f - y) > 1f)
						value *= modifierUnder * Mathf.Pow(powerUnder, height * .50f - y);
					else value *= modifierUnder;
				}
				else if (y > height * .50f)
				{
					if (Mathf.Pow(powerAbove, y - height * .50f) > 1f)
						value *= modifierAbove * Mathf.Pow(powerAbove, y - height * .50f);
					else value *= modifierAbove;
				}

				Color color = new Color(value, value, value);
				tex.SetPixel(x, y, color);
				grid.cells[x, y] = new Pixel(x, y, color);
				i++;
			}
		}

		tex.Apply();
		return tex;
	}

	public void GenerateLava()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (grid.cells[x, y].color.r < .5f && grid.cells[x, y].y < height * .3f)
				{
					lavas.Add(Instantiate(lavaGO, new Vector3(x - width * .5f, y + height * .5f, 0), Quaternion.identity, lavaHolder.transform));
				}
			}
		}
	}

	public void ApplyCellularAutomata()
	{
		for (int i = 0; i < steps; i++)
		{
			grid.nextStep();
		}
	}

	public void GenerateTerrain()
	{
		int i = 0;
		foreach(Pixel pixel in grid.cells)
		{
			if (pixel.color.r > .5f)
			{
				pixels.Add(Instantiate(pixelGO, new Vector3(pixel.x - width * .5f, pixel.y + height * .5f, 0), Quaternion.identity, pixelHolder.transform));
				pixels[i].GetComponent<SpriteRenderer>().color = new Color(pixel.color.r -.2f, pixel.color.g-.5f, pixel.color.b-.5f);
				i++;
			}
		}
	}

	public void DeleteTerrain()
	{
		foreach (GameObject pixel in pixels)
		{
			DestroyImmediate(pixel);
		}
		foreach (GameObject lava in lavas)
		{
			DestroyImmediate(lava);
		}
		lavas.Clear();
		pixels.Clear();
	}
}