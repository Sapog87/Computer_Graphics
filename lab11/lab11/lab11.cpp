#include <iostream>
#include <cstdio>

#include <GL/glew.h>
#include <GL/glu.h>

#include <SFML/Graphics.hpp>
#include <SFML/OpenGL.hpp>

// ID шейдерной программы
GLuint Program;
// ID атрибута
GLint Attrib_vertex;
// ID Vertex Buffer Object
GLuint VBO;

struct Vertex {
	GLfloat x;
	GLfloat y;
	GLfloat r;
	GLfloat g;
	GLfloat b;
	GLfloat a;
};

const char* VertexShaderSource = R"(
#version 330 core
in vec2 coord;
in vec4 color;
out vec4 vertexColor;
void main() {
	gl_Position = vec4(coord * 0.5, 0.0, 1.0);
	vertexColor = color;
}
)";

// Исходный код фрагментного шейдера
const char* FragShaderSource = R"(
#version 330 core
void main() {
	gl_FragColor = vec4(0.5f, 0.0f, 0.0f, 1.0f);
}
)";

const char* FragShaderSource1 = R"(
#version 330 core
uniform vec4 color;
void main() {
	gl_FragColor = color;
}
)";

const char* FragShaderSource2 = R"(
#version 330 core
in vec4 vertexColor;
out vec4 color;
void main() {
	color = vertexColor;
}
)";


void checkOpenGLerror() {
	int error = glGetError();
	if (error != 0)
		std::cout << error << std::endl;
}

void ShaderLog(unsigned int shader)
{
	int infologLen = 0;
	glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infologLen);
	if (infologLen > 1)
	{
		int charsWritten = 0;
		std::vector<char> infoLog(infologLen);
		glGetShaderInfoLog(shader, infologLen, &charsWritten, infoLog.data());
		std::cout << "InfoLog: " << infoLog.data() << std::endl;
	}
}

float getRandom() {
	return static_cast<float>(rand()) / RAND_MAX;
}

void InitVBO() {
	glGenBuffers(1, &VBO);
	Vertex shapes[20] = {
		{ -1.0f, 1.0f, getRandom(), getRandom(), getRandom(), 1.0f},
		{ -1.0f, -1.0f,  getRandom(), getRandom(), getRandom(), 1.0f },
		{ 1.0f, -1.0f, getRandom(), getRandom(), getRandom(), 1.0f },
		{ 1.0f, 1.0f, getRandom(), getRandom(), getRandom(), 1.0f },
		{ 0.0f, 0.0f, getRandom(), getRandom(), getRandom(), 1.0f },
	};

	int k = 5;
	for (int i = 0; i < 10; i++)
	{
		shapes[k] = { (float)cos(20.0f * i * 3.14159 / 180.0f), (float)sin(20.0f * i * 3.14159 / 180.0f), getRandom(), getRandom(), getRandom(), 1.0f };
		k++;
	}

	for (int i = 0; i < 5; i++)
	{
		shapes[k] = { (float)cos(72.0f * i * 3.14159 / 180.0f), (float)sin(72.0f * i * 3.14159 / 180.0f), getRandom(), getRandom(), getRandom(), 1.0f };
		k++;
	}

	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(shapes), shapes, GL_STATIC_DRAW);
	checkOpenGLerror();
}

void InitShader(const char* fragShaderSource) {
	GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &VertexShaderSource, NULL);
	glCompileShader(vShader);
	ShaderLog(vShader);

	GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fShader, 1, &fragShaderSource, NULL);
	glCompileShader(fShader);
	ShaderLog(fShader);

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);
	glLinkProgram(Program);

	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok) {
		std::cout << "error attach shaders \n";
		return;
	}

	const char* attr_name = "coord";
	Attrib_vertex = glGetAttribLocation(Program, attr_name);
	if (Attrib_vertex == -1) {
		std::cout << "could not bind attrib " << attr_name << std::endl;
		return;
	}
	checkOpenGLerror();
}

void Init() {
	InitShader(FragShaderSource);
	InitVBO();
}

void Draw(int prog, int start, int count) {
	glUseProgram(Program);

	GLint colorLocation = glGetUniformLocation(Program, "color");
	if (colorLocation != -1)
		glUniform4f(colorLocation, 1.0f, 0.0f, 0.0f, 1.0f);

	//glEnableVertexAttribArray(Attrib_vertex);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);

	glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), 0);
    glEnableVertexAttribArray(0);

    glVertexAttribPointer(1, 4, GL_FLOAT, GL_FALSE, sizeof(Vertex), (GLvoid*)(2 * sizeof(GLfloat)));
    glEnableVertexAttribArray(1);

	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glDrawArrays(prog, start, count);

	glDisableVertexAttribArray(Attrib_vertex);
	glUseProgram(0);
	checkOpenGLerror();
}


// Освобождение шейдеров
void ReleaseShader() {
	// Передавая ноль, мы отключаем шейдерную программу
	glUseProgram(0);
	// Удаляем шейдерную программу
	glDeleteProgram(Program);
}

// Освобождение буфера
void ReleaseVBO() {
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glDeleteBuffers(1, &VBO);
}

void Release() {
	// Шейдеры
	ReleaseShader();
	// Вершинный буфер
	ReleaseVBO();
}


int main() {
	srand(static_cast<unsigned int>(time(0)));
	sf::Window window(sf::VideoMode(600, 600), "My OpenGL window", sf::Style::Default, sf::ContextSettings(24));
	window.setVerticalSyncEnabled(true);
	window.setActive(true);
	glewInit();
	Init();
	int t = 0;
	int k = 0;
	while (window.isOpen()) {

		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed) { window.close(); }
			else if (event.type == sf::Event::Resized) { glViewport(0, 0, event.size.width, event.size.height); }
			else if (event.type == sf::Event::KeyPressed)
			{
				if (event.key.code == sf::Keyboard::Key::Up)
					t++;
				else if (event.key.code == sf::Keyboard::Key::Right)
					k++;
			}
		}
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		switch (k % 3)
		{
		case 0:
			InitShader(FragShaderSource);
			break;
		case 1:
			InitShader(FragShaderSource1);
			break;
		case 2:
			InitShader(FragShaderSource2);
			break;
		default:
			break;
		}

		switch (t % 3)
		{
		case 0:
			Draw(GL_QUADS, 0, 4);
			break;
		case 1:
			Draw(GL_TRIANGLE_FAN, 4, 11);
			break;
		case 2:
			Draw(GL_POLYGON, 15, 5);
			break;
		default:
			break;
		}
		window.display();
	}
	Release();
	return 0;
}
