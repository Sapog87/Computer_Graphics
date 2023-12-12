#include <SFML/Graphics.hpp>
#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include <cmath>
#include <iostream>
#include <fstream>
#include <sstream>

using namespace std;

struct Position {
	GLfloat x, y, z;
};

struct Normal {
	GLfloat nx, ny, nz;
};

struct TextureCordinate
{
	GLfloat tx, ty;
};

vector <string> split(string str, char separator) {
	vector < string > strings;
	int startIndex = 0, endIndex = 0;
	for (int i = 0; i <= str.size(); i++) {

		if (str[i] == separator || i == str.size()) {
			endIndex = i;
			string temp;
			temp.append(str, startIndex, endIndex - startIndex);
			strings.push_back(temp);
			startIndex = endIndex + 1;
		}
	}
	return strings;
}

bool loadModel(const std::string& filePath, std::vector<float>& vertices, std::vector<float>& textureCoords, int& n) {
	n = 0;
	std::ifstream file(filePath);
	if (!file) {
		std::cerr << "Failed to open file: " << filePath << std::endl;
		return false;
	}

	std::vector<Position> _positions;
	std::vector<Normal> _normals;
	std::vector<TextureCordinate> _texCoords;

	std::string line;
	while (std::getline(file, line)) {
		vector <string> strings = split(line, ' ');
		std::string type = strings[0];

		if (type == "v") {
			_positions.push_back({ std::stof(strings[1]), std::stof(strings[2]),std::stof(strings[3]) });
		}
		else if (type == "vn") {
			_normals.push_back({ std::stof(strings[1]), std::stof(strings[2]),std::stof(strings[3]) });
		}
		else if (type == "vt") {
			_texCoords.push_back({ std::stof(strings[1]), std::stof(strings[2]) });
		}
		else if (type == "f") {
			for (int i = 1; i < strings.size(); i++) {
				auto strs = split(strings[i], '/');
				int v = stoi(strs[0]) - 1;
				int vt = stoi(strs[1]) - 1;
				int vn = stoi(strs[2]) - 1;

				vertices.push_back(_positions[v].x);
				vertices.push_back(_positions[v].y);
				vertices.push_back(_positions[v].z);

				textureCoords.push_back(_texCoords[vt].tx);
				textureCoords.push_back(_texCoords[vt].ty);

				n++;
			}
		}
	}

	return true;
}

const char* vertexShaderSource = R"(
    #version 330 core
    layout (location = 0) in vec3 aPos;
    layout (location = 1) in vec2 aTexCoord;

	out vec2 TexCoord;

    uniform mat4 view;
    uniform mat4 projection;
    uniform mat4 models[7];
	uniform float sizes[7];

    void main()
    {
        mat4 model = models[gl_InstanceID];
		float size = sizes[gl_InstanceID];
        gl_Position = projection * view * model * vec4(aPos * size, 1.0);
		TexCoord = vec2(aTexCoord.x, 1.0f - aTexCoord.y);
    }
)";

