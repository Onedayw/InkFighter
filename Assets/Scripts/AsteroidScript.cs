﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AsteroidScript : MonoBehaviour {
	public Camera cam;

		//************
		//
		// Fields
		//  
		//************

	public Material trailMaterial;                  //the material of the trail.  Changing this during runtime will have no effect.
	private float lifeTime = 0.8f;                   //the amount of time in seconds that the trail lasts
	private float changeTime = 0.5f;                 //time point when the trail begins changing its width (if widthStart != widthEnd)
	private float widthStart = 1.0f;                 //the starting width of the trail
	private float widthEnd = 0.3f;                   //the ending width of the trail
	private float vertexDistanceMin = 0.1f;         //the minimum distance between the center positions
	private float patternDetectTime = 0.0f;
	private float patternDetectInterval = 2.0f;
	public Vector3 renderDirection = new Vector3(0, 0, -1); //the direction that the mesh of the trail will be rendered towards
	public bool colliderIsTrigger = true;           //determines if the collider is a trigger.  Changing this during runtime will have no effect.
	public bool colliderEnabled = true;             //determines if the collider is enabled.  Changing this during runtime will have no effect.
	public bool pausing = false;                     //determines if the trail is pausing, i.e. neither creating nor destroying vertices
	public GameObject player;

	private Transform trans;                        //transform of the object this script is attached to                    
	private Mesh mesh;                              
	private new PolygonCollider2D collider;
	private float inkRange = 20.0f;
	private bool flag = true;

	private LinkedList<Vector3> centerPositions;    //the previous positions of the object this script is attached to
	private LinkedList<Vertex> leftVertices;        //the left vertices derived from the center positions
	private LinkedList<Vertex> rightVertices;       //the right vertices derived from the center positions

	//************
	//
	// Public Methods
	//
	//************

	/// <summary>
	/// Changes the material of the trail during runtime.
	/// </summary>
	public void ChangeTrailMaterial(Material material) {
		trailMaterial = material;
		collider.GetComponent<Renderer>().material = material;
	}

	/// <summary>
	/// Changes if the collider is a trigger or not during runtime.
	/// </summary>
	public void ChangeColliderTrigger(bool isTrigger) {
		colliderIsTrigger = isTrigger;
		collider.isTrigger = isTrigger;
	}

	/// <summary>
	/// Changes if the collider is enabled or not during runtime.
	/// </summary>
	public void ChangeColliderEnabled(bool enabled) {
		colliderEnabled = enabled;
		collider.enabled = enabled;
	}

	//************
	//
	// Private Unity Methods
	//
	//************

	private void Awake() {
		//create an object and mesh for the trail
		player=GameObject.Find ("Player");
		cam = Camera.main;

		GameObject trail = new GameObject("Trail", new[] { typeof(MeshRenderer), typeof(MeshFilter), typeof(PolygonCollider2D) } );
		trail.tag = "Trail";
		mesh = trail.GetComponent<MeshFilter>().mesh = new Mesh();
		trail.GetComponent<Renderer>().material = trailMaterial;
		trail.GetComponent<Renderer>().sortingLayerName = "Player";
		trail.GetComponent<Renderer>().sortingOrder = 0;

		//get and set the polygon collider on this trail.
		collider = trail.GetComponent<PolygonCollider2D>();
		collider.isTrigger = colliderIsTrigger;
		collider.SetPath(0, null);

		//get the transform of the object this script is attatched to
		trans = base.transform;

		//set the first center position as the current position
		centerPositions = new LinkedList<Vector3>();
		centerPositions.AddFirst(trans.position);

		leftVertices = new LinkedList<Vertex>();
		rightVertices = new LinkedList<Vertex>();
	}

	private void Update() {
		
	}

	//************
	//
	// Private Methods
	//
	//************

	/// <summary>
	///     /// Adds new vertices if the object has moved more than 'vertexDistanceMin' from the most recent center position.
	/// If a pair of vertices has been added, this method returns true.
	/// </summary>
	private bool TryAddVertices() {
		bool vertsAdded = false;

		//check if the current position is far enough away (> 'vertexDistanceMin') from the most recent position where two vertices were added
		if ((centerPositions.First.Value - trans.position).sqrMagnitude > vertexDistanceMin * vertexDistanceMin && isInRange ()) {
			//calculate the normalized direction from the 1) most recent position of vertex creation to the 2) current position
			Vector3 dirToCurrentPos = (trans.position - centerPositions.First.Value).normalized; 

			//calculate the positions of the left and right vertices --> they are perpendicular to 'dirToCurrentPos' and 'renderDirection'
			Vector3 cross = Vector3.Cross(renderDirection, dirToCurrentPos);
			Vector3 leftPos = trans.position + (cross * -widthStart * 0.5f);
			Vector3 rightPos = trans.position + (cross * widthStart * 0.5f);

			//trace can be added, see if player has enough health to add it
			PlayerController control = player.GetComponent<PlayerController> ();
			vertsAdded = control.removeHealth (1);
			if (vertsAdded) {			
				//create two new vertices at the calculated positions
				leftVertices.AddFirst(new Vertex(leftPos, trans.position, (leftPos - trans.position).normalized) );
				rightVertices.AddFirst(new Vertex(rightPos, trans.position, (rightPos - trans.position).normalized) );

				//add the current position as the most recent center position
				centerPositions.AddFirst(trans.position);
			}

			//vertsAdded = true;

		}
		//if (vertsAdded) {

		//}
		return vertsAdded;
	}

	/// <summary>
	/// Removes any pair of vertices (left + right) that have been alive longer than the specified lifespan.
	/// If a pair of vertices have been removed, this method returns true.
	/// </summary>
	private bool TryRemoveVertices() {
		bool vertsRemoved = false;
		LinkedListNode<Vertex> leftVertNode = leftVertices.Last;

		//continue looking at the last left vertex 1) while one exists and 2) while the last left vertex is older than its lifeTime
		while (leftVertNode != null && leftVertNode.Value.TimeAlive > lifeTime) {
			//remove the left vertex from the collection
			leftVertices.RemoveLast();
			leftVertNode = leftVertices.Last;

			//remove its partnered right vertex from the collection since they were created at the same time.
			rightVertices.RemoveLast();

			//remove the center position that the two vertices were derived from
			centerPositions.RemoveLast();
			vertsRemoved = true;
		}

		return vertsRemoved;
	}
	/// <summary>
	/// Recalculates the widths of the vertices based on the amount of time they have been alive.  
	/// </summary>
	private void SetVertexWidths() {
		LinkedListNode<Vertex> leftVertNode = leftVertices.First;
		LinkedListNode<Vertex> rightVertNode = rightVertices.First;

		float widthDelta = widthStart - widthEnd;
		float timeDelta = lifeTime - changeTime;

		//iterate through all the left and right vertex pairs
		while (leftVertNode != null) {
			Vertex leftVert = leftVertNode.Value;
			Vertex rightVert = rightVertNode.Value;

			//if the alive time of this vertex pair is greater than the specified time to begin changing width
			if (leftVert.TimeAlive > changeTime) {
				//calculate the new width of the trail based on the amount of time the vertex has been alive
				float width = widthStart - (widthDelta * ( (leftVert.TimeAlive - changeTime) / timeDelta) );

				//each vertex is half of the calculated trail width from the center
				float halfWidth = width * 0.5f;

				//since the left and right vertices were created at the same time, the new width is the same for both vertices
				leftVert.AdjustWidth(halfWidth);
				rightVert.AdjustWidth(halfWidth);
			}

			//increment the left and right vertex nodes
			leftVertNode = leftVertNode.Next;
			rightVertNode = rightVertNode.Next;
		}
	}

	/// <summary>
	/// Sets the mesh and the polygon collider of the mesh.
	/// </summary>
	private void SetMesh() {
		//only continue if there are at least two center positions in the collection
		if (centerPositions.Count < 2) {
			return;
		}

		//create an array for the 1) trail vertices, 2) trail uvs, 3) trail triangles, and 4) vertices on the collider path
		Vector3[] vertices = new Vector3[centerPositions.Count * 2];
		Vector2[] uvs = new Vector2[centerPositions.Count * 2];
		int[] triangles = new int[ (centerPositions.Count - 1) * 6];
		Vector2[] colliderPath = new Vector2[ (centerPositions.Count - 1) * 2];

		LinkedListNode<Vertex> leftVertNode = leftVertices.First;
		LinkedListNode<Vertex> rightVertNode = rightVertices.First;
		//get the change in time between the first and last pair of vertices
		float timeDelta = leftVertices.Last.Value.TimeAlive - leftVertices.First.Value.TimeAlive;

		//iterate through all the pairs of vertices (left + right)
		for (int i = 0; i < leftVertices.Count; ++i) {
			Vertex leftVert = leftVertNode.Value;
			Vertex rightVert = rightVertNode.Value;

			//trail vertices
			int vertIndex = i * 2;
			vertices[vertIndex] = leftVert.Position;
			vertices[vertIndex + 1] = rightVert.Position;

			//collider vertices 
			colliderPath[i] = leftVert.Position;
			colliderPath[colliderPath.Length - (i + 1) ] = rightVert.Position;

			//trail uvs
			float uvValue = leftVert.TimeAlive / timeDelta;
			uvs[vertIndex] = new Vector2(uvValue, 0);
			uvs[vertIndex + 1] = new Vector2(uvValue, 1);

			//trail triangles
			if (i > 0) {
				int triIndex = (i - 1) * 6;
				triangles[triIndex] = vertIndex -2;
				triangles[triIndex + 1] = vertIndex - 1;
				triangles[triIndex + 2] = vertIndex + 1;
				triangles[triIndex + 3] = vertIndex - 2;
				triangles[triIndex + 4] = vertIndex + 1;
				triangles[triIndex + 5] = vertIndex;
			}

			//increment the left and right vertex nodes
			leftVertNode = leftVertNode.Next;
			rightVertNode = rightVertNode.Next; 
		}

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;

		if (colliderEnabled) {
			collider.SetPath(0, colliderPath);
		}
	}
		
	private bool isInRange() {
		Vector3 dist = this.transform.position - player.transform.position;
		if (dist.magnitude <= inkRange) {
			return true;
		} else {
			return false;
		}
	}

	private bool detectCircle () {
		int size = centerPositions.Count; 
		double center_x = 0, center_y = 0, error = 0, error_r, radius = 0, diff_x, diff_y, x, y;
		if (size > 20) {
			for (LinkedListNode<Vector3> iter = centerPositions.First; iter != null; iter = iter.Next) {
				x = iter.Value.x;
				y = iter.Value.y;
				center_x += x;
				center_y += y;
			}
			center_x /= size;
			center_y /= size;
			for (LinkedListNode<Vector3> iter = centerPositions.First; iter != null; iter = iter.Next) {
				x = iter.Value.x;
				y = iter.Value.y;
				diff_x = (x - center_x) * (x - center_x);
				diff_y = (y - center_y) * (y - center_y);
				radius += Math.Sqrt (diff_x + diff_y);
			}
			radius /= size;
			for (LinkedListNode<Vector3> iter = centerPositions.First; iter != null; iter = iter.Next) {
				x = iter.Value.x;
				y = iter.Value.y;
				diff_x = (x - center_x) * (x - center_x);
				diff_y = (y - center_y) * (y - center_y);
				error_r = Math.Sqrt (diff_x + diff_y);
				error += (error_r - radius) * (error_r - radius);
			}
			error /= size;
			return error / radius < 0.15;
		}
		return false;
	}

	//************
	//
	// Private Classes
	//
	//************

	private class Vertex {
		private Vector3 centerPosition; //the center position in the trail that this vertex was derived from
		private Vector3 derivedDirection; //the direction from the 1) center position to the 2) position of this vertex
		private float creationTime;

		public Vector3 Position { get; private set; }
		public float TimeAlive { get { return Time.time - creationTime; } }

		public void AdjustWidth(float width) {
			Position = centerPosition + (derivedDirection * width);
		}

		public Vertex(Vector3 position, Vector3 centerPosition, Vector3 derivedDirection) {
			this.Position = position;
			this.centerPosition = centerPosition;
			this.derivedDirection = derivedDirection;
			creationTime = Time.time;
		}
	}


	/// <summary>
	/// //////////////////////////////////////////
	/// </summary>
	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		player=GameObject.Find ("Player");
	}


	int count = 0;

	//Iphone version!
	void FixedUpdate () {
		if (Input.touchCount > 0 && flag) {
			for(int i=0;i<2;i++){
				if (Input.GetTouch (i).phase == TouchPhase.Moved || Input.GetTouch (i).phase == TouchPhase.Began) {
					Vector2 mouse = Input.GetTouch (i).position;//Input.mousePosition;
					Vector2 rawPosition = Camera.main.ScreenToWorldPoint (mouse); //Input.GetTouch(0).position
					if (mouse.x > 220 || mouse.y > 220) {
						GetComponent<Rigidbody2D> ().MovePosition (rawPosition);	
						break;
					} 
				}
				if (Input.GetTouch (i).phase == TouchPhase.Ended) {
					Vector2 mouse = Input.GetTouch (i).position;
					if (mouse.x > 220 || mouse.y > 220) {
						flag = false;	
						break;
					} 
				}	
			}
		}
			
		if (!pausing) {
			//set the mesh and adjust widths if vertices were added or removed
			if (TryAddVertices () | TryRemoveVertices ()) {
				if (widthStart != widthEnd) {
					SetVertexWidths ();
				}
				SetMesh ();

				if (Time.time > patternDetectTime + patternDetectInterval && detectCircle ()) {
					Debug.Log ("circle detected" + count++.ToString());
					patternDetectTime = Time.time;
				}

			}
		}	
		if (flag == false && centerPositions.Count <= 1) {
			if (TryAddVertices () | TryRemoveVertices ()) {
				if (widthStart != widthEnd) {
					SetVertexWidths ();
				}
				SetMesh ();
			}
			centerPositions.Clear ();
			leftVertices.Clear ();
			rightVertices.Clear ();
			SetMesh ();

			Destroy (gameObject);
		}
	}


	/*
	// computer version!
	void FixedUpdate () {   
		Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
		GetComponent<Rigidbody2D>().MovePosition (rawPosition);
		if (!pausing) {
			//set the mesh and adjust widths if vertices were added or removed
			if (TryAddVertices () | TryRemoveVertices ()) {
				if (widthStart != widthEnd) {
					SetVertexWidths ();
				}
				SetMesh ();
				if (Time.time > patternDetectTime + patternDetectInterval && detectCircle ()) {
					Debug.Log ("circle detected" + count++.ToString());
					patternDetectTime = Time.time;
				}
			}
		}
	}
	*/

	public void reflect() {
		
	}
}
