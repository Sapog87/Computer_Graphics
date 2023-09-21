from PIL import Image, ImageDraw, ImageTk
import tkinter as tk

def flood_fill(image, x, y, target_image, target_color):
    width, height = image.size
    pixels = image.load()

    target_image_width, target_image_height = target_image.size
    target_pixels = target_image.load()

    if x < 0 or x >= width or y < 0 or y >= height:
        return

    if pixels[x, y] != target_color:
        return

    left_x = x
    while left_x > 0 and pixels[left_x - 1, y] == target_color:
        left_x -= 1

    right_x = x
    while right_x < width - 1 and pixels[right_x + 1, y] == target_color:
        right_x += 1

    for i in range(left_x, right_x + 1):
        pixels[i, y] = target_pixels[i % target_image_width, y % target_image_height]

    for i in range(left_x + 1, right_x):
        flood_fill(image, i, y - 1, target_image, target_color)
    for i in range(left_x + 1, right_x):
        flood_fill(image, i, y + 1, target_image, target_color)

prev_x, prev_y = None, None

def main():

    root = tk.Tk()
    root.title("Рисование границ")

    image = Image.new("RGB", (400, 400), (255, 255, 255))  # Белый фон
    draw = ImageDraw.Draw(image)

    def paint(event):
        global prev_x, prev_y
        x, y = event.x, event.y
        if prev_x is not None and prev_y is not None:
            draw.line((prev_x, prev_y, x, y), fill=(0, 0, 0), width=1)
            canvas.create_line(prev_x, prev_y, x, y, fill='black', width=1)
        prev_x, prev_y = x, y

    def reset_prev_coordinates(event):
        global prev_x, prev_y
        prev_x, prev_y = None, None

    canvas = tk.Canvas(root, width=image.width, height=image.height)
    canvas.pack()
    canvas.bind("<B1-Motion>", paint)
    canvas.bind("<ButtonRelease-1>", reset_prev_coordinates)

    def save_image():
        image.save('image_1bb.png')
        print("Изображение сохранено как output_image.png")
        root.quit()
        root.destroy()

    save_button = tk.Button(root, text="Сохранить", command=save_image)
    save_button.pack()

    root.mainloop()


    start_x = 200
    start_y = 200

    image = Image.open('image_1bb.png')
    target_image = Image.open('input_image.png')
    target_color = image.getpixel((start_x, start_y))
    flood_fill(image, start_x, start_y, target_image, target_color)
    image.save('filled_image_1b.png')

    image_small = Image.open('image_1bb.png')
    target_image_small = Image.open('input_image_small.png')
    flood_fill(image_small, start_x, start_y, target_image_small, target_color)
    image_small.save('filled_image_small_1b.png')

    root = tk.Tk()
    canvas = tk.Canvas(root, width=800, height=400)
    canvas.pack()

    im1 = ImageTk.PhotoImage(Image.open('filled_image_1b.png'))

    im2 = ImageTk.PhotoImage(Image.open('filled_image_small_1b.png'))

    canvas.create_image(200, 200, image=im1)

    canvas.create_image(600, 200, image=im2)

    root.mainloop()

if __name__ == "__main__":
    main()