const char* fragmentShaderSource = R"(
    #version 330 core

    in vec2 TexCoord;
    out vec4 FragColor;

    uniform sampler2D texture1;

    void main() {
        FragColor = texture(texture1, TexCoord);
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

	GLuint vertexArray, vertexBuffer, textureCoordBuffer, shaderProgram;
	GLuint texture1;

	glGenVertexArrays(1, &vertexArray);
	glGenBuffers(1, &vertexBuffer);
	glGenBuffers(1, &textureCoordBuffer);

	glBindVertexArray(vertexArray);

	std::vector<float> vertices;
	std::vector<float> textureCoords;
	int n;
	if (!loadModel("plant.obj", vertices, textureCoords, n)) {
		std::cerr << "Failed to load obj" << std::endl;
		return -1;
	}

	glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
	glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(float), vertices.data(), GL_STATIC_DRAW);

	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	glBindBuffer(GL_ARRAY_BUFFER, textureCoordBuffer);
	glBufferData(GL_ARRAY_BUFFER, textureCoords.size() * sizeof(float), textureCoords.data(), GL_STATIC_DRAW);

	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(1);

	sf::Image image1;
	if (!image1.loadFromFile("plant.jpg"))
	{
		std::cerr << "Failed to load textures" << std::endl;
		return -1;
	}

	glGenTextures(1, &texture1);
	glBindTexture(GL_TEXTURE_2D, texture1);

	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, image1.getSize().x, image1.getSize().y, 0, GL_RGBA, GL_UNSIGNED_BYTE, image1.getPixelsPtr());

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_BORDER);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_BORDER);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	glBindTexture(GL_TEXTURE_2D, 0);

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

	GLint modelsLoc = glGetUniformLocation(shaderProgram, "models");
	GLint viewLoc = glGetUniformLocation(shaderProgram, "view");
	GLint projectionLoc = glGetUniformLocation(shaderProgram, "projection");
	GLint sizesLoc = glGetUniformLocation(shaderProgram, "sizes");

	sf::Clock clock;

	int r = 3000;
	std::vector<glm::vec3> positions;
	positions.push_back(glm::vec3(0, 0, 0));
	for (int i = 1; i < 7; ++i) {
		float x = static_cast<float>(rand()) / r;
		float y = static_cast<float>(rand()) / r / 10;
		float z = static_cast<float>(rand()) / r;
		positions.push_back(glm::vec3(x, y, z));
	}

	std::vector<float> orbitSpeeds;
	for (int i = 0; i < 7; ++i) {
		orbitSpeeds.push_back(static_cast<float>(rand()) / 10000);
	}

	std::vector<float> sizes;
	sizes.push_back(1.5f);
	for (int i = 1; i < 7; ++i) {
		sizes.push_back(1.0f);
	}

	
	glm::vec3 cameraPos = glm::vec3(0.0f, 5.0f, 20.0f);  // Начальная позиция камеры
	glm::vec3 cameraFront = glm::vec3(0.0f, 0.0f, -1.0f); // Начальное направление взгляда
	glm::vec3 cameraUp = glm::vec3(0.0f, 1.0f, 0.0f);    // Верхний вектор камеры

	float yaw = -90.0f;
	float pitch = 0.0f;

	while (window.isOpen())
	{
		sf::Event event;
		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed) {
				window.close();
			}
			else if (event.type == sf::Event::KeyPressed)
			{
				if (sf::Keyboard::isKeyPressed(sf::Keyboard::W))
					cameraPos += cameraFront;
				if (sf::Keyboard::isKeyPressed(sf::Keyboard::S))
					cameraPos -= cameraFront;
				if (sf::Keyboard::isKeyPressed(sf::Keyboard::A))
					cameraPos -= glm::normalize(glm::cross(cameraFront, cameraUp));
				if (sf::Keyboard::isKeyPressed(sf::Keyboard::D))
					cameraPos += glm::normalize(glm::cross(cameraFront, cameraUp));
			}
			else if (sf::Mouse::isButtonPressed(sf::Mouse::Left)) {
				float sensitivity = 0.1f;
				sf::Vector2i mouseDelta = sf::Mouse::getPosition(window) - sf::Vector2i(window.getSize()) / 2;
				yaw += mouseDelta.x * sensitivity;
				pitch -= mouseDelta.y * sensitivity;

				if (pitch > 89.0f)
					pitch = 89.0f;
				if (pitch < -89.0f)
					pitch = -89.0f;

				glm::vec3 front;
				front.x = cos(glm::radians(yaw)) * cos(glm::radians(pitch));
				front.y = sin(glm::radians(pitch));
				front.z = sin(glm::radians(yaw)) * cos(glm::radians(pitch));
				cameraFront = glm::normalize(front);

				sf::Mouse::setPosition(sf::Vector2i(window.getSize()) / 2, window);
			}
		}

		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		glUseProgram(shaderProgram);

		glm::mat4 projection = glm::perspective(glm::radians(45.0f), 800.0f / 600.0f, 0.1f, 100.0f);
		glm::mat4 view = glm::lookAt(cameraPos, cameraPos + cameraFront, cameraUp);

		std::vector<glm::mat4> modelMatrices;
		modelMatrices.push_back(glm::rotate(glm::mat4(1.0f), glm::radians(clock.getElapsedTime().asSeconds() * 60.0f), glm::vec3(0.0f, 1.0f, 0.0f)));

		for (int i = 1; i < 7; ++i) {
			float orbitRadius = sqrt(positions[i].x * positions[i].x + positions[i].z * positions[i].z);
			float time = clock.getElapsedTime().asSeconds();
			float satelliteX = orbitRadius * std::cos(orbitSpeeds[i] * time);
			float satelliteZ = orbitRadius * std::sin(orbitSpeeds[i] * time);

			glm::mat4 model = glm::mat4(1.0f);
			model = glm::translate(model, glm::vec3(satelliteX, 0, satelliteZ));
			model = glm::translate(model, glm::vec3(0, positions[i].y, 0));
			model = glm::rotate(model, glm::radians(clock.getElapsedTime().asSeconds() * 60.0f), glm::vec3(0.0f, 1.0f, 0.0f));
			modelMatrices.push_back(model);
		}

		glUniformMatrix4fv(modelsLoc, modelMatrices.size(), GL_FALSE, &modelMatrices[0][0][0]);
		glUniformMatrix4fv(viewLoc, 1, GL_FALSE, &view[0][0]);
		glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, &projection[0][0]);
		glUniform1fv(sizesLoc, sizes.size(), &sizes[0]);

		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, texture1);
		glUniform1i(glGetUniformLocation(shaderProgram, "texture1"), 0);

		glDrawArraysInstanced(GL_QUADS, 0, n, 7);

		glBindTexture(GL_TEXTURE_2D, 0);

		window.display();
	}

	glDeleteVertexArrays(1, &vertexArray);
	glDeleteBuffers(1, &vertexBuffer);
	glDeleteBuffers(1, &textureCoordBuffer);
	glDeleteTextures(1, &texture1);
	glDeleteProgram(shaderProgram);

	return 0;
}