#version 460 core

in vec2 VERTEX;
in vec2 TEX_COORDS;

uniform mat4 TRANSFORM;

out vec2 UV;

void main()
{
    gl_Position = TRANSFORM * vec4(VERTEX, 0, 1);
    UV = TEX_COORDS;
}
