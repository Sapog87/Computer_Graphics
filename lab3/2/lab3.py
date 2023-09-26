# -*- coding: cp1251 -*-
import matplotlib.pyplot as plt

# Функция для рисования отрезка целочисленным алгоритмом Брезенхема
def draw_line_bresenham(x1, y1, x2, y2):
    dx = abs(x2 - x1)
    dy = abs(y2 - y1)
    steep = dy > dx

    if steep:
        x1, y1 = y1, x1
        x2, y2 = y2, x2

    if x1 > x2:
        x1, x2 = x2, x1
        y1, y2 = y2, y1

    dx = x2 - x1
    dy = abs(y2 - y1)
    error = dx / 2
    ystep = 1 if y1 < y2 else -1
    y = y1

    line_points = []

    for x in range(x1, x2 + 1):
        if steep:
            line_points.append((y, x))
        else:
            line_points.append((x, y))

        error -= dy
        if error < 0:
            y += ystep
            error += dx

    return line_points

# Функция для рисования отрезка алгоритмом ВУ (Ву Шаолинь)
def draw_line_wu(x1, y1, x2, y2):
    points = []

    dx = abs(x2 - x1)
    dy = abs(y2 - y1)
    is_steep = dy > dx

    if is_steep:
        x1, y1 = y1, x1
        x2, y2 = y2, x2

    if x1 > x2:
        x1, x2 = x2, x1
        y1, y2 = y2, y1

    dx = x2 - x1
    dy = y2 - y1
    gradient = dy / dx

    # Обработка первой точки
    xend = round(x1)
    yend = y1 + gradient * (xend - x1)
    xgap = 1 - fractional_part(x1 + 0.5)
    xpxl1 = xend  # x координата окончания отрезка
    ypxl1 = int(yend)

    if is_steep:
        points.append((ypxl1, xpxl1))
        points.append((ypxl1 + 1, xpxl1))
    else:
        points.append((xpxl1, ypxl1))
        points.append((xpxl1, ypxl1 + 1))

    intery = yend + gradient

    # Обработка второй точки
    xend = round(x2)
    yend = y2 + gradient * (xend - x2)
    xgap = fractional_part(x2 + 0.5)
    xpxl2 = xend  # x координата окончания отрезка
    ypxl2 = int(yend)

    if is_steep:
        points.append((ypxl2, xpxl2))
        points.append((ypxl2 + 1, xpxl2))
    else:
        points.append((xpxl2, ypxl2))
        points.append((xpxl2, ypxl2 + 1))

    # Заполнение промежуточных точек
    if is_steep:
        for x in range(xpxl1 + 1, xpxl2):
            points.append((int(intery), x))
            points.append((int(intery) + 1, x))
            intery += gradient
    else:
        for x in range(xpxl1 + 1, xpxl2):
            points.append((x, int(intery)))
            points.append((x, int(intery) + 1))
            intery += gradient

    return points

# Вспомогательная функция для нахождения дробной части числа
def fractional_part(num):
    return num - int(num)

# Задаем координаты начала и конца отрезка
x1, y1 = 20, 30
x2, y2 = 300, 150

# Рисуем отрезок с помощью обоих алгоритмов
line_bresenham = draw_line_bresenham(x1, y1, x2, y2)
line_wu = draw_line_wu(x1, y1+10, x2, y2+10)

# Создаем изображение и рисуем на нем отрезок
plt.figure(figsize=(8, 4))
plt.scatter(*zip(*line_bresenham), s=5, label='Брезенхем')
plt.scatter(*zip(*line_wu), s=5, label='Алгоритм ВУ (Ву Шаолинь)')
plt.legend()
plt.xlim(0, 400)
plt.ylim(0, 200)
plt.gca().invert_yaxis()
plt.gca().set_aspect('equal', adjustable='box')
plt.show()
