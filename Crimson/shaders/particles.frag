#version 460 core

in flat int ID;

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

out vec4 color;

void main()
{
    color = particles[ID].color;
}
