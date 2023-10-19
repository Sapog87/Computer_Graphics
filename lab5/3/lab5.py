from tkinter import *

class BezierCurve:
    def __init__(self):
        self.root = Tk()
        self.canvas = Canvas(self.root, width=800, height=600)
        self.canvas.pack()

        self.points = []
        self.curves = []

        self.canvas.bind("<Button-1>", self.add_point)
        self.canvas.bind("<B1-Motion>", self.move_point)
        self.canvas.bind("<Button-3>", self.delete_point)

        self.root.mainloop()

    def add_point(self, event):
        x, y = event.x, event.y
        self.points.append((x, y))
        self.canvas.create_oval(x-3, y-3, x+3, y+3, fill="pink")

        if len(self.points) > 1:
            self.canvas.create_line(
                self.points[-2][0], self.points[-2][1],
                x, y, fill="black"
            )

            if len(self.points) > 3:
                self.draw_curve()

    def move_point(self, event):
        x, y = event.x, event.y
        prev_x, prev_y = self.points[-1]
        self.canvas.move(CURRENT, x - prev_x, y - prev_y)
        self.points[-1] = (x, y)

        if len(self.points) > 3:
            self.draw_curve()

    def delete_point(self, event):
        x, y = event.x, event.y
        for i in range(len(self.points)):
            px, py = self.points[i]
            if abs(px - x) < 5 and abs(py - y) < 5:
                del self.points[i]
                self.canvas.delete("all")
                self.redraw()
                break

        if len(self.points) > 3:
            self.draw_curve()

    def draw_curve(self):
        self.curves = []
        for i in range(3, len(self.points), 3):
            curve_points = self.points[i - 3 : i + 1]
            self.draw_cubic_bezier_curve(curve_points)

    def draw_cubic_bezier_curve(self, points):
        x0, y0 = points[0]
        x1, y1 = points[1]
        x2, y2 = points[2]
        x3, y3 = points[3]

        self.canvas.create_line(x0, y0, x1, y1, fill="blue")
        self.canvas.create_line(x2, y2, x3, y3, fill="blue")

        for t in range(0, 101, 1):
            t = t / 100
            x = (1 - t) ** 3 * x0 + 3 * (1 - t) ** 2 * t * x1 + 3 * (1 - t) * t ** 2 * x2 + t ** 3 * x3
            y = (1 - t) ** 3 * y0 + 3 * (1 - t) ** 2 * t * y1 + 3 * (1 - t) * t ** 2 * y2 + t ** 3 * y3
            self.canvas.create_oval(x, y, x + 1, y + 1, fill="red")

    def redraw(self):
        for i in range(1, len(self.points)):
            x1, y1 = self.points[i - 1]
            x2, y2 = self.points[i]
            self.canvas.create_oval(x1-3, y1-3, x1+3, y1+3, fill="pink")
            self.canvas.create_line(x1, y1, x2, y2, fill="black")

        for curve_points in self.curves:
            self.draw_cubic_bezier_curve(curve_points)

if __name__ == "__main__":
    app = BezierCurve()