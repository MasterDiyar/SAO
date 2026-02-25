shader_type canvas_item;

uniform float speed : hint_range(0.1, 10.0) = 2.0;
uniform float distortion : hint_range(-100.0, 100.0) = 30.0; // Сила изгиба
uniform float height_factor : hint_range(1.0, 4.0) = 2.0;   // Степень кривизны

void vertex() {
    // Получаем коэффициент от 0 (низ) до 1 (верх)
    float strength = 1.0 - UV.y;
    
    // Возведение в степень создает эффект изгиба кривой
    // Чем выше height_factor, тем сильнее изгиб будет сконцентрирован вверху
    float curve = pow(strength, height_factor);
    
    float wind = sin(TIME * speed);
    float offset = wind * distortion * curve;
    
    VERTEX.x += offset;
}