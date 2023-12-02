def get_first_digit(line):
    for char in line:
        if char.isdigit():
            return char

input = open('./input.txt')
sum = 0
for line in input.readlines():
    first_digit = get_first_digit(line)
    last_digit = get_first_digit(line[::-1])
    calibration_value = int(str(first_digit) + str(last_digit))
    sum += calibration_value
print(sum)
input.close()