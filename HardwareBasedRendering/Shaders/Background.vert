#version 450
#extension GL_ARB_separate_shader_objects : enable
#extension GL_ARB_shading_language_420pack : enable

out gl_PerVertex {
    vec4 gl_Position;
};



void main()
{
	//calculate an triangle that covers the whole viewport based on gl_VertexIndex
	//so vertex buffer is no longer needed.
	vec2 uv = vec2((gl_VertexIndex << 1) & 2, gl_VertexIndex & 2);
	gl_Position = vec4(uv * 2.0f - 1.0f, 0.0f, 1.0f);
}
