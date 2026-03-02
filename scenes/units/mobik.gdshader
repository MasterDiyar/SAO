shader_type canvas_item;

uniform float speed = 2.0;
uniform float amplitude = 20.0;

void vertex() {
	float strength = 1.0 - UV.y;
	VERTEX.x += sin(TIME * speed) * amplitude * pow(strength, 2);
}

