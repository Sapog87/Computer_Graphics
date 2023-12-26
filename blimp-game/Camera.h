#pragma once

#include "headers.h"
#include "Shader.h"

const glm::vec3 POSITION = glm::vec3(0.0f, 0.0f, 50.0f);
const glm::vec3 WORLD_UP = glm::vec3(0.0f, 1.0f, 0.0f);
const float YAW = -90.0f;
const float PITCH = 0.0f;
const float SPEED = 5.0f;
const float SENSITIVITY = 0.1f;
const float ZOOM = 45.0f;

class Camera
{
	void updateVectors()
	{
		// new Front vector
		glm::vec3 front;
		front.x = cos(glm::radians(Yaw)) * cos(glm::radians(Pitch));
		front.y = sin(glm::radians(Pitch));
		front.z = sin(glm::radians(Yaw)) * cos(glm::radians(Pitch));
		Front = glm::normalize(front);

		Right = glm::normalize(glm::cross(Front, WorldUp)); 
		Up = glm::normalize(glm::cross(Right, Front));
	}



	float Yaw;
	float Pitch;

	float MovementSpeed = SPEED;
	float MouseSensitivity = SENSITIVITY;
	float Zoom = ZOOM;

public:
	glm::vec3 Position;
	glm::vec3 Front;
	glm::vec3 Up;
	glm::vec3 Right;
	glm::vec3 WorldUp;

	enum Camera_Movement {
		FORWARD,
		LEFT,
		BACKWARD,
		RIGHT,
		UP,
		DOWN,
	};

	Camera(glm::vec3 position = POSITION, glm::vec3 worldUp = WORLD_UP, float yaw = YAW, float pitch = PITCH) {
		Position = position;
		WorldUp = worldUp;
		Yaw = yaw;
		Pitch = pitch;
		updateVectors();
	}

	inline glm::mat4 GetViewMatrix() const
	{
		return glm::lookAt(Position, Position + Front, Up);
	}

	inline glm::mat4 GetProjectionMatrix() const
	{
		return glm::perspective(glm::radians(Zoom), (float)SCREEN_WIDTH / (float)SCREEN_HEIGHT, 0.1f, 1000.0f);
	}

	void UpdateUniforms(const Shader* s) const {
		s->use();
		
		s->SetMat4("view",GetViewMatrix());
		checkOpenGLerror();
		s->SetMat4("projection",GetProjectionMatrix());
		
		checkOpenGLerror();
		glUseProgram(0);
	}


	void ProcessKeyboard(Camera_Movement direction, float deltaTime)
	{
		float velocity = MovementSpeed * deltaTime;
		switch (direction)
		{
		case Camera::FORWARD:
			Position += Front * velocity;
			break;
		case Camera::LEFT:
			Position -= Right * velocity;
			break;
		case Camera::BACKWARD:
			Position -= Front * velocity;
			break;
		case Camera::RIGHT:
			Position += Right * velocity;
			break;
		case Camera::UP:
			Position += Up * velocity;
			break;
		case Camera::DOWN:
			Position -= Up* velocity;
			break;
		default:
			break;
		}
		
	}

	void ProcessRotation(float pitch=0,float yaw=0) {
		Yaw += yaw;
		Pitch += pitch;
		Pitch=glm::clamp(pitch, -89.0f, 89.0f);
		updateVectors();
	}


};