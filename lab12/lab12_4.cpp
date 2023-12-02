#include <SFML/Graphics.hpp>
#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include <cmath>

void hsvToRgb(float hue, float& red, float& green, float& blue) {
	// Ensure hue is in the range [0, 360)
	hue = fmod(hue, 360.0);

	// Normalize saturation and value to be in the range [0, 1]
	float saturation = 1;
	float value = 1;

	// Chroma (color intensity)
	float C = value * saturation;

	// Hue' (normalized hue)
	float Hp = hue / 60.0;

	// X, Y, Z values for the chromaticity coordinates
	double X = C * (1 - fabs(fmod(Hp, 2.0) - 1));
	double M = value - C;

	// Initialize RGB values
	double R = 0.0, G = 0.0, B = 0.0;

	// Determine which sector of the color wheel Hue' is in and set RGB accordingly
	if (0 <= Hp && Hp < 1) {
		R = C;
		G = X;
	}
	else if (1 <= Hp && Hp < 2) {
		R = X;
		G = C;
	}
	else if (2 <= Hp && Hp < 3) {
		G = C;
		B = X;
	}
	else if (3 <= Hp && Hp < 4) {
		G = X;
		B = C;
	}
	else if (4 <= Hp && Hp < 5) {
		R = X;
		B = C;
	}
	else if (5 <= Hp && Hp < 6) {
		R = C;
		B = X;
	}

	// Add the same amount to each channel to match lightness
	red = R + M;
	green = G + M;
	blue = B + M;
}

//float x = 0, y = 0, z = 0;

const char* vertexShaderSource = R"(
    #version 330 core
    layout (location = 0) in vec3 aPos;
    layout (location = 1) in vec3 aColor;

    out vec3 ourColor;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;

    uniform float scale_x;
    uniform float scale_y;

    void main()
    {
        gl_Position = projection * view * model * vec4(aPos.x * scale_x, aPos.y * scale_y, 0.0, 1.0);
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

	GLuint vertexArray, vertexBuffer, shaderProgram, colorBuffer;

	glGenVertexArrays(1, &vertexArray);
	glGenBuffers(1, &vertexBuffer);
	glGenBuffers(1, &colorBuffer);

	glBindVertexArray(vertexArray);

	int numVertices = 361;
	std::vector<float> vertices;
	vertices.push_back(0);
	vertices.push_back(0);
	for (int i = 0; i < numVertices; ++i)
	{
		float theta = 2.0f * 3.14159265359f * float(i) / float(numVertices-2);
		float x = cos(theta);
		float y = sin(theta);
		vertices.push_back(x);
		vertices.push_back(y);
	}

	std::vector<float> colors;
	colors.push_back(1);
	colors.push_back(1);
	colors.push_back(1);
	for (int h = 0; h < numVertices-1; ++h)
	{
		float r, b, g;
		hsvToRgb(h, r, g, b);
		colors.push_back(r);
		colors.push_back(g);
		colors.push_back(b);
	}

	glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
	glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(float), vertices.data(), GL_STATIC_DRAW);

	// Position attribute
	glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	glBindBuffer(GL_ARRAY_BUFFER, colorBuffer);
	glBufferData(GL_ARRAY_BUFFER, colors.size() * sizeof(float), colors.data(), GL_STATIC_DRAW);

	// Color attribute
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
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
	GLint scale_x_loc = glGetUniformLocation(shaderProgram, "scale_x");
	GLint scale_y_loc = glGetUniformLocation(shaderProgram, "scale_y");

	sf::Clock clock;

	float scale_x = 1.0f;
	float scale_y = 1.0f;

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
				case sf::Keyboard::Up:
					scale_y += 0.1f;
					break;
				case sf::Keyboard::Down:
					scale_y = std::max(0.1f, scale_y - 0.1f);
					break;
				case sf::Keyboard::Left:
					scale_x = std::max(0.1f, scale_x - 0.1f);
					break;
				case sf::Keyboard::Right:
					scale_x += 0.1f;
					break;
				}
			}
		}

		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		glUseProgram(shaderProgram);

		glm::mat4 model = glm::mat4(1.0f);
		glm::mat4 view = glm::lookAt(glm::vec3(0.0f, 0.0f, 3.0f), glm::vec3(0.0f, 0.0f, 1.0f), glm::vec3(0.0f, 1.0f, 0.0f));
		glm::mat4 projection = glm::perspective(glm::radians(45.0f), 800.0f / 600.0f, 0.1f, 100.0f);

		glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "model"), 1, GL_FALSE, &model[0][0]);
		glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "view"), 1, GL_FALSE, &view[0][0]);
		glUniformMatrix4fv(glGetUniformLocation(shaderProgram, "projection"), 1, GL_FALSE, &projection[0][0]);
		glUniform1f(scale_x_loc, scale_x);
		glUniform1f(scale_y_loc, scale_y);

		glDrawArrays(GL_TRIANGLE_FAN, 0, numVertices);

		window.display();
	}

	glDeleteVertexArrays(1, &vertexArray);
	glDeleteBuffers(1, &vertexBuffer);
	glDeleteProgram(shaderProgram);

	return 0;
}