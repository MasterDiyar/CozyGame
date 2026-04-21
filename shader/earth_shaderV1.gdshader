shader_type canvas_item;

uniform sampler2D layer_r : repeat_enable, filter_linear_mipmap_anisotropic; 
uniform sampler2D layer_g : repeat_enable, filter_linear_mipmap_anisotropic;
uniform sampler2D layer_b : repeat_enable, filter_linear_mipmap_anisotropic;
uniform sampler2D noise_img : repeat_enable, filter_linear_mipmap_anisotropic;

uniform float uv_scale = 10.0;

// Функция для поворота координат вокруг центра (0.5, 0.5)
vec2 rotate(vec2 uv, float degrees) {
    float radians = degrees * 0.0174533; // Перевод в радианы
    float s = sin(radians);
    float c = cos(radians);
    
    // Сдвигаем UV в центр, чтобы поворот шел не от угла
    uv -= 0.5;
    // Матрица поворота
    uv = vec2(uv.x * c - uv.y * s, uv.x * s + uv.y * c);
    // Возвращаем назад
    uv += 0.5;
    return uv;
}

void fragment() {
    vec2 base_uv = UV * uv_scale;
    
    float noise = texture(noise_img, UV).r; 
    vec4 weights = texture(TEXTURE, UV);
    
    // Поворачиваем координаты индивидуально для каждого слоя
    vec2 uv_r = rotate(base_uv, 10.0) + vec2(0.123, 0.456);
	vec2 uv_g = base_uv + vec2(0.789, 0.012);
	vec2 uv_b = rotate(base_uv, -100.0) + vec2(0.345, 0.678);
    
    // Читаем текстуры с новыми координатами
    vec4 tex_r = texture(layer_r, uv_r);
    vec4 tex_g = texture(layer_g, uv_g);
    vec4 tex_b = texture(layer_b, uv_b);
    
    vec4 final_color = tex_r * weights.r + 
                       tex_g * weights.g + 
                       tex_b * weights.b;
                       
    final_color.rgb *= (0.8 + noise * 0.3);
    
    // Важно: берем альфу маски, чтобы спрайт не стал сплошным квадратом
    COLOR = vec4(final_color.rgb, texture(TEXTURE, UV).a);
}