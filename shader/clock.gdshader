shader_type canvas_item;

uniform float fill_amount : hint_range(0.0, 1.0) = 0.5;
uniform bool clockwise = true;
uniform float rotation_offset : hint_range(0.0, 360.0) = 0.0;

void fragment() {
    vec2 uv = UV - vec2(0.5);
    
    float angle = atan(uv.y, uv.x);
    
    float tau = 6.28318530718;
    float normalized_angle = mod((angle / tau) + 0.25 + (rotation_offset / 360.0), 1.0);
    
    if (!clockwise) 
        normalized_angle = 1.0 - normalized_angle;

    vec4 color = texture(TEXTURE, UV);
    
    if (normalized_angle > fill_amount)
        color.a = 0.0;
    
    COLOR = color;
}