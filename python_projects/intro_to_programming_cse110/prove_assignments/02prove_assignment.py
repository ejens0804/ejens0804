# Word Games Assignment
print("Welcome to Word Games!")
print("Please enter the following:")
adjective = input("Adjective:")
animal = input("Animal:")
verb1 = input("Verb:")
exclamation = input("Exclamation:")
verb2 = input("Verb:")
verb3 = input("Verb:")

story_mode = input("Would you like to use story 1, 2, or 3?")
story_mode = int(story_mode)

if story_mode == 1:
    print("Story Number 1 is:\n")
    print("During one of my classes today I felt very " + adjective.lower() + "." + "  This made my day very interesting, but when I got home I found a "
        + animal.capitalize() + " sitting on my desk! I then " + verb1.lower() + " over to my desk and shouted " + exclamation.upper() + "!!! Now I "
        + verb2.lower() + " down the stairs, and I " + verb3.lower() + " across the lawn and fall over.  Overall, today was a pretty interesting day.  The End!")
    
elif story_mode == 2:
    print("Story Number 2 Is:\n")
    print("Once upon a time there was a whale named Jeff!  He loved to " + verb1.lower() + " and play in the water!  One day as he was swimming along, he found a " + animal.capitalize() + 
        " trapped under a tangle of " + adjective.lower() + " fishing nets!  Oh " + exclamation.upper() + "!!!  He exclaimed!  This truly was a strange predicament!  I'll " + verb2.lower() + " over to that "
        + animal.capitalize() + " and try to help him!  Jeff started to " + verb3.lower() + " closer until he noticed something odd...  That animal is made of plastic!!  " +
        "'Whew' Jeff said, glad that no one was hurt.  The End!")   
    
elif story_mode == 3:
    print("Story Number 3 Is:\n")
    print("As the knight in his bright " + adjective.lower() + "armor crested the hill, he beheld a dark, sinister castle!  He started to " + verb1.lower() + 
        " towards the castle to save whatever maiden was inside, when he spotted a monstrous " + animal.capitalize() + "!  This gigantic beast had long fangs, and a huge pair of bat like wings! Well "
        + exclamation.upper() +  " he yelled in fright!  This'll definitely make things harder than he had imagined!  He started to " + verb2.lower() + " away from the creature, thinking that he should've just stayed home...  "
        + "Oh well, I guess I'll just have to go " + verb3.lower() + " somewhere else today.  And then he walked home.  The End!")
   
else: 
    print("Sorry, we don't have that story written yet.  Come back later please.") 