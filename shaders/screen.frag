#version 460 core

uniform sampler2D TEXTURE;
uniform vec2 CAM_SIZE;
uniform vec2 SCREEN_SIZE;

in vec2 UV;
out vec4 color;

struct Light
{
    vec2 pos;
    float radius;
    vec4 color;
    float strength;
};

uniform Light lights[32];
uniform int LIGHTS;

uniform float AMBIENT_LIGHT;

float f(float d, float m) { return 1.0 / (m*d*d + 1.0); }

vec4 calcLight(Light l, vec2 pos)
{
    float dist = distance(pos, l.pos);
    float m = 1.0 / (l.radius * l.radius);
    return l.color * f(dist, m) * l.strength + AMBIENT_LIGHT;
}

vec4 light(vec4 col)
{
    if (LIGHTS == 0 || AMBIENT_LIGHT >= 1) return col;

    // calculate the pixel coordinates according to the camera's resolution (rather than the window's)
    vec2 pos = gl_FragCoord.xy / (SCREEN_SIZE / CAM_SIZE);
    // gl_FragCoords treats (0, 0) as the bottom left corner. The engine treats it as the top left.
    pos.y = CAM_SIZE.y - pos.y;

    vec4 sum = vec4(0);
    for (int i = 0; i < LIGHTS; i++)
        sum += calcLight(lights[i], pos);
    return col * sum;
}

void main()
{
    color = light(texture(TEXTURE, UV));
}
