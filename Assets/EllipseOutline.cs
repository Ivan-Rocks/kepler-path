using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseOutline : MonoBehaviour
{
	[SerializeField]
	private float a = 6; 
	[SerializeField]
	private float b = 4;

	[SerializeField]
	private Font font;


	[SerializeField]
	private int divisons = 50;

	private LineRenderer line_renderer; 
	private Material material;

	private Vector3[] positions;
	private float dangle;

	private float previous_a;
	private float prevoius_b;
	private float previous_divisons;


	public float angular_speed = 1.0f; //1 rad per second

	private Circle circle;
	private VectorDebug vector_debug;
	private Vector3 vector_head;
	private float circle_radius = 0.3f;

	private VectorDebug up_vector; 
	private VectorDebug right_vector;
	private VectorDebug vertical_vector; 
	private VectorDebug horizontal_vector;

	private void Start()
	{
		line_renderer = gameObject.AddComponent<LineRenderer>();
		line_renderer.widthMultiplier = 0.1f;
		material = new Material(Shader.Find("Unlit/Color"));
		material.color = Color.white;
		line_renderer.material = material;

		update_divisons();
		draw_ellipse_outline();
		set_up_objects();
	}

	private void set_up_objects()
	{
		vector_head = Vector3.right * a;
		circle = new Circle();
		circle.SetDivisons(40);
		circle.SetRadius(circle_radius);
		vector_debug = new VectorDebug(transform, 0, 10, 0.05f, 0.1f, 0.3f);
		vector_debug.SetMaterial(material);
		vector_debug.SetColor(Color.blue);
		vector_debug.tail = Vector3.zero;

		up_vector = new VectorDebug(transform, 0, 10, 0.05f, 0.1f, 0.3f); 
		up_vector.SetMaterial(material); 
		up_vector.SetColor(Color.green); 
		up_vector.tail = Vector3.zero; 

		right_vector = new VectorDebug(transform, 0, 10, 0.05f, 0.1f, 0.3f); 
		right_vector.SetMaterial(material); 
		right_vector.SetColor(Color.red); 
		right_vector.tail = Vector3.zero; 

		vertical_vector = new VectorDebug(transform, 0, 10, 0.05f, 0.1f, 0.3f); 
		vertical_vector.SetMaterial(material); 
		vertical_vector.SetColor(Color.white); 
		vertical_vector.tail = Vector3.right * a;

		horizontal_vector = new VectorDebug(transform, 0, 10, 0.05f, 0.1f, 0.3f); 
		horizontal_vector.SetMaterial(material); 
		horizontal_vector.SetColor(Color.white); 
		horizontal_vector.tail = Vector3.zero; 

		vector_debug.showText = true;
		up_vector.showText = true;
		right_vector.showText = true;
		vertical_vector.showText = false;
		horizontal_vector.showText = false;

		vector_debug.SetFont(font);
		up_vector.SetFont(font);
		right_vector.SetFont(font);
	}

	private void Update()
	{
		if(previous_divisons != divisons)
			update_divisons();
		if((previous_a != a) || (prevoius_b != b))
			draw_ellipse_outline();

		DoAnimation();
	}

	private float angle;
	private void DoAnimation()
	{
		angle += Time.deltaTime * angular_speed;
		if(angle > (2 * Mathf.PI))
			angle = 0;
		vector_head.x = a * Mathf.Cos(angle); 
		vector_head.y = b * Mathf.Sin(angle);

		vector_debug.head = vector_head - vector_head.normalized * circle_radius * 0.5f;

		up_vector.head = Vector3.up * vector_head.y;
		right_vector.head = Vector3.right * vector_head.x;

		horizontal_vector.tail = up_vector.head;
		horizontal_vector.head = vector_head;

		vertical_vector.tail = right_vector.head;
		vertical_vector.head = vector_head;

		vector_debug.Update();
		up_vector.Update();
		right_vector.Update();

		vertical_vector.Update();
		horizontal_vector.Update();

		circle.UpdatePositions(vector_head);
	}

	private void update_divisons()
	{
		positions = new Vector3[divisons + 1];
		line_renderer.positionCount = divisons + 1;
		dangle = 2 * Mathf.PI / divisons;
		previous_divisons = divisons;
	}

	private void draw_ellipse_outline()
	{
		float angle = 0;
		Vector3 position = new Vector3();
		for(int i = 0; i < (divisons + 1); i++, angle += dangle)
		{
			position.x = a * Mathf.Cos( angle );
			position.y = b * Mathf.Sin( angle );
			positions[i] = position;
		}	
		line_renderer.SetPositions(positions);
		previous_a = a;
		prevoius_b = b;
	}

}
