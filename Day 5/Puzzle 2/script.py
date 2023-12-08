import re

class CriteriaMap:
  def __init__(self, sourceCriterion, destinationCriterion, sourceStart, sourceEnd, destinationStart, destinationEnd):
    self.sourceCriterion = sourceCriterion
    self.destinationCriterion = destinationCriterion
    self.sourceStart = sourceStart
    self.sourceEnd = sourceEnd
    self.destinationStart = destinationStart
    self.destinationEnd = destinationEnd

def chunk(list, n): 
    for i in range(0, len(list), n):  
        yield list[i:i + n] 
    

input = open('./input.txt')
sum = 0
lines = input.readlines()

line = lines.pop()

seeds = line.split()
seeds = [+seed for seed in seeds]

linesByCriteriaMap = [[]]
linesForCriteriaMap =  []

criteriaMap: CriteriaMap

for line in lines:
   if(line.isspace()):
      
      linesByCriteriaMap.append(linesForCriteriaMap)
      linesForCriteriaMap =  []
      continue
   linesForCriteriaMap.append(line)



print(sum)
input.close()