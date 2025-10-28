//#version 460 core
//
//struct Particle
//{
//    vec4 color;
//    vec2 gravity;
//    vec2 velocity;
//    vec2 position;
//    vec2 size;
//    float lifetime;
//    float elapsed;
//};
//
//layout (std430, binding = 3) buffer particlesSSBO
//{
//    Particle particles[];
//};
////uniform Particle particles[256];
//uniform int PARTICLES;
//uniform vec2 SCREEN_SIZE;
//uniform vec2 POSITION;
//
//out vec4 color;
//
//bool isParticle(vec2 pos, Particle p)
//{
//    return pos.x > p.position.x && pos.x < p.position.x + p.size.x &&
//    pos.y > p.position.y && pos.y < p.position.y + p.size.y;
//}
//
//void main()
//{
//    vec2 pos = gl_FragCoord.xy;
//    pos.y = SCREEN_SIZE.y - pos.y;
//    for (int i = 0; i < PARTICLES; i++)
//    {
//        if (isParticle(pos - POSITION, particles[i]))
//        {
//            color = particles[i].color;
//            return;
//        }
//    }
//    color = vec4(0);
//}

#version 460 core

in vec2 UV;
uniform sampler2D TEXTURE;

out vec4 color;

void main()
{
    color = texture(TEXTURE, UV);
}
