import re

number_of_green_cubes = 13
number_of_red_cubes = 12
number_of_blue_cubes = 14

def get_game_id(line):
    return int(re.search('\d+', line).group())

def is_possible(line):
    if get_maximum_number_of_cubes(line, 'green') > number_of_green_cubes:
        return False
    if get_maximum_number_of_cubes(line, 'red') > number_of_red_cubes:
        return False
    if get_maximum_number_of_cubes(line, 'blue') > number_of_blue_cubes:
        return False
    
    return True

def get_maximum_number_of_cubes(line, color):
    revealed_cubes = [int(re.search('\d+', revealed_cubes_in_color).group()) for revealed_cubes_in_color in re.findall('\d+ ' + color, line)]
    return max(revealed_cubes)

input = open('./input.txt')
sum = 0
for line in input.readlines():
    game_id = get_game_id(line)
    if is_possible(line):
        sum += game_id

print(sum)
input.close()