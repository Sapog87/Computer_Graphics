from PIL import Image

def flood_fill(image, x, y, target_color, replacement_color):
    width, height = image.size
    pixels = image.load()

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
        pixels[i, y] = replacement_color

    for i in range(left_x + 1, right_x):
        flood_fill(image, i, y - 1, target_color, replacement_color)
    for i in range(left_x + 1, right_x):
        flood_fill(image, i, y + 1, target_color, replacement_color)

def main():
    image = Image.open('image.png')

    start_x = 300
    start_y = 300

    replacement_color = (255, 0, 0)

    target_color = image.getpixel((start_x, start_y))

    flood_fill(image, start_x, start_y, target_color, replacement_color)

    image.save('filled_image_1a.png')

if __name__ == "__main__":
    main()