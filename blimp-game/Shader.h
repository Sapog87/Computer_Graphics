#pragma once
#include"Headers.h"
#include<sstream>
#include<fstream>

using namespace std;

class Shader {
	void checkCompileErrors(GLuint shader, string type)
	{
		GLint success;
		GLchar infoLog[1024];
		if (type != "PROGRAM")
		{
			glGetShaderiv(shader, GL_COMPILE_STATUS, &success);
			if (!success)
			{
				glGetShaderInfoLog(shader, 1024, NULL, infoLog);
				cout << "compilation error: " << type << "\n" << infoLog << "\n -- --------------------------------------------------- -- " << endl;
			}
		}
		else
		{
			glGetProgramiv(shader, GL_LINK_STATUS, &success);
			if (!success)
			{
				glGetProgramInfoLog(shader, 1024, NULL, infoLog);
				cout << "linking error: " << type << "\n" << infoLog << "\n -- --------------------------------------------------- -- " << endl;
			}
		}
	}
public:


	void getUniformLocation(const string& name, GLint& pos) const {
		auto _pos = glGetUniformLocation(ID, name.c_str());
		if (_pos == -1)
		{
			cout << "could not bind uniform " << name << endl;
			return;
		}
		pos = _pos;
	}

	GLuint ID;
	void Release() {
		glUseProgram(0);
		glDeleteProgram(ID);
	
	}
	Shader() {}
	Shader(const string& vertex_path, const string& frag_path) {
		string vertex_code;
		string fragment_code;

		//reading source code
		try {
			stringstream vShaderStream, fShaderStream;
			ifstream vShaderSource(vertex_path); //vertex
			if (!vShaderSource.is_open()) {
				throw exception("vertex shader code can't be oppened");
			}
			vShaderStream << vShaderSource.rdbuf();
			vShaderSource.close();

			ifstream fShaderSource(frag_path); //fragment
			if (!fShaderSource.is_open()) {
				throw exception("fragment shader code can't be oppened");
			}
			fShaderStream << fShaderSource.rdbuf();
			fShaderSource.close();

			vertex_code = vShaderStream.str();
			fragment_code = fShaderStream.str();

		}


		catch (ifstream::failure& e) {
			cerr << "can't read shader file" << e.what() << endl;
		}


		const char* v_code = vertex_code.c_str();
		const char* f_code = fragment_code.c_str();

		GLuint vert;
		GLuint frag;

		//vertex
		vert = glCreateShader(GL_VERTEX_SHADER);
		glShaderSource(vert, 1, &v_code, NULL);
		glCompileShader(vert);
		checkCompileErrors(vert,"VERTEX");

		//fragment
		frag = glCreateShader(GL_FRAGMENT_SHADER);
		glShaderSource(frag, 1, &f_code, NULL);
		glCompileShader(frag);
		checkCompileErrors(frag, "FRAGMENT");

		//shader program
		ID = glCreateProgram();
		glAttachShader(ID, vert);
		glAttachShader(ID, frag);
		glLinkProgram(ID);
		checkCompileErrors(ID, "PROGRAM");
		glDeleteShader(vert);
		glDeleteShader(frag);

	}


	inline void use() const {
		glUseProgram(ID);
	}

	void SetFloat(const string& name, float value) const
	{
		GLint pos;
		getUniformLocation(name, pos);
		glUniform1f(pos, value);
	}

	void SetVec3(const string& name, const glm::vec3& value) const
	{
		GLint pos;
		getUniformLocation(name, pos);
		glUniform3fv(pos, 1, &value[0]);
	}

	void SetVec4(const std::string& name, const glm::vec4& value) const
	{
		GLint pos;
		getUniformLocation(name, pos);
		glUniform4fv(pos, 1, &value[0]);
	}


	void SetMat3(const string& name, const glm::mat3& mat) const
	{
		GLint pos;
		getUniformLocation(name, pos);
		glUniformMatrix3fv(pos, 1, GL_FALSE, &mat[0][0]);
	}

	void SetMat4(const string& name, const glm::mat4& mat) const
	{
		GLint pos;
		getUniformLocation(name, pos);
		glUniformMatrix4fv(pos, 1, GL_FALSE, &mat[0][0]);
	}
};