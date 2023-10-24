import math
import random
import argparse

from PIL import Image, ImageDraw

global ang, stack, randomm, itt

def execute_model(filename, iter = 6, randd = False):
    with open(filename, 'r') as ff:
        lines = ff.readlines()
        line1 = lines[0].split(' ')

        global ang, stack, randomm, itt
        ang = int(line1[1])
        stack = []
        randomm = randd
        itt = iter

        print(filename, iter, randomm)

        _grules = lines[1::]
        grules = {}
        for r in _grules:
            _t = r.split(' ')
            rr = _t[1]
            if _t[1][len(_t[1])-1] in ['\n','\r']:
                rr = _t[1][:-1:]
            grules[_t[0]] = rr

        print(grules)

        result = generate(line1[0], itt, grules)
        state = prerender(result, int(line1[2]))
        render(state, result, int(line1[2]))

def prerender(cmds, angle, border=100):
    xmin, xmax, ymin, ymax, x, y = [0] * 6

    for symbol in cmds:
        x, y, angle = execute(x, y, angle, symbol)
        xmin, xmax = min(x, xmin), max(x, xmax)
        ymin, ymax = min(y, ymin), max(y, ymax)

    print(xmin, xmax)
    print(ymin, ymax)
    width = abs(xmin) + abs(xmax)
    height = abs(ymin) + abs(ymax)

    if width % 2 != 0:
        width += 1
    if height % 2 != 0:
        height += 1

    shift_x = abs(xmin)
    shift_y = abs(ymin)

    return (width + 2 * border, height + 2 * border, shift_x + border, shift_y + border)


def render(state, cmds, angle):
    px, py, lx, ly = [0] * 4
    w, h, sx, sy = state

    img = Image.new('RGB', (w, h), 'white')
    drw = ImageDraw.Draw(img)

    width = itt + 1
    color = (100,75,0)
    inc = 255 / itt

    stack.clear()
    for symbol in cmds:
        if symbol == '[':
            stack.append((px,py,angle,color,width))
            continue
        elif symbol == ']':
            px,py,angle,color,width = stack.pop()
            lx, ly = px, py
            continue
        elif (symbol == '@'):
            width -= 1
            color = (color[0], int(color[1] + inc), color[2])
            continue
        px, py, angle = execute(px, py, angle, symbol)
        if symbol not in  {'[',']'}:
            drw.line((lx + sx, ly + sy, px + sx, py + sy), fill=color, width=width)
        lx, ly = px, py

    img.save("gg.png")

def generate(init, iterations, rules):
    for _ in range(iterations):
        res = []
        for var in init:
            res.append(rules[var] if rules.get(var) else var)

        init = ''.join(res)

    return init


def execute(x, y, angle, symbol):
    global stack, ang

    sign = lambda a: (a>0) - (a<0)
    _a = sign(ang)
    if (randomm):
       _a *= random.randint(0,abs(ang))
    else:
        _a *= abs(ang)

    if symbol == '+':
        angle = (angle + _a) % 360
    elif symbol == '-':
        angle = (angle - _a) % 360
    elif symbol == '[':
        stack.append((x,y,angle))
    elif symbol == ']':
        x,y,angle = stack.pop()
    elif ord(symbol) in range(65, 91):
        x += round(20 * math.cos(math.radians(angle)))
        y += round(20 * math.sin(math.radians(angle)))

    return x, y, angle

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='L System')
    parser.add_argument('-f', metavar='file', type=str, default=None, help='file')
    parser.add_argument('-i', metavar='iterations', type=int, default=6, help='iterations')
    parser.add_argument('-r', metavar='random', type=bool, default=False, help='random')
    args = parser.parse_args()
    if args.f:
        execute_model(args.f, args.i, args.r)
    else:
        parser.print_help()