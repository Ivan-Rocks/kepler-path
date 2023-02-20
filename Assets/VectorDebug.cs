using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class VectorUtility
{
	/*rhs and lhs must be normalized*/
	public static float GetAngle360(Vector3 lhs, Vector3 rhs)
	{
		float dot_result = Vector3.Dot(lhs, rhs);
		float angle = Mathf.Lerp(0, 360, Mathf.InverseLerp(1, -1, dot_result)); 
		return angle;
	}

	/*around_vector must be a normalized vector*/ 									  /*normalized*/
	public static Vector3 RotateAroundPerpendicular(this Vector3 this_vector, Vector3 around_vector, float rotation_angle)
	{	
		Vector3 rotated_vector = this_vector * Mathf.Cos(rotation_angle) + Vector3.Cross(this_vector, around_vector) * Mathf.Sin(rotation_angle); 
		return rotated_vector;
	}
}

public class VectorDebug
{
	public Vector3 head; 
	public Vector3 tail; 

	public bool showText = false;

 	private GameObject game_object;
 	private Transform transform;
 	private MeshFilter mesh_filter; 
 	private MeshRenderer mesh_renderer;
 	private Mesh mesh;
 	private Vector3[] position_buffer;
 	private int[] triangle_buffer;
 	private int height_subdivisons; 
 	private int vertical_subdivisons;
 	private float head_length;
 	private float tail_radius;
 	private float head_radius;
 	private int position_count; 
 	private int triangle_count;
 	private float d_angle;
 	private Text text;
 	private RectTransform text_transform;
 	public static Camera camera;

 	public void SetMaterial(Material mat)
 	{
 		mesh_renderer.material = mat;
 	}
 	public void SetFont(Font font)
 	{
 		text.font = font;
 	}

	public VectorDebug(Transform parent, int height_subdivisons = 0, int vertical_subdivisons = 10, float tail_radius = 1f, float head_radius = 2f, float head_length = 0.5f)
	{
		camera = Camera.main;
		this.head_radius = head_radius; 
		this.tail_radius = tail_radius; 
		this.head_length = head_length; 
		this.height_subdivisons = height_subdivisons; 
		this.vertical_subdivisons = vertical_subdivisons;
		d_angle = ((float)360) / vertical_subdivisons;
		create_objects(parent);
		create_mesh();
	}

	public void Update()
	{
		update_mesh();
	}
	public void SetColor(Color color)
	{
		mesh_renderer.material.color = color;
	}
	private void create_objects(Transform parent)
	{
		game_object = new GameObject("Vector"); 
		transform = game_object.transform;
		mesh_filter = game_object.AddComponent<MeshFilter>(); 
		mesh_renderer = game_object.AddComponent<MeshRenderer>(); 
		mesh_renderer.material = new Material(Shader.Find("Standard")); 
		mesh_renderer.material.color = Color.white;
		create_mesh();

		transform.SetParent(parent);

		GameObject text_obj = new GameObject("Text"); 
		text = text_obj.AddComponent<Text>();
		text_transform = text_obj.GetComponent<RectTransform>();
		text_transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
		text.fontSize = 25;
		//text.fontStyle = FontStyle.Bold;
		text_transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
		text_transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
	}
	private void create_mesh()
	{
		mesh = new Mesh();
		position_count = 2 + vertical_subdivisons * (height_subdivisons + 3);
		triangle_count = 2 * vertical_subdivisons * 3 + 6 * vertical_subdivisons * (height_subdivisons + 2);
		position_buffer = new Vector3[position_count]; 
		triangle_buffer = new int[triangle_count]; 
		create_triangles();
	}
	private void create_triangles()
	{
		int counter = 0;
		for(int i = 0; i < vertical_subdivisons; i++)
		{
			triangle_buffer[counter + 2] = position_count - 2;			//tail
			triangle_buffer[counter + 1] = i;
			triangle_buffer[counter + 0] = (i + 1) % vertical_subdivisons;
			counter	+= 3;
		}

		for(int i = 0; i < (height_subdivisons + 2); i++)
		{
			for(int j = 0; j < vertical_subdivisons; j++)
			{
				triangle_buffer[counter + 2] = vertical_subdivisons * i + j;
				triangle_buffer[counter + 1] = vertical_subdivisons * (i + 1) + j;
				triangle_buffer[counter + 0] = vertical_subdivisons * (i + 1) + (j + 1) % vertical_subdivisons;

				triangle_buffer[counter + 5] = vertical_subdivisons * (i + 1) + (j + 1) % vertical_subdivisons;
				triangle_buffer[counter + 4] = vertical_subdivisons * i +  (j + 1) % vertical_subdivisons;
				triangle_buffer[counter + 3] = vertical_subdivisons * i + j;
				counter	 += 6;
			}
		}

		for(int i = 0; i < vertical_subdivisons; i++)
		{
			triangle_buffer[counter + 0] = position_count - 1;
			triangle_buffer[counter + 1] = (height_subdivisons + 2) * vertical_subdivisons + i;
			triangle_buffer[counter + 2] = (height_subdivisons + 2) * vertical_subdivisons + (i + 1) % vertical_subdivisons;
			counter += 3;
		}
	}

	private void update_mesh()
	{
		if(showText)
		{
			text_transform.position = camera.WorldToScreenPoint(head);
			text.text = head.ToString();
		}
		//transform.position = tail;
		Vector3 dir = head - tail; 
		float tail_length = dir.magnitude - head_length;
		dir.Normalize();
		float d_length = tail_length / (height_subdivisons + 1); 
		Vector3 perp_vector;
		if((Mathf.Abs(dir.y) == 1) && (dir.x == 0) && (dir.z == 0))
			perp_vector = Vector3.right;
		else
	    	perp_vector = Vector3.Cross(Vector3.Cross(dir, Vector3.up), dir).normalized; 
		Vector3 tail_perp_vector = perp_vector * tail_radius;
		Vector3 head_perp_vector = perp_vector * head_radius;

		position_buffer[position_count - 2] = tail; 
		position_buffer[position_count - 1] = head;

		int counter = 0;
		float length = 0;
		for(int i = 0; i < (height_subdivisons + 2); i++)
		{
			for(int j = 0; j < vertical_subdivisons; j++)
			{
				position_buffer[counter + j] = tail_perp_vector + dir * length + tail;
				//tail_perp_vector = tail_perp_vector.RotateAroundPerpendicular(dir, d_angle);
				tail_perp_vector = Quaternion.AngleAxis(d_angle, dir) * tail_perp_vector;
			}
			length += d_length;
			counter += vertical_subdivisons;
		}
		int tail_position_count = position_count - 2 - vertical_subdivisons;
		length -= d_length;
		for(int i = 0; i < vertical_subdivisons; i++)
		{
			position_buffer[tail_position_count + i] = head_perp_vector + dir * length + tail;
			head_perp_vector = Quaternion.AngleAxis(d_angle, dir) * head_perp_vector;
		}
		mesh.Clear(); 
		mesh.vertices = position_buffer; 
		mesh.triangles = triangle_buffer;
		mesh.RecalculateNormals();
		mesh_filter.mesh = mesh;
	}
}
