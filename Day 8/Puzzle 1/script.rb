class Node
  def initialize(id, left, right)
    @id = id
    @left = left
    @right = right
  end

  attr_reader :id
  attr_reader :left
  attr_reader :right

end

$lines = File.readlines("input.txt", chomp: true)
$instructions = $lines.shift.split("")
$node_lines = $lines[1..]

$nodes = $node_lines.map{ |line|
  parts = line.scan(/[A-Z][A-Z][A-Z]/)
  node = Node.new(parts[0], parts[1], parts[2])
}

$current_node = $nodes.detect {|node| node.id == "AAA"}
$number_of_steps = 0

while $current_node.id != "ZZZ"
  $instruction_index = $number_of_steps % $instructions.length
  $instruction = $instructions[$instruction_index]
  $next_node_id = $instruction == "L" ? $current_node.left : $current_node.right
  $current_node = $nodes.detect {|node| node.id == $next_node_id}
  $number_of_steps += 1
end

puts $number_of_steps
