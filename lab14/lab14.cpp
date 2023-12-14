#include <iostream>
#include <SFML/Graphics.hpp>
#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include "glm/gtc/type_ptr.hpp"
#include <cmath>
#include <fstream>
#include <sstream>

using namespace std;

struct Model {
	vector<float> vertices;
	vector<float> textureCoords;
	vector<float> normals;
	int n = 0;
	int p = 0;
	int type = GL_QUADS;
};

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
	vector<string> strings;
	int startIndex = 0, endIndex = 0;
	for (int i = 0; i <= str.size(); i++) {

		if (str[i] == separator || i == str.size()) {
			endIndex = i;
			string temp;
			temp.append(str, startIndex, endIndex - startIndex);
			if (!temp.empty())
				strings.push_back(temp);
			startIndex = endIndex + 1;
		}
	}
	return strings;
}

bool loadModel(const std::string& filePath, Model& obj) {
	obj = Model();
	int n = 0;
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
		if (!line.empty()) {
			vector<string> strings = split(line, ' ');
			std::string type = strings[0];

			if (type == "v") {
				_positions.push_back({ std::stof(strings[1]), std::stof(strings[2]), std::stof(strings[3]) });
			}
			else if (type == "vn") {
				_normals.push_back({ std::stof(strings[1]), std::stof(strings[2]), std::stof(strings[3]) });
			}
			else if (type == "vt") {
				_texCoords.push_back({ std::stof(strings[1]), std::stof(strings[2]) });
			}
			else if (type == "f") {
				if (strings.size() - 1 == 3)
					obj.type = GL_TRIANGLES;

				for (int i = 1; i < strings.size(); i++) {
					auto strs = split(strings[i], '/');

					int v = stoi(strs[0]) - 1;
					int vt = stoi(strs[1]) - 1;
					int vn = stoi(strs[2]) - 1;

					obj.vertices.push_back(_positions[v].x);
					obj.vertices.push_back(_positions[v].y);
					obj.vertices.push_back(_positions[v].z);

					obj.normals.push_back(_normals[vn].nx);
					obj.normals.push_back(_normals[vn].ny);
					obj.normals.push_back(_normals[vn].nz);

					obj.textureCoords.push_back(_texCoords[vt].tx);
					obj.textureCoords.push_back(_texCoords[vt].ty);

					obj.n++;
				}
			}
		}
	}

	return true;
}

void checkOpenGLerror() {
	int error = glGetError();
	if (error != 0)
		std::cout << error << std::endl;
}

const char* vertexShaderSource = R"(
	#version 330 core
	layout (location = 0) in vec3 aPos;
	layout (location = 1) in vec2 aTexCoords;
	layout (location = 2) in vec3 aNormal;
	
	out vec3 FragPos;
	out vec3 Normal;
	out vec2 TexCoords;
	
	uniform mat4 model;
	uniform mat4 view;
	uniform mat4 projection;
	
	void main()
	{
	    FragPos = vec3(model * vec4(aPos, 1.0));
	    Normal = mat3(transpose(inverse(model))) * aNormal;  
	    TexCoords = aTexCoords;
	    
	    gl_Position = projection * view * vec4(FragPos, 1.0);
	}
)";

