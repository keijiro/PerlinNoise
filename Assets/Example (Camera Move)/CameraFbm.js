#pragma strict

var octave = 2;

var positionFrequency = 0.5;
var rotationFrequency = 0.8;
var positionAmount = 0.15;
var rotationAmount = 1.5;

private var positionTime = 0.0;
private var rotationTime = 0.0;
private var frequencyScale = 1.0;
private var amountScale = 1.0;

function Update() {
	positionTime += Time.deltaTime * positionFrequency * frequencyScale;
	rotationTime += Time.deltaTime * rotationFrequency * frequencyScale;

	var dx = Perlin.Fbm(positionTime, octave);
	var dy = Perlin.Fbm(positionTime + 10, octave);
	var dz = Perlin.Fbm(positionTime + 20, octave);
	var rx = Perlin.Fbm(rotationTime + 30, octave);
	var ry = Perlin.Fbm(rotationTime + 40, octave);

	transform.localPosition = Vector3(dx, dy, dz) * (positionAmount * amountScale);
	transform.localRotation =
		Quaternion.AngleAxis(rx * rotationAmount * amountScale, Vector3.right) *
		Quaternion.AngleAxis(ry * rotationAmount * amountScale, Vector3.up);
}

function OnGUI() {
	frequencyScale = GUI.HorizontalSlider(Rect(10, 30, 200, 32), frequencyScale, 0.0, 2.0);
	amountScale = GUI.HorizontalSlider(Rect(10, 60, 200, 32), amountScale, 0.0, 2.0);
}
