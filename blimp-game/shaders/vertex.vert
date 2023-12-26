#version 330 core

layout (location = 0) in vec3 pos;
layout (location = 1) in vec3 normal;
layout (location = 2) in vec2 texcoord;

out vec2 otexcoord;
out vec3 onormal;
out vec3 opos;

uniform mat4 view;
uniform mat4 projection;
uniform mat4 model;

void main(){
     opos = vec3(model*vec4(pos,1.0));
     onormal = mat3(transpose(inverse(model)))*normal;
     gl_Position = projection * view * vec4(opos,1.0);
     otexcoord =  vec2(texcoord.x, 1.0f - texcoord.y);
}