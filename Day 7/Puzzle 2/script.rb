HAND_TYPE = {
  five_of_a_kind: 7,
  four_of_a_kind: 6,
  full_house: 5,
  three_of_a_kind: 4,
  two_pairs: 3,
  one_pair: 2,
  high_card: 1,
}

class Hand
  def initialize(cards, bid)
    @cards = cards
    @bid = bid
    grouped_cards = cards.filter {|card| card != 0}.group_by(&:itself).map {|k,v| v}
    number_of_jokers = cards.count {|card| card == 0}
    if grouped_cards.any? {|group| group.length == 5}
      @type = HAND_TYPE[:five_of_a_kind]
    elsif grouped_cards.any? {|group| group.length == 4}
      @type = HAND_TYPE[:four_of_a_kind]
    elsif grouped_cards.any? {|group| group.length == 3}
      if grouped_cards.any? {|group| group.length == 2}
        @type = HAND_TYPE[:full_house]
      else
        @type = HAND_TYPE[:three_of_a_kind]
      end
    elsif grouped_cards.any? {|group| group.length == 2}
      if grouped_cards.count {|group| group.length == 2} == 2
        @type = HAND_TYPE[:two_pairs]
      else
        @type = HAND_TYPE[:one_pair]
      end
    elsif number_of_jokers == 5
      @type = HAND_TYPE[:five_of_a_kind]
    else
      @type = HAND_TYPE[:high_card]
    end

    if @type == HAND_TYPE[:four_of_a_kind] && number_of_jokers > 0
      @type = HAND_TYPE[:five_of_a_kind]
    elsif @type == HAND_TYPE[:three_of_a_kind] && number_of_jokers > 0
      if number_of_jokers == 1
        @type = HAND_TYPE[:four_of_a_kind]
      end
      if number_of_jokers == 2
        @type = HAND_TYPE[:five_of_a_kind]
      end
    elsif @type == HAND_TYPE[:two_pairs] && number_of_jokers > 0
      @type = HAND_TYPE[:full_house]
    elsif @type == HAND_TYPE[:one_pair] && number_of_jokers > 0
      if number_of_jokers == 1
        @type = HAND_TYPE[:three_of_a_kind]
      end
      if number_of_jokers == 2
        @type = HAND_TYPE[:four_of_a_kind]
      end
      if number_of_jokers == 3
        @type = HAND_TYPE[:five_of_a_kind]
      end
    elsif @type == HAND_TYPE[:high_card] && number_of_jokers > 0
      if number_of_jokers == 1
        @type = HAND_TYPE[:one_pair]
      end
      if number_of_jokers == 2
        @type = HAND_TYPE[:three_of_a_kind]
      end
      if number_of_jokers == 3
        @type = HAND_TYPE[:four_of_a_kind]
      end
      if number_of_jokers == 4
        @type = HAND_TYPE[:five_of_a_kind]
      end
    end
end

attr_reader :type
attr_reader :cards
attr_reader :bid

def display_details()
    puts "Customer id #@cust_id"
    puts "Customer name #@cust_name"
    puts "Customer address #@cust_addr"
  end
end

$lines = File.readlines('input.txt', chomp: true)
$hands = $lines.map{ |line|
  parts = line.split
  cards = parts[0].split('').map { |card|
    parsedCard = 0
    if card == "T"
      parsedCard = 10
    elsif card == "J"
      parsedCard = 0
    elsif card == "Q"
      parsedCard = 12
    elsif card == "K"
      parsedCard = 13
    elsif card == "A"
      parsedCard = 14
    else
      parsedCard = Integer(card)
    end
  }
  bid = Integer(parts[1])
  hand = Hand.new(cards, bid)
}

$sum_of_winnings = $hands
  .sort_by { |hand| [hand.type, hand.cards[0], hand.cards[1], hand.cards[2], hand.cards[3], hand.cards[4]] }
  .each_with_index
  .map { |hand, index| hand.bid * (index + 1) }
  .sum

puts $sum_of_winnings
