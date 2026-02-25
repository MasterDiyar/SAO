shader_type canvas_item;

uniform vec2 player_pos; // Позиция игрока, передаваемая из скрипта
uniform float interaction_radius = 50.0; // Радиус взаимодействия
uniform float speed = 2.0;
uniform float amplitude = 20.0;
uniform float boost_amplitude = 60.0; // Амплитуда при касании

void vertex() {
    // 1. Получаем мировую позицию конкретного инстанса травы
    vec2 world_pos = (MODEL_MATRIX * vec4(0.0, 0.0, 0.0, 1.0)).xy;
    
    // 2. Считаем расстояние до игрока
    float dist = distance(world_pos, player_pos);
    
    // 3. Определяем силу колыхания: если близко — используем boost_amplitude
    float current_amp = amplitude;
	float wind = .0;
	float strength = .0;
    if (dist < interaction_radius) {
        current_amp = boost_amplitude;
		float phase_offset = INSTANCE_CUSTOM.r * 10.0;
    	strength = 1.0 - UV.y;
    	wind = 1.1;
    }
	else{
    	float phase_offset = INSTANCE_CUSTOM.r * 10.0;
    	strength = 1.0 - UV.y;
    	wind = sin(TIME * speed + phase_offset);
    }
    VERTEX.x += wind * current_amp * pow(strength, 2.0);
}