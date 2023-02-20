using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{

	[SerializeField]
	private float width = 2; 		//x direction

	[SerializeField]
	private float height = 2; 		// z direction


	[SerializeField]
	private int xdivisons = 1; 

	[SerializeField]
	private int zdivisons = 1;

	[SerializeField]
	private Color color = Color.white; 
	[SerializeField]
	private Color crest_color = Color.yellow;
	[SerializeField]
	private Color trough_color = Color.green;

	[SerializeField]
	private float MaxVertexHeight = 1.0f;

	[SerializeField]
	private int NumCycles = 1;


	private Material material; 
	private MeshFilter mesh_filter;
	private Mesh mesh;

	private Vector3[] positions; 
	private int[] triangles;
	private Color[] colors;

	private Vector3 offset_vector;
	private float dwidth; 
	private float dheight;

	private int previous_xdivisons; 
	private int previous_zdivisons;


	private void Start()
	{

		MeshRenderer mesh_renderer = gameObject.AddComponent<MeshRenderer>(); 
		mesh_filter = gameObject.AddComponent<MeshFilter>();
		material = new Material(Shader.Find("UI/Default")); 
		mesh_renderer.material = material; 

		CreateGridMesh(); 	//fills the mesh object
		UpdateGridMesh(); 

		previous_zdivisons = zdivisons; 
		previous_xdivisons = xdivisons;
	}

	void Update()
	{
		material.color = color; 

		if((previous_xdivisons != xdivisons) || (previous_zdivisons != zdivisons))
		{
			CreateGridMesh(); 
			previous_zdivisons = zdivisons; 
			previous_xdivisons = xdivisons;
		}
		UpdateGridMesh(Time.time);
	}

	void UpdateGridMesh(float offset = 0)
	{
		dwidth = width / (float)(xdivisons + 1);
		dheight = height / (float)(zdivisons + 1);
		offset_vector = new Vector3(width * 0.5f,0,  height * 0.5f);


		for(int i = 0; i < (zdivisons + 2); i++)
		{
			for(int j = 0; j < (xdivisons + 2); j++)
			{
				int index = (xdivisons + 2) * i + j; 
				float x_value = Mathf.Sin(2 * Mathf.PI * NumCycles * Mathf.InverseLerp(0, xdivisons + 1, j) + offset);
				float z_value = Mathf.Cos(2 * Mathf.PI * NumCycles * Mathf.InverseLerp(0, zdivisons + 1, i) + offset);
				positions[index] = Vector3.left * dwidth * j + Vector3.back * dheight * i + Vector3.up * (x_value * z_value) * MaxVertexHeight;
				positions[index] += offset_vector;
				colors[index] = Color.Lerp(trough_color, crest_color, Mathf.InverseLerp(-1, 1, (x_value + z_value)));
			}
		}

		for(int j = 0; j < (zdivisons + 1); j++)
		{
			for(int i = 0; i < (xdivisons + 1); i++)
			{
				triangles[(xdivisons + 1) * j * 6 + i * 6 + 2] = i + 0 + (xdivisons + 2) * j;
				triangles[(xdivisons + 1) * j * 6 + i * 6 + 1] = i + 1 + (xdivisons + 2) * j;
				triangles[(xdivisons + 1) * j * 6 + i * 6 + 0] = i + 1 + (xdivisons + 2) * (j + 1);
				triangles[(xdivisons + 1) * j * 6 + i * 6 + 5] = i + 1 + (xdivisons + 2) * (j + 1);
				triangles[(xdivisons + 1) * j * 6 + i * 6 + 4] = i + (xdivisons + 2) * (j + 1);
				triangles[(xdivisons + 1) * j * 6 + i * 6 + 3] = i + 0 + (xdivisons + 2) * j;
			}
		}

		mesh.Clear(); 
		mesh.vertices = positions;
		mesh.triangles = triangles;
		mesh.colors = colors;
		mesh.RecalculateNormals();
		mesh_filter.mesh = mesh;
	}


	void CreateGridMesh()
	{
		positions = new Vector3[(xdivisons + 2) * (zdivisons + 2)];
		triangles = new int[(xdivisons + 1) * (zdivisons + 1) * 6]; 
		colors = new Color[positions.Length];
		mesh = new Mesh();
	}

}
