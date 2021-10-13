#version 450

layout(set = 0, binding = 0) uniform ModelBuffer
{
    mat4 Model;
};

layout(set = 1, binding = 0) uniform ViewBuffer
{
    mat4 View;
};

layout(set = 1, binding = 1) uniform ProjectionBuffer
{
    mat4 Projection;
};



layout(location = 0) in vec3 Position;
layout(location = 1) in vec4 Color;
layout(location = 0) out vec4 fsin_color;

void main()
{
    vec4 modelPosition = Model * vec4(Position, 1);
    vec4 viewPosition = View * modelPosition;
    vec4 projPosition = Projection * viewPosition;
    gl_Position = projPosition;
    fsin_color = Color;
}