#version 450
#extension GL_ARB_separate_shader_objects : enable
layout(location = 0) out vec4 color;
layout(push_constant) uniform Styles{
    layout(offset=64) vec4 lineColor;
};
void main()
{
    color = lineColor;
}
