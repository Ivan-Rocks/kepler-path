using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCreator : MonoBehaviour
{

	[SerializeField]
	private Color color; 

	Material material;
	private void Start()
	{
		MeshRenderer mesh_renderer = gameObject.AddComponent<MeshRenderer>(); 
		MeshFilter mesh_filter = gameObject.AddComponent<MeshFilter>();

		material = new Material(Shader.Find("Unlit/Color")); 
		mesh_renderer.material = material;
		CreateTriangleMesh(mesh_filter);
	}

	private void CreateTriangleMesh(MeshFilter mesh_filter)
	{
		Vector3[] positions = new Vector3[3]; 
		int[] triangles = new int[3]; 

		float radius = 2;
		positions[0] = Vector3.up * radius; 
		positions[1] = (new Vector3(-1, -1, 0)) * radius; 
		positions[2] = (new Vector3(1, -1, 0)) * radius; 

		//clockwise sense
		triangles[0] = 2; 
		triangles[1] = 1;
		triangles[2] = 0;

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
