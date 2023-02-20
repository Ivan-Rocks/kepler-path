using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadCreator : MonoBehaviour
{
	[SerializeField]
	private Color color; 

	private Material material;

	private void Start()
	{
		MeshRenderer mesh_renderer = gameObject.AddComponent<MeshRenderer>(); 
		MeshFilter mesh_filter = gameObject.AddComponent<MeshFilter>(); 

		material = new Material(Shader.Find("Unlit/Color"));
		mesh_renderer.material = material;

		CreateQuadMesh(mesh_filter);
	}

	private void CreateQuadMesh(MeshFilter mesh_filter)
	{
		Vector3[] positions = new Vector3[4]; 		//4 vertices
		int[] triangles = new int[6]; 				//2 triangles

		float side_length = 5;

		//top left
		positions[0] = new Vector3(-side_length * 0.5f, side_length * 0.5f, 0);
		//top right
		positions[1] = new Vector3(side_length * 0.5f, side_length * 0.5f, 0); 
		//bottom right
		positions[2] = new Vector3(side_length * 0.5f, -side_length * 0.5f, 0); 
		//bottom left
		positions[3] = new Vector3(-side_length * 0.5f, -side_length * 0.5f, 0);

		triangles[0] = 0; 
		triangles[1] = 1;
		triangles[2] = 2; 

		triangles[3] = 2;
		triangles[4] = 3;
		triangles[5] = 0;

		Mesh mesh = new Mesh();
		mesh.Clear(); 
		mesh.vertices = positions; 
		mesh.triangles = triangles;

		mesh_filter.mesh = mesh;
	}

	private void Update()
	{
		material.color = color;
	}
}
