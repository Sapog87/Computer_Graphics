import numpy as np
import matplotlib.pyplot as plt

def rasterize_triangle(v1, v2, v3, colors):
    # Определение минимальной и максимальной координаты y
    ymin = min(v1[1], v2[1], v3[1])
    ymax = max(v1[1], v2[1], v3[1])

    # Создание буфера пикселей
    image = np.zeros((int(ymax - ymin + 1), 500, 3))

    # Алгоритм растеризации
    for y in range(int(ymin), int(ymax + 1)):
        # Определение минимальной и максимальной координаты x для текущей строки
        x_min = image.shape[1]
        x_max = -1

        for x in range(image.shape[1]):
            # Вычисление барицентрических координат
            alpha = ((v2[1] - v3[1]) * (x - v3[0]) + (v3[0] - v2[0]) * (y - v3[1])) / \
                    ((v2[1] - v3[1]) * (v1[0] - v3[0]) + (v3[0] - v2[0]) * (v1[1] - v3[1]))
            beta = ((v3[1] - v1[1]) * (x - v3[0]) + (v1[0] - v3[0]) * (y - v3[1])) / \
                   ((v2[1] - v3[1]) * (v1[0] - v3[0]) + (v3[0] - v2[0]) * (v1[1] - v3[1]))
            gamma = 1 - alpha - beta

            # Проверка, что пиксель находится внутри треугольника
            if 0 <= alpha <= 1 and 0 <= beta <= 1 and 0 <= gamma <= 1:
                # Вычисление значений компонент цвета
                R = alpha * colors[0][0] + beta * colors[1][0] + gamma * colors[2][0]
                G = alpha * colors[0][1] + beta * colors[1][1] + gamma * colors[2][1]
                B = alpha * colors[0][2] + beta * colors[1][2] + gamma * colors[2][2]

                # Установка значения цвета в буфере пикселей
                image[int(y - ymin), x, :] = [R, G, B]                

                # Обновление минимальной и максимальной координаты x
                if x < x_min:
                    x_min = x
                if x > x_max:
                    x_max = x

        # Отрисовка строки
        if x_min != image.shape[1] and x_max != -1:
            plt.imshow(image / 255)

    plt.show()


v1 = [100, 150]
v2 = [200, 250]
v3 = [400, 100]
colors = [[255, 0, 0], [0, 255, 0], [0, 0, 255]]
rasterize_triangle(v1, v2, v3, colors)