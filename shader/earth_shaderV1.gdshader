shader_type canvas_item;

uniform sampler2D layer_r : repeat_enable; 
uniform sampler2D layer_g : repeat_enable;
uniform sampler2D layer_b : repeat_enable;

uniform sampler2D noise_img : repeat_enable;

uniform float uv_scale = 10.0;

void fragment() {
	vec2 tiled_uv = UV * uv_scale;
	
	float noise = texture(noise_img, UV).r; 
	
    vec4 weights = texture(TEXTURE, UV);
	
    vec4 tex_r = texture(layer_r, tiled_uv);
    vec4 tex_g = texture(layer_g, tiled_uv);
    vec4 tex_b = texture(layer_b, tiled_uv);
    
    vec4 final_color = tex_r * weights.r + 
                       tex_g * weights.g + 
                       tex_b * weights.b;
					
	final_color.rgb *= (0.9 + noise * 0.2);
    
    COLOR = vec4(final_color.rgb, weights.r + weights.g + weights.b + weights.a);
}