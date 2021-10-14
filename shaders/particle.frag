#version 460 core

struct Particle
{
    vec2 position;
    vec4 color;
    vec2 size;
    // the c# class has a few more fields. we use them to calculate the position on the CPU.
};

uniform Particle particles[256];
uniform int PARTICLES;
uniform vec2 SCREEN_SIZE;

out vec4 color;

bool isParticle(vec2 pos, Particle p)
{
    return pos.x > p.position.x && pos.x < p.position.x + p.size.x &&
    pos.y > p.position.y && pos.y < p.position.y + p.size.y;
}

void main()
{
    vec2 pos = gl_FragCoord.xy;
    pos.y = SCREEN_SIZE.y - pos.y;
    for (int i = 0; i < PARTICLES; i++)
    {
        if (isParticle(pos, particles[i]))
        {
            color = particles[i].color;
            return;
        }
    }
    color = vec4(0);
}
