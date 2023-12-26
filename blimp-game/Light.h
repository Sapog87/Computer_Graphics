#pragma once
#include "Headers.h"
#include "Shader.h"

void init_light(const Shader* s, const Camera* c);
const Camera* camera;


void init_light(const Shader* s, const Camera* c)
{
	s->use();
	float shininess = 64.0f;
	s->SetFloat("material.shininess", shininess);
	glm::vec4 diffuse{ 0.7f, 0.7f, 0.7f, 1.f };
	glUseProgram(0);
	camera = c;
}


struct DirLight
{
	glm::vec3 direction{ 0.5f, 0.f, -0.3f };
	glm::vec3 ambient{ 0.2f, 0.2f, 0.2f };
	glm::vec3 diffuse{ 0.5f, 0.5f, 0.5f };
	glm::vec3 specular{ 1.0f, 1.0f, 1.0f };

	void SetUniforms(const Shader* s)
	{
		glm::vec3 position = camera->Position;
		s->use();
		s->SetVec3("viewPos", position);

		s->SetVec3("dirLight.direction", direction);
		s->SetVec3("dirLight.ambient", ambient);
		s->SetVec3("dirLight.diffuse", diffuse);
		s->SetVec3("dirLight.specular", specular);

		glUseProgram(0);
	}
};

struct PointLight
{
	glm::vec3 lposition{ 2.0f,0.0f,0.0f };
	glm::vec3 ambient{ 0.2f, 0.2f, 0.2f, };
	glm::vec3 diffuse{ 1.0f, 1.0f, 1.0f };
	glm::vec3 specular{ 1.0f, 1.0f, 1.0f };
	float constant = 1.0f;
	float linear = 0.09f;
	float quadratic = 0.032f;

	void SetUniforms(const Shader* s, glm::vec3 lamp_pos)
	{
		glm::vec3 direction = camera->Front;
		glm::vec3 position = camera->Position;
		s->use();

		s->SetVec3("viewPos", position);

		s->SetVec3("pointLight.position", lamp_pos);
		s->SetVec3("pointLight.ambient", ambient);
		s->SetVec3("pointLight.diffuse", diffuse);
		s->SetVec3("pointLight.specular", specular);
		s->SetFloat("pointLight.constant", constant);
		s->SetFloat("pointLight.linear", linear);
		s->SetFloat("pointLight.quadratic", quadratic);

		glUseProgram(0);
	}

};
