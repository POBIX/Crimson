#version 460 core

in vec2 UV;
uniform sampler2D TEXTURE;
out vec4 color;

void main()
{
    color = texture2D(TEXTURE, UV);
}