const char* fragmentShaderSource = R"(
	#version 330 core
	out vec4 FragColor;
	
	struct Material {
	    sampler2D diffuse;
	    sampler2D specular;
	    float shininess;
	}; 
	
	struct DirLight {
	    vec3 direction;
		
	    vec3 ambient;
	    vec3 diffuse;
	    vec3 specular;
	};
	
	struct PointLight {
	    vec3 position;
	    
	    float constant;
	    float linear;
	    float quadratic;
		
	    vec3 ambient;
	    vec3 diffuse;
	    vec3 specular;
	};
	
	struct SpotLight {
	    vec3 position;
	    vec3 direction;
	    float cutOff;
	    float outerCutOff;
	  
	    float constant;
	    float linear;
	    float quadratic;
	  
	    vec3 ambient;
	    vec3 diffuse;
	    vec3 specular;       
	};
	
	in vec3 FragPos;
	in vec3 Normal;
	in vec2 TexCoords;
	
	uniform vec3 viewPos;
	uniform DirLight dirLight;
	uniform PointLight pointLight;
	uniform SpotLight spotLight;
	uniform Material material;
	uniform sampler2D ourTexture;
	uniform int num;
	
	vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
	vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
	vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
	vec3 ApplyToonShading(vec3 color);
	vec3 CalculateMinnaert(PointLight light, vec3 normal, vec3 fragPos, float minnaertFactor);
	
	void main()
	{    
	    vec3 norm = normalize(Normal);
	    vec3 viewDir = normalize(viewPos - FragPos);
		vec3 result;

		if (num == 0 || num == 1) {
			result += CalcPointLight(pointLight, norm, FragPos, viewDir);    
		}
		else if (num == 2 || num == 3) {
			result += CalcPointLight(pointLight, norm, FragPos, viewDir);  
			result = ApplyToonShading(result);
		}
		else {
			result = CalculateMinnaert(pointLight, norm, FragPos, 0.5f);
		}
	    
	    // directional lighting
	    //result = CalcDirLight(dirLight, norm, viewDir);

	    // point lights
	    //result += CalcPointLight(pointLight, norm, FragPos, viewDir);    

	    // spot light
	    //result += CalcSpotLight(spotLight, norm, FragPos, viewDir);    

	    
	    FragColor = vec4(result, 1.0);
	}
	
	// calculates the color when using a directional light.
	vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
	{
	    vec3 lightDir = normalize(-light.direction);

	    // diffuse shading
	    float diff = max(dot(normal, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, normal);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // combine results
	    vec3 ambient = light.ambient * vec3(texture(ourTexture, TexCoords));
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, TexCoords));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, TexCoords));

	    return (ambient + diffuse + specular);
	}
	
	// calculates the color when using a point light.
	vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
	{
	    vec3 lightDir = normalize(light.position - fragPos);

	    // diffuse shading
	    float diff = max(dot(normal, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, normal);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // attenuation
	    float distance = length(light.position - fragPos);
	    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

	    // combine results
	    vec3 ambient = light.ambient * vec3(texture(ourTexture, TexCoords));
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, TexCoords));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, TexCoords));

	    ambient *= attenuation;
	    diffuse *= attenuation;
	    specular *= attenuation;

	    return (ambient + diffuse + specular);
	}
	
	// calculates the color when using a spot light.
	vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
	{
	    vec3 lightDir = normalize(light.position - fragPos);

	    // diffuse shading
	    float diff = max(dot(normal, lightDir), 0.0);

	    // specular shading
	    vec3 reflectDir = reflect(-lightDir, normal);
	    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	    // attenuation
	    float distance = length(light.position - fragPos);
	    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    

	    // spotlight intensity
	    float theta = dot(lightDir, normalize(-light.direction)); 
	    float epsilon = light.cutOff - light.outerCutOff;
	    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);

	    // combine results
	    vec3 ambient = light.ambient * vec3(texture(ourTexture, TexCoords));
	    vec3 diffuse = light.diffuse * diff * vec3(texture(ourTexture, TexCoords));
	    vec3 specular = light.specular * spec * vec3(texture(ourTexture, TexCoords));

	    ambient *= attenuation * intensity;
	    diffuse *= attenuation * intensity;
	    specular *= attenuation * intensity;

	    return (ambient + diffuse + specular);
	}

	vec3 ApplyToonShading(vec3 color)
	{
	    float intensity = 0.5 * (color.r + color.g + color.b);
	
	    float levels[3];
	    levels[0] = 0.15; // Dark threshold
	    levels[1] = 0.5; // Mid threshold
	    levels[2] = 0.9; // Highlight threshold
	
	    if (intensity < levels[0])
	        intensity = 0.0;
	    else if (intensity < levels[1])
	        intensity = 0.5;
	    else if (intensity < levels[2])
	        intensity = 0.9;
	    else
	        intensity = 1.0;
	
	    color = vec3(intensity) * color;
	
	    return color;
	}

	vec3 CalculateMinnaert(PointLight light, vec3 normal, vec3 fragPos, float minnaertFactor)
	{
		vec3 lightDir = normalize(light.position - fragPos);

	    float minnaertTerm = pow(max(dot(normal, normalize(lightDir)), 0.0), minnaertFactor);
	    
	    vec3 color = vec3(texture(ourTexture, TexCoords)) * minnaertTerm;
	
	    return color;
	}
)";

void init(GLuint* vertexArray, GLuint* vertexBuffer, GLuint* textureCoordBuffer, GLuint* normalBuffer, GLuint* texture, string path_to_image, string path_to_obj, Model& obj)
{
	loadModel(path_to_obj, obj);

	glBindVertexArray(*vertexArray);

	glBindBuffer(GL_ARRAY_BUFFER, *vertexBuffer);
	glBufferData(GL_ARRAY_BUFFER, obj.vertices.size() * sizeof(float), obj.vertices.data(), GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, *textureCoordBuffer);
	glBufferData(GL_ARRAY_BUFFER, obj.textureCoords.size() * sizeof(float), obj.textureCoords.data(), GL_STATIC_DRAW);

	glBindBuffer(GL_ARRAY_BUFFER, *normalBuffer);
	glBufferData(GL_ARRAY_BUFFER, obj.normals.size() * sizeof(float), obj.normals.data(), GL_STATIC_DRAW);

	sf::Image image;
	if (!image.loadFromFile(path_to_image))
	{
		std::cerr << "Failed to load texture" << std::endl;
		return;
	}

	glBindTexture(GL_TEXTURE_2D, *texture);

	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, image.getSize().x, image.getSize().y, 0, GL_RGBA, GL_UNSIGNED_BYTE, image.getPixelsPtr());

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
}

