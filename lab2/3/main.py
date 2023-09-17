from tkinter import *
from tkinter import ttk
import cv2
from PIL import Image, ImageTk
import numpy as np

def apply():
    h, s, v = cv2.split(hsv_img)
    h2, s2, v2 = int(spinbox1.get()), int(spinbox2.get()), int(spinbox3.get())
    
    h_new = np.mod(h + h2, 180).astype(np.uint8)
    s_new = np.mod(s + s2, 256).astype(np.uint8)
    v_new = np.mod(v + v2, 256).astype(np.uint8)

    hsv_new = cv2.merge([h_new, s_new, v_new])
    
    bgr_img_output = cv2.cvtColor(hsv_new, cv2.COLOR_HSV2BGR)
    cv2.imwrite("output_image.png", bgr_img_output)    
    new_img = ImageTk.PhotoImage(file="output_image.png")
    img.configure(image=new_img)
    img.image = new_img


root = Tk()
root.geometry("750x750") 
bgr_img = cv2.imread('input_image.png')


ttk.Label(text="Оттенок").pack(anchor=NW)
spinbox1 = ttk.Spinbox(from_=0, to=179)
spinbox1.set(0)
spinbox1.pack(anchor=NW)

ttk.Label(text="Насыщенность").pack(anchor=NW)
spinbox2 = ttk.Spinbox(from_=0, to=255)
spinbox2.set(0)
spinbox2.pack(anchor=NW)

ttk.Label(text="Яркость").pack(anchor=NW)
spinbox3 = ttk.Spinbox(from_=0, to=255)
spinbox3.set(0)
spinbox3.pack(anchor=NW)


blue, green, red = cv2.split(bgr_img)
t_image = cv2.merge((red, green, blue))
tt_image = Image.fromarray(t_image)
display_image = ImageTk.PhotoImage(image=tt_image)
img = ttk.Label(image=display_image)
img.pack()

hsv_img = cv2.cvtColor(bgr_img, cv2.COLOR_BGR2HSV)

ttk.Button(text="Применить", command=apply).pack()
 
root.mainloop()
