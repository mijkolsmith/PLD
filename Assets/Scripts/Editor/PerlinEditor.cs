using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PerlinNoise))]
public class PerlinEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PerlinNoise myScript = (PerlinNoise)target;

		if (GUILayout.Button("Generate Texture"))
		{
			myScript.offsetX = Random.Range(-99999f, 99999f);
			myScript.offsetY = Random.Range(-99999f, 99999f);
			myScript.GetComponent<Renderer>().material.mainTexture = myScript.GenerateTexture();
		}

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Steps: ", GUILayout.Width(224));
		myScript.steps = EditorGUILayout.IntField(myScript.steps);
		EditorGUILayout.EndHorizontal();
		if (GUILayout.Button("Apply Cellular Automata"))
		{
			myScript.ApplyCellularAutomata();
		}

		if (GUILayout.Button("Generate Terrain from Noise"))
		{
			myScript.GenerateTerrain();
		}

		if (GUILayout.Button("Generate Lava"))
		{
			myScript.GenerateLava();
		}

		if (GUILayout.Button("Refresh Terrain (all steps above)"))
		{
			myScript.DeleteTerrain();

			myScript.offsetX = Random.Range(-99999f, 99999f);
			myScript.offsetY = Random.Range(-99999f, 99999f);
			myScript.GetComponent<Renderer>().material.mainTexture = myScript.GenerateTexture();

			myScript.ApplyCellularAutomata();

			myScript.GenerateTerrain();

			myScript.GenerateLava();
		}

		if (GUILayout.Button("Delete Terrain"))
		{
			myScript.DeleteTerrain();
		}
	}
}
