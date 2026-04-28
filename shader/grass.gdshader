shader_type canvas_item;

uniform float speed = 1.5;
uniform float amplitude = 20.0;
uniform float detail = 0.5;

uniform sampler2D screen_texture : filter_nearest_mipmap;

void fragment() {
    COLOR = texture(TEXTURE, UV);
}

float rand(vec2 co) {
    return fract(sin(dot(co, vec2(12.9898, 78.233))) * 43758.5453);
}

void vertex() {
    float noise = rand(floor(MODEL_MATRIX[3].xy));
    
    float height_factor = 1.0 - UV.y;
    
    float time = TIME * speed + noise * 10.0;
    float wave = sin(time) * 1.0;
    wave += sin(time * 1.5 + noise) * 0.5;
    wave += sin(time * 3.0) * 0.2;
    
    VERTEX.x += wave * amplitude * height_factor;
    VERTEX.y += abs(wave) * amplitude * 0.1 * height_factor;
}