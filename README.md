# RSM-Card-Game-Test

A simple demo of a card game drawing cards to hand, discarding cards, and shuffling discard pile into deck.

## Player Turn

- Draw phase
  - while(hand < 4)
    - if(deck == 0)
      - shuffle discard
    - draw card
- Play Phase
  - Can play card if enough energy
  - Can click 'End Turn' to  end turn
- End Turn Phase
  - Discard hand
  - Go to next turn

## Development Requirements

- Create cards from json hosted at website
- Card Anatomy:
  - Cost
  - Name
  - Main Art
  - Background Art
  - Type
  - Text
- Deck display in lower left of screen
  - Shows card count in deck
- Energy count display
  - Shows current energy slash max energy
- Discard Pile
  - Shows card count in discard pile
- Show player hand on semi circle
- Animate card draw
- On mouse hover over card in hand: raise card to view it
- If (energy >= card cost) 
  - Card cost text is white
  - Card has glow around it
  - Card can be played
- Else  
  - Card cost text is red
  - Card has no glow
  - Card cannot be played
- Click and drag card
  - If release in center of screen and enough energy
    - Card hover animation for 2 sec
    - Discard card
    - Reduce energy
  - Else if release in other area of screen
    - Return to same hand slot
    
## Code Design
