#version 460 core

in vec2 VERTEX;
in vec2 TEX_COORDS;

uniform mat4 TRANSFORM;
uniform mat4 CAMERA;

struct Particle
{
    vec4 color;
    vec2 gravity;
    vec2 velocity;
    vec2 position;
    vec2 initialPos;
    vec2 size;
    float lifetime;
    float elapsed;
    float delay;
    float rotation;
    bool visible;
};

layout (std430, binding = 4) buffer particlesSSBO
{
    Particle particles[];
};

out vec2 UV;
out int ID;

mat4 trans(vec2 v)
{
    mat4 m = mat4(1);
    m[3][0] = v.x;
    m[3][1] = v.y;
    return m;
}

mat4 scale(vec2 v)
{
    mat4 m = mat4(1);
    m[0][0] = v.x;
    m[1][1] = v.y;
    return m;
}

mat4 rotate(float angle)
{
    float c = cos(angle);
    float s = sin(angle);
    mat4 m = mat4(1);
    m[0][0] = c;
    m[1][0] = -s;
    m[0][1] = s;
    m[1][1] = c;
    return m;
}

void main()
{
    ID = gl_InstanceID;
    UV = TEX_COORDS;

    Particle p = particles[ID];

    mat4 pos = CAMERA * trans(p.position) * scale(p.size) * rotate(p.rotation);

    gl_Position = (TRANSFORM + pos) * vec4(VERTEX, 0, 1);
}

