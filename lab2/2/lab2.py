# -*- coding: cp1251 -*-
import cv2
import numpy as np
import matplotlib.pyplot as plt

# Загрузка изображения
image = cv2.imread('image.jpg')

# Разделение каналов
blue_channel = image[:, :, 0]
green_channel = image[:, :, 1]
red_channel = image[:, :, 2]

# Построение гистограммы для каждого канала
hist_blue = cv2.calcHist([blue_channel], [0], None, [256], [0, 256])
hist_green = cv2.calcHist([green_channel], [0], None, [256], [0, 256])
hist_red = cv2.calcHist([red_channel], [0], None, [256], [0, 256])

# Вывод результатов
plt.figure(figsize=(10, 5))

plt.subplot(2, 2, 1)
plt.imshow(cv2.cvtColor(blue_channel, cv2.COLOR_BGR2RGB))
plt.title('Blue Channel')

plt.subplot(2, 2, 2)
plt.imshow(cv2.cvtColor(green_channel, cv2.COLOR_BGR2RGB))
plt.title('Green Channel')

plt.subplot(2, 2, 3)
plt.imshow(cv2.cvtColor(red_channel, cv2.COLOR_BGR2RGB))
plt.title('Red Channel')

plt.subplot(2, 2, 4)
plt.plot(hist_blue, color='blue')
plt.plot(hist_green, color='green')
plt.plot(hist_red, color='red')
plt.title('Color Histograms')
plt.legend(['Blue', 'Green', 'Red'])

plt.tight_layout()
plt.show()
