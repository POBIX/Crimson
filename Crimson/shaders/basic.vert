#version 460 core

in vec2 VERTEX;
uniform mat4 TRANSFORM;

void main()
{
   gl_Position = TRANSFORM * vec4(VERTEX, 0, 1);
}
