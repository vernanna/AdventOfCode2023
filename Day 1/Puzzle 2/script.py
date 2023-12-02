import re

spelled_digits = {
  "one": 1,
  "two": 2,
  "three": 3,
  "four": 4,
  "five": 5,
  "six": 6,
  "seven": 7,
  "eight": 8,
  "nine": 9
}

def get_digits(line):
    digits = []
    for index, char in enumerate(line):
        if char.isdigit():
            digits.append((index, char))
    
    for spelled_digit, digit in spelled_digits.items():
        for occurrence in re.finditer(spelled_digit, line):
            digits.append((occurrence.start(), digit))

    return digits

    

input = open('./input.txt')
sum = 0
for line in input.readlines():
    digits = get_digits(line)
    first_digit = min(digits, key=lambda item: item[0])[1]
    last_digit = max(digits, key=lambda item: item[0])[1]
    calibration_value = int(str(first_digit) + str(last_digit))
    sum += calibration_value
print(sum)
input.close()