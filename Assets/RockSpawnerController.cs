﻿using UnityEngine;
using System.Collections;

public class RockSpawnerController : MonoBehaviour {

	public float timeBetweenSpawns = 2f;
	public GameObject human;
	
	public float moveSpeed = 3;
	public Transform[] pointArray;
	
	public int currentPointIndex = 0;
	public Vector2 currentTargetPoint;
	
	public float locationErrorMargin = 0.1f;
	public bool pathMode = false;	
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnHuman", 0, timeBetweenSpawns);
		
		if (pointArray.Length >= 2) {
			pathMode = true;
			currentTargetPoint = GetNextPoint();
			transform.position = new Vector3(transform.position.x, transform.position.y, pointArray[0].position.z);
		} else if (pointArray.Length == 1) {
			pathMode = false;
			transform.position = pointArray[0].position;
		} else {
			pathMode = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pathMode) {
			float distance = Vector2.Distance (transform.position, currentTargetPoint);
			
			if (distance > locationErrorMargin) {
				MoveToPoint (currentTargetPoint);
			} else {
				currentTargetPoint = GetNextPoint ();
			}
		}
	}
	
	void SpawnHuman() {
		Instantiate (human, transform.position, Quaternion.identity);
	}
	
	void SpawnHuman(Vector2 position) {
		Instantiate (human, position, Quaternion.identity);
	}
	
	Vector2 GetNextPoint() {
		if (currentPointIndex + 1 < pointArray.Length) {
			return pointArray[++currentPointIndex].position;
		} else {
			currentPointIndex = 0;
			return pointArray[currentPointIndex].position;
		}
	}
	
	void MoveToPoint(Vector2 point) {
		transform.position = Vector2.MoveTowards(transform.position, point, moveSpeed * Time.deltaTime);
	}

}



