shader_type canvas_item;

uniform vec2 scale = vec2(1.0, 1.0);
uniform float grid_scale = 2.0; 
// Для новой формулы лучше подходят значения от 2.0 до 15.0
uniform float blend_power : hint_range(2.0, 15.0) = 8.0; 

varying vec2 world_position;

void vertex() {
    world_position = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
}

vec2 hash(vec2 p) {
    vec3 p3 = fract(vec3(p.xyx) * vec3(0.1031, 0.1030, 0.0973));
    p3 += dot(p3, p3.yzx + 33.33);
    return fract((p3.xx + p3.yz) * p3.zy);
}

void fragment() {
    vec2 uv = world_position * scale * 0.01;
    
    vec2 grid_uv = uv * grid_scale;
    vec2 cell_id = floor(grid_uv); 
    vec2 cell_fract = fract(grid_uv); 
    
    vec4 final_color = vec4(0.0);
    float total_weight = 0.0;
    
    for (int y = -1; y <= 1; y++) {
        for (int x = -1; x <= 1; x++) {
            vec2 neighbor = vec2(float(x), float(y));
            vec2 random_point = hash(cell_id + neighbor);
            vec2 to_point = neighbor + random_point - cell_fract;
            
            float dist = length(to_point);
            
            // ИСПРАВЛЕНИЕ: Используем экспоненту (Гауссиан) вместо clamp.
            // Эта функция плавно убывает, но никогда не равна ровно 0.0
            float weight = exp(-dist * dist * blend_power);
            
            vec2 random_uv_offset = hash(cell_id + neighbor) * 100.0;
            vec4 tex_color = texture(TEXTURE, uv + random_uv_offset);
            
            final_color += tex_color * weight;
            total_weight += weight;
        }
    }
    COLOR = final_color / max(total_weight, 0.00001);
}