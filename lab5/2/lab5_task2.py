import pygame
import random
import math
import argparse

WIDTH, HEIGHT = 800, 600
WHITE = (255, 255, 255)

pygame.init()
screen = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Midpoint Displacement")

class Point2D:
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def draw(self):
        pygame.draw.circle(screen, (0, 0, 0), (int(self.x), int(self.y)), 0)

class Edge:
    def __init__(self, start, end):
        self.start = start
        self.end = end

    def draw(self):
        pygame.draw.line(screen, (0, 0, 0), (int(self.start.x), int(self.start.y)), (int(self.end.x), int(self.end.y)), 2)

def midpoint(vector, hl, hr, r, iter, delay):
    if iter == 0:
        return

    len = math.sqrt((hr.x - hl.x) ** 2 + (hr.y - hl.y) ** 2)
    h = (hl.y + hr.y) / 2 + random.randint(-int(r * len), int(r * len))
    np = Point2D((hr.x - hl.x) / 2 + hl.x, h)
    vector.insert(vector.index(hl) + 1, np)

    redraw(screen, vector, delay)

    midpoint(vector, hl, vector[vector.index(hl) + 1], r, iter - 1, delay)
    midpoint(vector, vector[vector.index(hr) - 1], hr, r, iter - 1, delay)

def redraw(screen, vector, delay):
    screen.fill(WHITE)
    for i in range(len(vector) - 1):
        Edge(vector[i], vector[i + 1]).draw()
    for point in vector:
        point.draw()
    pygame.display.flip()
    pygame.time.delay(delay)

def main():
    parser = argparse.ArgumentParser(description='L System')
    parser.add_argument('-r', metavar='randomness', type=float, default=0.3, help='randomness')
    parser.add_argument('-i', metavar='iterations', type=int, default=8, help='iterations')
    parser.add_argument('-d', metavar='delay', type=int, default=10, help='delay')
    args = parser.parse_args()

    r = args.r
    iterations = args.i
    delay = args.d

    vector = [Point2D(50, HEIGHT // 2), Point2D(WIDTH - 50, HEIGHT // 2)]
    redraw(screen, vector, delay)

    midpoint(vector, vector[0], vector[1], r, iterations, delay)

    running = True
    while running:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False

    pygame.quit()

if __name__ == "__main__":
    main()
