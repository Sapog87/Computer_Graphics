import cv2
import numpy as np
import matplotlib.pyplot as plt

image = cv2.imread('input_image.jpg')

gray1 = 0.299 * image[:, :, 2] + 0.587 * image[:, :, 1] + 0.114 * image[:, :, 0]
gray2 = 0.2126 * image[:, :, 2] + 0.7152 * image[:, :, 1] + 0.0722 * image[:, :, 1]
difference = np.abs(gray1 - gray2)

cv2.imwrite('gray1.jpg', gray1)
cv2.imwrite('gray2.jpg', gray2)
cv2.imwrite('difference.jpg', difference.astype(np.uint8))

gray1_im = cv2.imread('gray1.jpg');
hist = cv2.calcHist([gray1_im], [0], None, [256], [0, 256])

gray2_im = cv2.imread('gray2.jpg');
hist = cv2.calcHist([gray2_im], [0], None, [256], [0, 256])

diff_im = cv2.imread('difference.jpg');

plt.figure(figsize=(12,10))


plt.subplot(5,1,1)
plt.imshow(gray1_im)

plt.subplot(5,1,2)
plt.hist(gray1_im.ravel(), 256, [0, 256], color='black', alpha = 0.5)
plt.xlabel('Интенсивность')
plt.ylabel('Частота')

plt.subplot(5,1,3)
plt.imshow(gray2_im)

plt.subplot(5,1,4)
plt.hist(gray2_im.ravel(), 256, [0, 256], color='black', alpha = 0.5)
plt.xlabel('Интенсивность')
plt.ylabel('Частота')

plt.subplot(5,1,5)
plt.imshow(diff_im)

plt.show()