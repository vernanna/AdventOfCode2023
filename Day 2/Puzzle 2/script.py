import re

def get_set_power(line):
    return get_maximum_number_of_cubes(line, 'green') * get_maximum_number_of_cubes(line, 'red') * get_maximum_number_of_cubes(line, 'blue')

def get_maximum_number_of_cubes(line, color):
    revealed_cubes = [int(re.search('\d+', revealed_cubes_in_color).group()) for revealed_cubes_in_color in re.findall('\d+ ' + color, line)]
    return max(revealed_cubes)

input = open('./input.txt')
sum = 0
for line in input.readlines():
    sum += get_set_power(line)

print(sum)
input.close()