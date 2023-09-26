# -*- coding: cp1251 -*-
import cv2
import numpy as np
import matplotlib.pyplot as plt

image = cv2.imread('image.jpg')

blue_channel = image[:, :, 0]
green_channel = image[:, :, 1]
red_channel = image[:, :, 2]

cv2.imwrite('blue_channel.jpg', blue_channel)
cv2.imwrite('green_channel.jpg', green_channel)
cv2.imwrite('red_channel.jpg', red_channel)

blue_channel_im = cv2.imread('blue_channel.jpg');
hist = cv2.calcHist([blue_channel_im], [0], None, [256], [0, 256])

green_channel_im = cv2.imread('green_channel.jpg');
hist = cv2.calcHist([green_channel_im], [0], None, [256], [0, 256])

red_channel_im = cv2.imread('red_channel.jpg');
hist = cv2.calcHist([red_channel], [0], None, [256], [0, 256])

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
plt.hist(blue_channel_im.ravel(), 256, [0, 256], color='blue', alpha = 0.5)
plt.hist(green_channel_im.ravel(), 256, [0, 256], color='green', alpha = 0.5)
plt.hist(red_channel_im.ravel(), 256, [0, 256], color='red', alpha = 0.5)
plt.title('Color Histograms')
plt.legend(['Blue', 'Green', 'Red'])

plt.tight_layout()
plt.show()
