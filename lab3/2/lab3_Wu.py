import tkinter as tk
from PIL import Image, ImageDraw, ImageTk
import os.path

prev_x, prev_y = None, None

def main():
    image  = None

    k = -1
    while(k == -1):
        root = tk.Tk()

        if not os.path.isfile("image_w.png"):
            image = Image.new("RGBA", (800, 800), (255, 255, 255, 255))
        else:
            image = Image.open("image_w.png")

        def close():
            k = 1
            root.quit()

        def paint(event):
            global prev_x, prev_y
            x, y = event.x, event.y
            if prev_x is not None and prev_y is not None:
                wu_line(x,y,prev_x, prev_y)
                prev_x, prev_y = None, None
                image.save("image_w.png")
                root.destroy()
                return
            prev_x, prev_y = x, y

        def draw_point(steep, x, y, alpha):
            if steep:
                x, y = y, x
            draw = ImageDraw.Draw(image)
            color = (0, 0, 0, int(alpha * 255))
            print(color)
            draw.point((x, y), fill=color)

        def wu_line(x0, y0, x1, y1):
            steep = abs(y1 - y0) > abs(x1 - x0)
            if steep:
                x0, y0 = y0, x0
                x1, y1 = y1, x1

            if x0 > x1:
                x0, x1 = x1, x0
                y0, y1 = y1, y0

            draw_point(steep, x0, y0, 1)
            draw_point(steep, x1, y1, 1)

            dx = x1 - x0
            dy = y1 - y0
            gradient = dy / dx
            y = y0 + gradient

            for x in range(x0 + 1, x1):
                draw_point(steep, x, int(y), 1 - (y - int(y)))
                draw_point(steep, x, int(y) + 1, y - int(y))
                y += gradient

        canvas = tk.Canvas(root, width=image.width, height=image.height)
        canvas.pack()
        im = ImageTk.PhotoImage(image=image)
        canvas.create_image(400,400, image=im)
        canvas.bind("<Button-1>", paint)
        save_button = tk.Button(root, text="Выйти", command=close)
        save_button.pack()

        root.mainloop()

if __name__ == "__main__":
    main()