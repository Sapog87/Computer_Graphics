from PIL import Image, ImageDraw
from queue import Queue

directions = [
    (0, 1),
    (1, 1),
    (1, 0),
    (1, -1),
    (0, -1),
    (-1, -1),
    (-1, 0),
    (-1, 1)
]

def find_boundary(image, start_x, start_y, boundary_color):
    width, height = image.size
    pixels = image.load()
    boundary = []

    q = Queue()
    q.put((start_x, start_y))

    while not q.empty():
        current_x, current_y = q.get()
        current_color = pixels[current_x, current_y]
        if current_color == boundary_color:
            boundary.append((current_x, current_y))
            pixels[current_x, current_y] = (0, 255, 0)
        else:
            continue

        for i in range(8):
            lx, ly = current_x + directions[i][0], current_y + directions[i][1]

            if (lx , ly) in boundary:
                continue

            if pixels[lx, ly] != boundary_color:
                continue

            if pixels[lx, ly] == boundary_color:
                has_neighbors = False
                for j in range(0,7,2):
                    has_neighbors = has_neighbors or pixels[lx + directions[j][0], ly + directions[j][1]] == (255,255,255)

                if not has_neighbors:
                    continue

            q.put((lx, ly))

    return boundary

def start_outline(x, y, pixels, size):
    inner_color = pixels[x, y]
    while x != size[0]:
        if pixels[x, y] != inner_color:
            return (x - 1, y)
        x += 1
    return None

def main():
    image = Image.open('image_1c.png')

    start_x, start_y = start_outline(250, 250, image.load(), image.size)

    boundary = find_boundary(image, start_x, start_y, (255,0,0))

    draw = ImageDraw.Draw(image)
    for x, y in boundary:
        draw.point((x, y), fill=(0,255,0))

    image.save('output_image_1c.png')

if __name__ == "__main__":
    main()