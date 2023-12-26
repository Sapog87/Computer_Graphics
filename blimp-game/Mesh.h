#pragma once
#include"Headers.h"
#include<iostream>
#include<string>
#include<sstream>
#include<fstream>
#include<vector>
#include "Shader.h"
using namespace std;


static vector<string> split(const string& s, char delim) {
	stringstream ss(s);
	string item;
	vector<string> elems;
	while (getline(ss, item, delim)) {
		if (item.empty()) continue;
		elems.push_back(item);

	}
	return elems;
}


class Mesh {

	vector<float> vertices;
	GLuint VBO;
	GLuint VAO;

	void parse_file(const string& path) {
		try {
			ifstream file(path);
			if (!file.is_open()) {
				throw exception("obj can't be oppened");
			}

			vector<vector<float>> v;
			vector<vector<float>> vn;
			vector<vector<float>> vt;

			string line;

			while (getline(file, line)) {
				istringstream iss(line);
				string type;
				iss >> type;
				if (type == "v") {
					auto vertex = split(line, ' ');
					vector<float> cv{};
					for (size_t j = 1; j < vertex.size(); j++)
					{
						cv.push_back(stof(vertex[j]));
					}
					v.push_back(cv);

				}
				else if (type == "vn") {
					auto normale = split(line, ' ');
					vector<float> cvn{};
					for (size_t j = 1; j < normale.size(); j++)
					{
						cvn.push_back(stof(normale[j]));
					}
					vn.push_back(cvn);
				}
				else if (type == "vt") {
					auto texture = split(line, ' ');
					vector<float> cvt{};
					for (size_t j = 1; j < texture.size(); j++)
					{
						cvt.push_back(stof(texture[j]));
					}
					vt.push_back(cvt);
				}
				else if (type == "f") {

					auto splitted = split(line, ' ');
						for (size_t i = 1; i < splitted.size(); i++)
						{
							auto triplet = split(splitted[i], '/');
							int positionIndex = stoi(triplet[0]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(v[positionIndex][j]);
							}
							int normaleIndex = stoi(triplet[2]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(vn[normaleIndex][j]);
							}
							int textureIndex = stoi(triplet[1]) - 1;
							for (int j = 0; j < 2; j++) {
								vertices.push_back(vt[textureIndex][j]);
							}
						}
					

				}
				else {
					continue;
				}
			}
			
		}
		catch (const exception& e)
		{
			cerr << e.what() << endl;
		}
		cout << "Data count:" << vertices.size() << endl;
	}

	void init_buffer() {
		glGenVertexArrays(1, &VAO);
		glGenBuffers(1,&VBO);
		glBindVertexArray(VAO);
		glBindBuffer(GL_ARRAY_BUFFER, VBO);
		GLint i0=0;
		GLint i1=1;
		GLint i2=2;
		
		glBufferData(GL_ARRAY_BUFFER, sizeof(GLfloat) * vertices.size(), &vertices[0], GL_STATIC_DRAW);
	
		//pos
		glVertexAttribPointer(i0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)0);
		glEnableVertexAttribArray(i0);
		//norm
		glVertexAttribPointer(i1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)(3 * sizeof(GLfloat)));
		glEnableVertexAttribArray(i1);
		//tex
		glVertexAttribPointer(i2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(GLfloat), (GLvoid*)(6 * sizeof(GLfloat)));
		glEnableVertexAttribArray(i2);
		
	
		glBindVertexArray(0);
		glDisableVertexAttribArray(i0);
		glDisableVertexAttribArray(i1);
		glDisableVertexAttribArray(i2);
	
	}

	void init_textue(const string& tex) {
		
		//InitTexture
		glGenTextures(1, &texture);
		glBindTexture(GL_TEXTURE_2D, texture);
		int width, height;
		unsigned char* image = SOIL_load_image(tex.c_str(), &width, &height, 0, SOIL_LOAD_RGB);
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, image);

		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);


		glBindTexture(GL_TEXTURE_2D, 0);
	}
public:
	GLuint texture;
	Mesh() {}
	Mesh(const string& path,const string& tex="") {
		parse_file(path);
		init_textue(tex);
		init_buffer();
	}
	~Mesh() {}

	void Draw(Shader* shader) {
		//shader->use();
		glBindVertexArray(VAO);		
		glEnableVertexAttribArray(0);
		glEnableVertexAttribArray(1);
		glEnableVertexAttribArray(2);
		glDrawArrays(GL_TRIANGLES, 0, vertices.size());
		glBindTexture(GL_TEXTURE_2D, 0);
		glBindVertexArray(0);
	}

	void ReleaseVBO()
	{
		glBindBuffer(GL_ARRAY_BUFFER, 0);
		glDeleteBuffers(1, &VBO);
		glDeleteVertexArrays(1, &VAO);
	}
};