#version 460 core

in vec2 UV;
in vec4 COLOR;

uniform sampler2D TEXTURE;

out vec4 color;

void main()
{
    color = texture(TEXTURE, UV) * COLOR;
}
