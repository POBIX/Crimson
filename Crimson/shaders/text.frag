#version 460 core

in vec2 UV;

uniform sampler2D TEXTURE;
uniform vec4 COLOR;

out vec4 color;

void main()
{
    float tex = texture(TEXTURE, UV).r;
    color = vec4(tex * COLOR.rgb, tex);
}