bool draw(GLuint* vertexArray, GLuint* vertexBuffer, GLuint* textureCoordBuffer, GLuint* normalBuffer, GLuint* shaderProgram, GLuint* texture, Model obj, glm::vec3 offset, int num)
{
	glUseProgram(*shaderProgram);

	GLint modelLoc = glGetUniformLocation(*shaderProgram, "model");
	GLint textureLoc = glGetUniformLocation(*shaderProgram, "ourTexture");
	GLint numLoc = glGetUniformLocation(*shaderProgram, "num");

	glBindVertexArray(*vertexArray);

	glBindBuffer(GL_ARRAY_BUFFER, *vertexBuffer);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	glBindBuffer(GL_ARRAY_BUFFER, *textureCoordBuffer);
	glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(1);

	glBindBuffer(GL_ARRAY_BUFFER, *normalBuffer);
	glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(2);

	glm::mat4 model = glm::translate(glm::mat4(1.0f), offset);

	glUniform1i(numLoc, num);

	glUniformMatrix4fv(modelLoc, 1, GL_FALSE, &model[0][0]);
	glUniform1i(textureLoc, 0);

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, *texture);

	glDrawArrays(obj.type, 0, obj.n);

	glUseProgram(0);

	return true;
}

void createProg(GLuint& shaderProgram, const char* vertexShaderSource, const char* fragmentShaderSource)
{
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
}

void light(vector<glm::vec3> pointLightPositions, GLuint* shaderProgram) {
	glUseProgram(*shaderProgram);

	glm::vec3 view = glm::vec3(0.0f, 0.0f, 10.0f);
	glm::vec3 front = glm::vec3(0.0f, 0.0f, -1.0f);


	glUniform3f(glGetUniformLocation(*shaderProgram, "viewPos"), view.x, view.y, view.z);
	glUniform1f(glGetUniformLocation(*shaderProgram, "material.shininess"), 30.0f);


	glUniform3f(glGetUniformLocation(*shaderProgram, "dirLight.direction"), -0.2f, -1.0f, -0.3f);
	glUniform3f(glGetUniformLocation(*shaderProgram, "dirLight.ambient"), 0.05f, 0.05f, 0.05f);
	glUniform3f(glGetUniformLocation(*shaderProgram, "dirLight.diffuse"), 0.4f, 0.4f, 0.4f);
	glUniform3f(glGetUniformLocation(*shaderProgram, "dirLight.specular"), 0.5f, 0.5f, 0.5f);


	glUniform3f(glGetUniformLocation(*shaderProgram, "pointLight.position"), pointLightPositions[0].x, pointLightPositions[0].y, pointLightPositions[0].z);
	glUniform3f(glGetUniformLocation(*shaderProgram, "pointLight.ambient"), 0.05f, 0.05f, 0.05f);
	glUniform3f(glGetUniformLocation(*shaderProgram, "pointLight.diffuse"), 1.0f, 1.0f, 1.0f);
	glUniform3f(glGetUniformLocation(*shaderProgram, "pointLight.specular"), 1.0f,1.0f, 1.0f);
	glUniform1f(glGetUniformLocation(*shaderProgram, "pointLight.constant"), 1.0f);
	glUniform1f(glGetUniformLocation(*shaderProgram, "pointLight.linear"), 0.0f);
	glUniform1f(glGetUniformLocation(*shaderProgram, "pointLight.quadratic"), 0.0f);


	glUniform3f(glGetUniformLocation(*shaderProgram, "spotLight.position"), view.x, view.y, view.z);
	glUniform3f(glGetUniformLocation(*shaderProgram, "spotLight.direction"), front.x, front.y, front.z);
	glUniform3f(glGetUniformLocation(*shaderProgram, "spotLight.ambient"), 0.0f, 0.0f, 0.0f);
	glUniform3f(glGetUniformLocation(*shaderProgram, "spotLight.diffuse"), 1.0f, 1.0f, 1.0f);
	glUniform3f(glGetUniformLocation(*shaderProgram, "spotLight.specular"), 1.0f, 1.0f, 1.0f);
	glUniform1f(glGetUniformLocation(*shaderProgram, "spotLight.constant"), 1.0f);
	glUniform1f(glGetUniformLocation(*shaderProgram, "spotLight.linear"), 0.007f);
	glUniform1f(glGetUniformLocation(*shaderProgram, "spotLight.quadratic"), 0.0002f);
	glUniform1f(glGetUniformLocation(*shaderProgram, "spotLight.cutOff"), glm::cos(glm::radians(12.5f)));
	glUniform1f(glGetUniformLocation(*shaderProgram, "spotLight.outerCutOff"), glm::cos(glm::radians(12.5f)));


	glUseProgram(0);
}

