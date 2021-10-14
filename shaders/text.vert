#version 460 core

in vec2 VERTEX;
in vec2 TEX_COORDS;
out vec2 UV;
uniform mat4 TRANSFORM;

void main()
{
    gl_Position = TRANSFORM * vec4(VERTEX, 0, 1);
    UV = TEX_COORDS;
}
