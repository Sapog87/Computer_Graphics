#pragma once

#include <iostream>
#include <GL/glew.h>

#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>

#include <glm/glm.hpp>
#include <glm/gtc/type_ptr.hpp>
#include <glm/gtc/matrix_transform.hpp>

#include<SOIL/SOIL.h> 

#define SCREEN_WIDTH 800
#define SCREEN_HEIGHT 800

void checkOpenGLerror();
void checkOpenGLerror() {
	GLenum err;
	if ((err = glGetError()) != GL_NO_ERROR)
	{
		std::cout << "OpenGl error!: " << err << std::endl;
	}
}