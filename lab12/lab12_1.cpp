#include <SFML/Graphics.hpp>
#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"

float x = 0, y = 0, z = 0;

const char* vertexShaderSource = R"(
    #version 330 core
    layout (location = 0) in vec3 aPos;
    layout (location = 1) in vec3 aColor;

    out vec3 ourColor;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;

    void main()
    {
        gl_Position = projection * view * model * vec4(aPos, 1.0);
        ourColor = aColor;
    }
)";

const char* fragmentShaderSource = R"(
    #version 330 core
    in vec3 ourColor;

    out vec4 FragColor;

    void main()
    {
        FragColor = vec4(ourColor, 1.0);
    }
)";

int main()
{
	sf::ContextSettings settings;
	settings.depthBits = 24;
	settings.stencilBits = 8;

	sf::RenderWindow window(sf::VideoMode(800, 600), "OpenGL", sf::Style::Default, settings);

	glewExperimental = GL_TRUE;
	glewInit();

	glEnable(GL_DEPTH_TEST);

	GLuint vertexArray, vertexBuffer, shaderProgram;

	glGenVertexArrays(1, &vertexArray);
	glGenBuffers(1, &vertexBuffer);

	glBindVertexArray(vertexArray);

	float vertices[] = {
		0.0f,  0.5f,  0.0f,  1.0f, 0.0f, 0.0f, // Vertex 1: Red
		-0.5f, -0.5f,  -0.5f,  0.0f, 1.0f, 0.0f, // Vertex 2: Green
		0.5f, -0.5f,  -0.5f,  0.0f, 0.0f, 1.0f, // Vertex 3: Blue

		0.0f,  0.5f,  0.0f,  1.0f, 0.0f, 0.0f, // Vertex 1: Red
		0.5f, -0.5f,  -0.5f,  0.0f, 0.0f, 1.0f, // Vertex 3: Blue
		0.0f, 0.0f, 0.5f,  1.0f, 1.0f, 0.0f, // Vertex 4: Yellow

		0.0f,  0.5f,  0.0f,  1.0f, 0.0f, 0.0f, // Vertex 1: Red
		0.0f, 0.0f, 0.5f,  1.0f, 1.0f, 0.0f, // Vertex 4: Yellow
		-0.5f, -0.5f,  -0.5f,  0.0f, 1.0f, 0.0f, // Vertex 2: Green

		-0.5f, -0.5f,  -0.5f,  0.0f, 1.0f, 0.0f, // Vertex 2: Green
		0.5f, -0.5f,  -0.5f,  0.0f, 0.0f, 1.0f, // Vertex 3: Blue
		0.0f, 0.0f, 0.5f,  1.0f, 1.0f, 0.0f  // Vertex 4: Yellow
	};

	glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	// Position attribute
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	// Color attribute
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)(3 * sizeof(float)));
	glEnableVertexAttribArray(1);

	// Shader setup
	GLuint vertexShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vertexShader, 1, &vertexShaderSource, NULL);
	glCompileShader(vertexShader);

	GLuint fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fragmentShader, 1, &fragmentShaderSource, NULL);
	glCompileShader(fragmentShader);

	shaderProgram = glCreateProgram();
	glAttachShader(shaderProgram, vertexShader);
	glAttachShader(shaderProgram, fragmentShader);
	glLinkProgram(shaderProgram);
	glDeleteShader(vertexShader);
	glDeleteShader(fragmentShader);

	GLint modelLoc = glGetUniformLocation(shaderProgram, "model");
	GLint viewLoc = glGetUniformLocation(shaderProgram, "view");
	GLint projectionLoc = glGetUniformLocation(shaderProgram, "projection");

	sf::Clock clock;


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
				glViewport(0, 0, event.size.width, event.size.height);
			}
			else if (event.type == sf::Event::KeyPressed)
			{
				switch (event.key.code)
				{
				case sf::Keyboard::Key::Q:
					x += 0.1f;
					break;
				case sf::Keyboard::Key::A:
					x -= 0.1f;
					break;
				case sf::Keyboard::Key::W:
					y += 0.1f;
					break;
				case sf::Keyboard::Key::S:
					y -= 0.1f;
					break;
				case sf::Keyboard::Key::E:
					z += 0.1f;
					break;
				case sf::Keyboard::Key::D:
					z -= 0.1f;
					break;
				}
			}
		}

		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		glUseProgram(shaderProgram);

		glm::mat4 model = glm::mat4(1.0f);

		model = glm::translate(model, glm::vec3(x, 0.0f, 0.0f));
		model = glm::translate(model, glm::vec3(0.0f, y, 0.0f));
		model = glm::translate(model, glm::vec3(0.0f, 0.0f, z));

		glm::mat4 view = glm::lookAt(glm::vec3(0.0f, 0.0f, 3.0f), glm::vec3(0.0f, 0.0f, 1.0f), glm::vec3(0.0f, 1.0f, 0.0f));
		glm::mat4 projection = glm::perspective(glm::radians(45.0f), 800.0f / 600.0f, 0.1f, 100.0f);

		glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "model"), 1, GL_FALSE, &model[0][0]);
		glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "view"), 1, GL_FALSE, &view[0][0]);
		glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "projection"), 1, GL_FALSE, &projection[0][0]);

		glDrawArrays(GL_TRIANGLES, 0, 12);

		window.display();
	}

	glDeleteVertexArrays(1, &vertexArray);
	glDeleteBuffers(1, &vertexBuffer);
	glDeleteProgram(shaderProgram);

	return 0;
}