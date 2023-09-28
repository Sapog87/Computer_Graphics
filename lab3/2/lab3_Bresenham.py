from PIL import Image, ImageDraw, ImageTk
import tkinter as tk

prev_x, prev_y = None, None

def main():

    root = tk.Tk()
    root.title("Рисование границ")

    image = Image.new("RGB", (800, 800), (255, 255, 255))

    def paint(event):
        global prev_x, prev_y
        x, y = event.x, event.y
        if prev_x is not None and prev_y is not None:
            draw_line(x,y,prev_x, prev_y, (0,0,0))
            canvas.create_line(prev_x, prev_y, x, y, fill='black', width=1)
            image.save("image_b.png")
            prev_x, prev_y = None, None
            return
        prev_x, prev_y = x, y

    def draw_line(x1, y1, x2, y2, color):
        pixels = image.load()

        dx = x2 - x1
        dy = y2 - y1

        sign_x = 1 if dx>0 else -1 if dx<0 else 0
        sign_y = 1 if dy>0 else -1 if dy<0 else 0

        if dx < 0: dx = -dx
        if dy < 0: dy = -dy

        if dx > dy:
            pdx, pdy = sign_x, 0
            es, el = dy, dx
        else:
            pdx, pdy = 0, sign_y
            es, el = dx, dy

        x, y = x1, y1

        error, t = el/2, 0        

        pixels[x, y] = color

        while t < el:
            error -= es
            if error < 0:
                error += el
                x += sign_x
                y += sign_y
            else:
                x += pdx
                y += pdy
            t += 1
            pixels[x, y] = color

    canvas = tk.Canvas(root, width=image.width, height=image.height)
    canvas.pack()
    canvas.bind("<Button-1>", paint)

    root.mainloop()

if __name__ == "__main__":
    main()