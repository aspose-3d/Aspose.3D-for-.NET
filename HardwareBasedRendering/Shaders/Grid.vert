#version 450
#extension GL_ARB_separate_shader_objects : enable


//Vertex buffer consist of position and color
layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;

//World view projection matrix
layout(push_constant) uniform Mat {
	mat4 worldViewProj;
};

//Pass the line color to fragment shader
layout(location = 0) out vec3 lineColor;

out gl_PerVertex {
    vec4 gl_Position;
};
void main()
{
	//Calculate the position in screen coordinate system
    gl_Position = worldViewProj * vec4(position, 1.0f);
	//pass the color from vertex buffer to fragment shader
    lineColor = color;
}