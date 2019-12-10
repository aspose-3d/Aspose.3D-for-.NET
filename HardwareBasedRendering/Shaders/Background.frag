#version 450
#extension GL_ARB_separate_shader_objects : enable
layout(location = 0) out vec4 outColor;


//These parameters will be provided in LinesRenderer through ICommandList.PushConstant
layout(push_constant) uniform Params {
    float height;
    vec4 upper;
    vec4 lower;
};

void main() {
	//create a vertical gradient color
    float v = gl_FragCoord.y / height;
    outColor = mix(lower, upper, v) + vec4(0.1, 0.1, 0.1, 0.1);
}
