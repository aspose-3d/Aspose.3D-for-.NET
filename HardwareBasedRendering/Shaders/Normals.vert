#version 450
#extension GL_ARB_separate_shader_objects : enable
layout (location = 0) in vec3 position;

layout(push_constant) uniform Mat {
	mat4 viewProj;
};
out gl_PerVertex {
    vec4 gl_Position;
};
void main()
{
    gl_Position = viewProj * vec4(position, 1.0f);
}
