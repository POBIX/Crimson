# Generates the vertices for a circle and copies the output to the clipboard.
import math
import pyperclip

output = ""
sides = 90

angle = 0
while angle <= math.tau:
    output += f"{math.cos(angle)}f, {math.sin(angle)}f,\n"
    angle += math.tau / sides

pyperclip.copy(output)
