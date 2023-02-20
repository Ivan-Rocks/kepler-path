using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{
	private GameObject gameObject;

	private float radius = 0.5f; 
	private int divisons = 10; 

	private float dangle;

	private Vector3[] positions; 
	private int[] triangles;

	private Vector3 center;

	private MeshFilter mesh_filter;
	private MeshRenderer mesh_renderer; 
	private Material material;
	private Mesh mesh;

	public Circle()
	{
		gameObject = new GameObject("Circle");
		SetUpComponents();
		SetDivisons(10);
		SetRadius(0.5f);
		UpdatePositions(Vector3.zero);
	}

	private void SetUpComponents()
	{
		mesh_filter = gameObject.AddComponent<MeshFilter>();
		mesh_renderer = gameObject.AddComponent<MeshRenderer>();
		material = new Material(Shader.Find("Unlit/Color"));
		material.color = Color.green;

		mesh_renderer.material = material;
		mesh = new Mesh();
		mesh.Clear();
		mesh_filter.mesh = mesh;
	}

	public void SetRadius(float radius) { this.radius = radius; UpdatePositions(center); } 
	public void SetDivisons(int divisons)
	{
	 	dangle = 2 * Mathf.PI / divisons; 
	 	positions = new Vector3[divisons + 1];
	 	triangles = new int[3 * divisons];

	 	for(int i = 0; i < divisons; i++)
		{
			triangles[i * 3 + 2] = divisons;
			triangles[i * 3 + 1] = i;
			triangles[i * 3 + 0] = (i + 1) % divisons;
		}
	 	UpdatePositions(center);

		mesh.triangles = triangles;
	 	this.divisons = divisons; 
	}

	public void UpdatePositions(Vector3 center)
	{
		float angle = 0;
		Vector3 _position = new Vector3();
		positions[positions.Length - 1] = center;
		for(int i = 0;  i < divisons; i++, angle += dangle)
		{
			_position.x = radius * Mathf.Cos(angle); 
			_position.y = radius * Mathf.Sin(angle);
			positions[i] = _position + center;
		}
		mesh.vertices = positions;
		mesh_filter.mesh = mesh;
		this.center = center;
	}

}
