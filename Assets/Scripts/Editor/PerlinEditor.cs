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
		EditorGUILayout.LabelField("Cellular Automata Steps: ", GUILayout.Width(224));
		myScript.steps = EditorGUILayout.IntField(myScript.steps);
		EditorGUILayout.EndHorizontal();
		if (GUILayout.Button("Apply Cellular Automata"))
		{
			Texture2D tex = (Texture2D) myScript.GetComponent<Renderer>().material.mainTexture;
			myScript.ApplyCellularAutomata(tex);
		}

		if (GUILayout.Button("Generate Terrain"))
		{
			myScript.GenerateTerrain();
		}

		if (GUILayout.Button("Generate Lava"))
		{
			myScript.GenerateLava();
		}

		if (GUILayout.Button("Delete Terrain"))
		{
			myScript.DeleteTerrain();
		}

		if (GUILayout.Button("Refresh (Execute all steps)"))
		{
			Texture2D tex = (Texture2D)myScript.GetComponent<Renderer>().sharedMaterial.mainTexture;

			myScript.DeleteTerrain();

			myScript.offsetX = Random.Range(-99999f, 99999f);
			myScript.offsetY = Random.Range(-99999f, 99999f);
			tex = myScript.GenerateTexture();

			myScript.ApplyCellularAutomata(tex);

			myScript.GenerateTerrain();

			myScript.GenerateLava();
		}
	}
}
