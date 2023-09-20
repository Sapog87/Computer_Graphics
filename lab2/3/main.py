from tkinter import *
from tkinter import ttk
import cv2
from PIL import Image, ImageTk
import numpy as np

def apply():
    image = cv2.imread('input_image.png')
    hsv_img = cv2.cvtColor(image, cv2.COLOR_BGR2HSV)

    h1, s1, v1 = int(spinbox1.get()), int(spinbox2.get()), int(spinbox3.get())

    h, s, v = cv2.split(hsv_img)
    
    h2 = h
    s2 = s
    v2 = v

    if (h1 >= 0):
        h2 = np.where(h <= 255 - h1, h + h1, 255)
    else:
        h2 = np.where(h >= -h1, h + h1, 0)

    if (s1 >= 0):
        s2 = np.where(s <= 255 - s1, s + s1, 255)
    else:
        s2 = np.where(s >= -s1, s + s1, 0)

    if (v1 >= 0):
        v2 = np.where(v <= 255 - v1, v + v1, 255)
    else:
        v2 = np.where(v >= -v1, v + v1, 0)

    h2 = h2.astype(h.dtype)
    s2 = s2.astype(s.dtype)
    v2 = v2.astype(v.dtype)

    final_hsv = cv2.merge((h2, s2, v2))

    image = cv2.cvtColor(final_hsv, cv2.COLOR_HSV2BGR)

    cv2.imwrite('output_image.png', image)
    
    new_img = ImageTk.PhotoImage(file="output_image.png")
    img.configure(image=new_img)
    img.image = new_img


root = Tk()
root.geometry("750x750") 
bgr_img = cv2.imread('input_image.png')


ttk.Label(text="Оттенок").pack(anchor=NW)
spinbox1 = ttk.Spinbox(from_=-255, to=255)
spinbox1.set(0)
spinbox1.pack(anchor=NW)

ttk.Label(text="Насыщенность").pack(anchor=NW)
spinbox2 = ttk.Spinbox(from_=-255, to=255)
spinbox2.set(0)
spinbox2.pack(anchor=NW)

ttk.Label(text="Яркость").pack(anchor=NW)
spinbox3 = ttk.Spinbox(from_=-255, to=255)
spinbox3.set(0)
spinbox3.pack(anchor=NW)


blue, green, red = cv2.split(bgr_img)
t_image = cv2.merge((red, green, blue))
tt_image = Image.fromarray(t_image)
display_image = ImageTk.PhotoImage(image=tt_image)
img = ttk.Label(image=display_image)
img.pack()

ttk.Button(text="Применить", command=apply).pack()
 
root.mainloop()
