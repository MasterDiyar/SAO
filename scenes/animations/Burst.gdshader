shader_type canvas_item;

// Точка, из которой исходит волна (в экранных координатах от 0.0 до 1.0)
uniform vec2 center = vec2(0.5, 0.5);
// Скорость движения волн
uniform float speed = 4.0;
// Частота (расстояние между гребнями волн)
uniform float frequency = 20.0;
// Ширина/интенсивность высветления
uniform float brightness_amount = 0.2;

void fragment() {
    // Получаем исходный цвет пикселя
    vec4 color = texture(TEXTURE, UV);
    
    // Считаем расстояние от центра до текущего пикселя
    float dist = distance(UV, center);
    
    // Создаем волну с помощью синуса
    // TIME - встроенная переменная времени в Godot
    float wave = sin(dist * frequency - TIME * speed);
    
    // Приводим значение синуса из диапазона [-1, 1] в [0, 1] для мягкости
    // Или используем max(0.0, wave), чтобы высветлять только гребни
    float mask = smoothstep(-0.5, 1.0, wave);
    
    // Добавляем яркость к исходному цвету
    color.rgb += mask * brightness_amount;
    
    COLOR = color;