#pragma once
#include "Headers.h"
#include "Camera.h"
#include "Mesh.h"
#include "Light.h"
#include "Entity.h"
#include <vector>

class Scene
{
	vector<glm::vec3> positions;
	vector<glm::mat4> modelMatrices;
	vector<float> sizes;
	vector<float> orbitSpeeds;
protected:
	float deltaTime;
	sf::Clock cl;
	sf::Clock globalCl;

public:
	inline void ResetClock()
	{
		deltaTime = cl.getElapsedTime().asSeconds();
		cl.restart();
	}
	vector<Mesh> meshes;
	vector<Shader> shaders;
	vector<Entity> entities;
	Camera camera;

	PointLight pl;
	DirLight dl;

	Scene(vector<string>& meshes_path, vector<string>& textures_path, vector<string>& verts_shaders_path, vector<string>& frag_shaders_path)
	{
		camera = Camera();
		for (size_t i = 0; i < meshes_path.size(); i++)
		{
			meshes.push_back(Mesh(meshes_path[i], textures_path[i]));
			shaders.push_back(Shader(verts_shaders_path[i], frag_shaders_path[i]));
		}

		init();
	}

	~Scene()
	{
		for (auto& mesh : meshes)		
			mesh.ReleaseVBO();		
		for (auto& shader : shaders)		
			shader.Release();		
	}
	void init()
	{
		set_lights();
	}

	void move(float x, float y, float z)
	{
		entities[1].do_move(x, y, z);
	}

	void set_lights()
	{
		for (auto& shader : shaders)
		{
			init_light(&shader, &camera);
			dl.SetUniforms(&shader);
		}
	}

	void draw()
	{
		for (auto& shader : shaders)
		{
			camera.UpdateUniforms(&shader);
			dl.SetUniforms(&shader);
		}


		for (unsigned int i = 0; i < entities.size(); i++)
		{
			entities[i].shader->use();

			glActiveTexture(GL_TEXTURE0);
			glBindTexture(GL_TEXTURE_2D, entities[i].mesh->texture);
			glUniform1i(glGetUniformLocation(entities[i].shader->ID, "ourTexture"), 0);
			glUseProgram(0);

			entities[i].draw();
		}
	}
};