int main()
{
	srand(time(0));

	sf::ContextSettings settings;
	settings.depthBits = 24;
	settings.stencilBits = 8;

	sf::RenderWindow window(sf::VideoMode(800, 600), "OpenGL", sf::Style::Default, settings);

	glewExperimental = GL_TRUE;
	glewInit();

	glEnable(GL_DEPTH_TEST);

	GLuint shaderProgram;

	GLuint vertexArray[5];
	GLuint textureCoordBuffer[5];
	GLuint texture[5];
	GLuint vertexBuffer[5];
	GLuint normalBuffer[5];

	glGenBuffers(5, vertexBuffer);
	glGenVertexArrays(5, vertexArray);
	glGenBuffers(5, textureCoordBuffer);
	glGenTextures(5, texture);

	createProg(shaderProgram, vertexShaderSource, fragmentShaderSource);

	Model objs[5];
	init(&vertexArray[0], &vertexBuffer[0], &textureCoordBuffer[0], &normalBuffer[0], &texture[0], "plant.jpg", "plant.obj", objs[0]);
	init(&vertexArray[1], &vertexBuffer[1], &textureCoordBuffer[1], &normalBuffer[1], &texture[1], "plant.jpg", "plant.obj", objs[1]);
	init(&vertexArray[2], &vertexBuffer[2], &textureCoordBuffer[2], &normalBuffer[2], &texture[2], "plant.jpg", "plant.obj", objs[2]);
	init(&vertexArray[3], &vertexBuffer[3], &textureCoordBuffer[3], &normalBuffer[3], &texture[3], "plant.jpg", "plant.obj", objs[3]);
	init(&vertexArray[4], &vertexBuffer[4], &textureCoordBuffer[4], &normalBuffer[4], &texture[4], "plant.jpg", "plant.obj", objs[4]);

	sf::Clock clock;

	GLint viewLoc = glGetUniformLocation(shaderProgram, "view");
	GLint projectionLoc = glGetUniformLocation(shaderProgram, "projection");

	std::vector<glm::vec3> offsets = {
		glm::vec3(0.0f,  0.0f,  0.0f),
		glm::vec3(5.0f,  0.0f, -5.0f),
		glm::vec3(-5.0f,  0.0f, -10.0f),
		glm::vec3(10.0f,  0.0f, -15.0f),
		glm::vec3(-10.0f,  0.0f, -20.0f),
	};

	//std::vector<glm::vec3> offsets = {
	//	glm::vec3(0.0f,  0.0f,  0.0f),
	//	glm::vec3(0.0f,  0.0f, -5.0f),
	//	glm::vec3(-1.5f, -2.2f, -2.5f),
	//	glm::vec3(-3.8f, -2.0f, -12.3f),
	//	glm::vec3(2.4f, -0.4f, -3.5f),
	//};

	std::vector<glm::vec3> pointLightPositions = {
		glm::vec3(0.0f,  0.0f, 10.0f),
	};

	glm::vec3 cameraPos = glm::vec3(0.0f, 5.0f, 20.0f);
	glm::vec3 cameraFront = glm::vec3(0.0f, 0.0f, -1.0f);
	glm::vec3 cameraUp = glm::vec3(0.0f, 1.0f, 0.0f);

	float yaw = -90.0f;
	float pitch = 0.0f;

	while (window.isOpen())
	{
		sf::Event event;
		while (window.pollEvent(event))
		{
			if (event.type == sf::Event::Closed)
			{
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

		glm::mat4 view = glm::lookAt(cameraPos, cameraPos + cameraFront, cameraUp);
		glm::mat4 projection = glm::perspective(glm::radians(45.0f), 800.0f / 600.0f, 0.1f, 100.0f);

		glUniformMatrix4fv(viewLoc, 1, GL_FALSE, &view[0][0]);
		glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, &projection[0][0]);

		light(pointLightPositions, &shaderProgram);

		for (int i = 0; i < 5; i++)
			draw(&vertexArray[i], &vertexBuffer[i], &textureCoordBuffer[i], &normalBuffer[i], &shaderProgram, &texture[i], objs[i], offsets[i], i);

		checkOpenGLerror();

		window.display();
	}

	glDeleteVertexArrays(5, vertexArray);
	glDeleteBuffers(5, vertexBuffer);
	glDeleteBuffers(5, textureCoordBuffer);
	glDeleteTextures(5, texture);
	glDeleteProgram(shaderProgram);

	return 0;
}