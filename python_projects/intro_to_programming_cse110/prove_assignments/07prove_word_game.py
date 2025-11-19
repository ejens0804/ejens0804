#secret word is bacon
guesses = 0
word = "bacon" 
guess = ""

# Define a function that will print the letter if it is in the word and capitalize it if it's in the right position
def letter_position(guess):
    for i in range(len(word)):
        if guess[i] == word[i]:
            print(guess[i].upper(), end = " ")
        elif guess[i] in word:
            print(guess[i], end = " ")
        else:
            print("_", end = " ")

# Main game loop
print("Welcome to the word guessing game!")
print("Your hint is: _ _ _ _ _")
while guess != word:
    guess = (input("What is your guess?")).lower()
    guesses += 1
    while len(guess) != 5:
        print("Your guess must be 5 letters long.")
        guess = (input("What is your guess?")).lower()
        guesses += 1
        continue
    letter_position(guess)

if guess == word:
        print("Congratulations! You guessed the word!")
        print(f"It took you {guesses} guesses.")