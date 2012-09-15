#pragma strict

var speed = 11.0;

function Update() {
	transform.localRotation =
		Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.up) *
		transform.localRotation;
}
