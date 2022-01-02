#version 460 core

in vec2 VERTEX;
in vec2 TEX_COORDS;
in vec4 TEXT_COLOR;
out vec2 UV;
out vec4 COLOR;
uniform mat4 TRANSFORM;

void main()
{
    gl_Position = TRANSFORM * vec4(VERTEX, 0, 1);
    UV = TEX_COORDS;
    COLOR = TEXT_COLOR;
}
