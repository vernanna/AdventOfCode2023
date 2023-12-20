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

$starting_nodes = $nodes.filter {|node| node.id.end_with?("A")}
$numbers_of_steps = []

for starting_node in $starting_nodes
  $current_node = starting_node
  $number_of_steps = 0

  while !$current_node.id.end_with?("Z")
    $instruction_index = $number_of_steps % $instructions.length
    $instruction = $instructions[$instruction_index]
    $next_node_id = $instruction == "L" ? $current_node.left : $current_node.right
    $current_node = $nodes.detect {|node| node.id == $next_node_id}
    $number_of_steps += 1
  end

  $numbers_of_steps << $number_of_steps
end

$smallest_number_of_steps = $numbers_of_steps.reduce(1, :lcm)

puts $smallest_number_of_steps
