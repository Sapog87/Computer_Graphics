#include "Headers.h"
#include "Mesh.h"
#include "Shader.h"
#include "Scene.h"
#include <iostream>

using namespace std;

static void make_scene(Scene* s)
{
	Entity ground = Entity(&s->meshes[0], &s->shaders[0]);
	ground.do_scale(20, 20, 0);
	s->entities.push_back(ground);

	Entity blimp = Entity(&s->meshes[1], &s->shaders[1]);
	blimp.do_move(0, 0, 30);
	blimp.do_rotate(90, glm::vec3{ 0.0f, 1.0f, 10.0f });
	s->entities.push_back(blimp);

	Entity tree = Entity(&s->meshes[2], &s->shaders[2]);
	tree.do_scale(0.1f, 0.1f, 0.1f);
	tree.do_rotate(70, glm::vec3{ 1.0f, 0.0f, 0.0f });
	s->entities.push_back(tree);

	for (int i = 0; i < 5; i++)
	{
		Entity cloud = Entity(&s->meshes[3], &s->shaders[3]);
		cloud.do_scale(0.005, 0.005, 0.005);
		int x = -10 + rand() % 20;
		int y = -10 + rand() % 20;
		cloud.do_move(x, y, 30);
		s->entities.push_back(cloud);
	}

	for (int i = 0; i < 2; i++)
	{
		Entity balloon = Entity(&s->meshes[4], &s->shaders[4]);
		balloon.do_scale(0.05, 0.05, 0.05);
		int x = -10 + rand() % 20;
		int y = -10 + rand() % 20;
		balloon.do_move(x, y, 30);
		s->entities.push_back(balloon);
	}
}

int main()
{
	srand(static_cast<unsigned int>(time(0)));

	sf::Window window(sf::VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "Gifts for everyone", sf::Style::Default, sf::ContextSettings(24));
	window.setVerticalSyncEnabled(true);
	window.setActive(true);
	GLenum errorcode = glewInit();
	if (errorcode != GLEW_OK)
		throw std::runtime_error(std::string(reinterpret_cast<const char*>(glewGetErrorString(errorcode))));

	glEnable(GL_DEPTH_TEST);
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);
	vector<string> meshes{ "models/cube.obj", "models/blimp.obj", "models/tree.obj", "models/cloud.obj", "models/balloon.obj" };
	vector<string> textures{ "textures/floor.png ",	 "textures/blimp.jpg", "textures/fir.jpg", "textures/cloud.jpg", "textures/balloon.jpg" };
	vector<string> vertes_s{ "shaders/vertex.vert", "shaders/vertex.vert", "shaders/vertex.vert", "shaders/vertex.vert", "shaders/vertex.vert" };
	vector<string> frags_s{ "shaders/fragment.frag", "shaders/fragment.frag", "shaders/fragment.frag", "shaders/fragment.frag", "shaders/fragment.frag" };
	Scene* s = new Scene(meshes, textures, vertes_s, frags_s);
	make_scene(s);
	while (window.isOpen())
	{
		sf::Event event;
		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
				window.close();
			}
			else if (event.type == sf::Event::Resized)
			{
				glViewport(0, 0, event.size.width,
					event.size.height);
			}
			else if (event.type == sf::Event::KeyPressed)
			{
				switch (event.key.code)
				{
				case sf::Keyboard::W:
					s->move(0.0f, 0.1f, 0);
					break;
				case sf::Keyboard::A:
					s->move(-0.1f, 0, 0);
					break;
				case sf::Keyboard::S:
					s->move(0.0f, -0.1f, 0);
					break;
				case sf::Keyboard::D:
					s->move(0.1f, 0, 0);
					break;
				case sf::Keyboard::Q:
					s->move(0.0f, 0, -0.1f);
					break;
				case sf::Keyboard::E:
					s->move(0.0f, 0, 0.1f);
					break;
				default:
					break;
				}
			}
		}
		s->ResetClock();

		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		s->draw();
		checkOpenGLerror();
		window.display();
	}

	return 0;
}
