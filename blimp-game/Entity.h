#pragma once
#include "Headers.h"
#include "Mesh.h"
#include "Shader.h"
#include "Camera.h"

class Entity
{
	float x, y, z;
	float rx = 1, ry = 0, rz = 0;
	float cx = 1, cy = 1, cz = 1;
	float degrees = 0;

	void update_uniforms()
	{
		auto model_matrix = glm::translate(glm::mat4(1.f), position) * glm::rotate(glm::mat4(1.f),
			glm::radians(degrees), rotation) * glm::scale(glm::mat4(1.f), scale);
		shader->SetMat4("model", model_matrix);

	}
public:
	string name;
	Mesh* mesh;
	Shader* shader;


	glm::vec3 position{ x,y,z };
	glm::vec3 rotation{ rx,ry,rz };
	glm::vec3 scale{ cx,cy,cz };


	Entity() {}
	Entity(Mesh* mesh, Shader* shader, string name = "")
	{
		this->mesh = mesh;
		this->shader = shader;
		this->name = name;
	}

	~Entity()
	{
	}


	void draw()
	{
		if (!mesh || !shader)
			return;
		shader->use();
		update_uniforms();
		mesh->Draw(shader);
		checkOpenGLerror();
		glUseProgram(0);
	}

	void do_move(float x, float y, float z)
	{
		this->x += x;
		this->y += y;
		this->z += z;
		position = glm::vec3(this->x, this->y, this->z);
	}

	void do_rotate(float degrees, glm::vec3 rot)
	{
		rotation = rot;
		this->degrees = degrees;
	}

	void do_scale(float cx, float cy, float cz)
	{
		this->cx = cx;
		this->cy = cy;
		this->cz = cz;
		scale = { cx,cy,cz };
	}
};