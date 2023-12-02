#include <iostream>
#include <SFML/Graphics.hpp>
#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"

const char* vertexShaderSource = R"(
    #version 330 core
    layout (location = 0) in vec3 aPos;
    layout (location = 1) in vec2 aTexCoord;
    layout (location = 2) in vec3 aColor;

    out vec2 TexCoord;
    out vec3 ourColor;

    uniform mat4 model;
    uniform mat4 view;
    uniform mat4 projection;

    void main()
    {
        gl_Position = projection * view * model * vec4(aPos, 1.0);
        TexCoord = aTexCoord;
        ourColor = aColor;
    }
)";

const char* fragmentShaderSource = R"(
    #version 330 core

    in vec2 TexCoord;
    out vec4 FragColor;

    uniform sampler2D texture1;
    uniform sampler2D texture2;
    uniform float coef;

    void main() {
        vec4 color1 = texture(texture1, TexCoord);
        vec4 color2 = texture(texture2, TexCoord);

        FragColor = mix(color1, color2, coef);
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
    GLuint texture1, texture2;

    glGenVertexArrays(1, &vertexArray);
    glGenBuffers(1, &vertexBuffer);
    glGenBuffers(1, &textureCoordBuffer);

    glBindVertexArray(vertexArray);

    float vertices[] = {
        // Front face
        -0.5f, -0.5f, 0.5f, 1.0f, 0.0f, 0.0f,  // Vertex 1
        0.5f, -0.5f, 0.5f, 0.0f, 1.0f, 0.0f,   // Vertex 2
        0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,    // Vertex 3
        -0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 0.0f,  // Vertex 4

        // Back face
        -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // Vertex 5
        0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,  // Vertex 6
        0.5f, 0.5f, -0.5f, 0.0f, 1.0f, 0.0f,   // Vertex 7
        -0.5f, 0.5f, -0.5f, 1.0f, 0.0f, 0.0f,  // Vertex 8

        // Right face
        0.5f, -0.5f, 0.5f, 0.0f, 1.0f, 0.0f,   // Vertex 2
        0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,  // Vertex 6
        0.5f, 0.5f, -0.5f, 0.0f, 1.0f, 0.0f,   // Vertex 7
        0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,   // Vertex 3

        // Left face
        -0.5f, -0.5f, 0.5f, 1.0f, 0.0f, 0.0f,  // Vertex 1
        -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // Vertex 5
        -0.5f, 0.5f, -0.5f, 1.0f, 0.0f, 0.0f,  // Vertex 8
        -0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 0.0f,   // Vertex 4

        // Top face
        -0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 0.0f,   // Vertex 4
        0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,   // Vertex 3
        0.5f, 0.5f, -0.5f, 0.0f, 1.0f, 0.0f,   // Vertex 7
        -0.5f, 0.5f, -0.5f, 1.0f, 0.0f, 0.0f,  // Vertex 8

        // Bottom face
        -0.5f, -0.5f, 0.5f, 1.0f, 0.0f, 0.0f,  // Vertex 1
        0.5f, -0.5f, 0.5f, 0.0f, 1.0f, 0.0f,  // Vertex 2
        0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,  // Vertex 6
        -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // Vertex 5
    };

    float textureCoords[] = {
        // Texture coordinates for front face
        0.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f,

        // Texture coordinates for back face
        0.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f,

        // Texture coordinates for right face
        0.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f,

        // Texture coordinates for left face
        0.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f,

        // Texture coordinates for top face
        0.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f,

        // Texture coordinates for bottom face
        0.0f, 0.0f,
        1.0f, 0.0f,
        1.0f, 1.0f,
        0.0f, 1.0f
    };

    glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);
    glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

    // Position attribute
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(0);

    // Color attribute
    glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(float), (void*)(3 * sizeof(float)));
    glEnableVertexAttribArray(2);

    glBindBuffer(GL_ARRAY_BUFFER, textureCoordBuffer);
    glBufferData(GL_ARRAY_BUFFER, sizeof(textureCoords), textureCoords, GL_STATIC_DRAW);

    // Texture coordinate attribute
    glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(float), (void*)0);
    glEnableVertexAttribArray(1);

    sf::Image image1, image2;
    if (!image1.loadFromFile("image.jpg") || !image2.loadFromFile("image.png"))
    {
        std::cerr << "Failed to load textures" << std::endl;
        return -1;
    }

    glGenTextures(1, &texture1);
    glBindTexture(GL_TEXTURE_2D, texture1);

    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, image1.getSize().x, image1.getSize().y, 0, GL_RGBA, GL_UNSIGNED_BYTE, image1.getPixelsPtr());

    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

    glGenTextures(1, &texture2);
    glBindTexture(GL_TEXTURE_2D, texture2);

    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, image2.getSize().x, image2.getSize().y, 0, GL_RGBA, GL_UNSIGNED_BYTE, image2.getPixelsPtr());

    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

    glBindTexture(GL_TEXTURE_2D, 0); // Unbind the textures

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
    GLint coefLoc = glGetUniformLocation(shaderProgram, "coef");

    sf::Clock clock;

    glm::float32 coef = 0.5;

    while (window.isOpen())
    {
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
                window.close();
            else if (event.type == sf::Event::KeyPressed)
            {
                switch (event.key.code)
                {
                case sf::Keyboard::Up:
                    coef += 0.05f;
                    break;
                case sf::Keyboard::Down:
                    coef -= 0.05f;
                    break;
                }
                coef = glm::clamp(coef, 0.0f, 1.0f);
            }
        }

        glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        glUseProgram(shaderProgram);

        glm::mat4 model = glm::mat4(1.0f);
        model = glm::rotate(model, glm::radians(clock.getElapsedTime().asSeconds() * 30.0f), glm::vec3(1.0f, 0.0f, 0.0f));
        model = glm::rotate(model, glm::radians(clock.getElapsedTime().asSeconds() * 30.0f), glm::vec3(0.0f, 1.0f, 0.0f));
        model = glm::rotate(model, glm::radians(clock.getElapsedTime().asSeconds() * 30.0f), glm::vec3(0.0f, 0.0f, 1.0f));

        glm::mat4 view = glm::lookAt(glm::vec3(0.0f, 0.0f, 3.0f), glm::vec3(0.0f, 0.0f, 0.0f), glm::vec3(0.0f, 1.0f, 0.0f));
        glm::mat4 projection = glm::perspective(glm::radians(45.0f), 800.0f / 600.0f, 0.1f, 100.0f);

        glUniformMatrix4fv(modelLoc, 1, GL_FALSE, &model[0][0]);
        glUniformMatrix4fv(viewLoc, 1, GL_FALSE, &view[0][0]);
        glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, &projection[0][0]);
        glUniform1f(coefLoc, coef);

        glActiveTexture(GL_TEXTURE0);
        glBindTexture(GL_TEXTURE_2D, texture1);
        glUniform1i(glGetUniformLocation(shaderProgram, "texture1"), 0);

        glActiveTexture(GL_TEXTURE1);
        glBindTexture(GL_TEXTURE_2D, texture2);
        glUniform1i(glGetUniformLocation(shaderProgram, "texture2"), 1);

        glDrawArrays(GL_QUADS, 0, 24);

        glBindTexture(GL_TEXTURE_2D, 0);

        window.display();
    }

    glDeleteVertexArrays(1, &vertexArray);
    glDeleteBuffers(1, &vertexBuffer);
    glDeleteBuffers(1, &textureCoordBuffer);
    glDeleteTextures(1, &texture1);
    glDeleteTextures(1, &texture2);
    glDeleteProgram(shaderProgram);

    return 0;
}