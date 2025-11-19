# Game overview
# Choose weapons, choose a door to go through, fight against a monster, and figure out how to find the treasure hidden in the dungeon
import sys

def my_word_game():
    print("\n")
    print("Welcome to Ethan's Adventure Game!!")
    print("Let's get started...Imagine you're a knight in armor, walking into a dark and gloomy dungeon...")
    print("You find a rack of weapons on the wall.  It has a battle axe, a spear, and a sword.")
    weapon = input("Do you pick up the AXE, SPEAR, or SWORD?")
    weapon_loop = 0

    weapon = weapon.lower()

    # First Decision (WEAPONS)
    while weapon_loop == 0:

        if weapon == "axe":
            print("\nYou grab the battle axe and start walking forward, only to arrive in front of 3 identical doors!")
            weapon_loop = 1
        elif weapon == "spear":
            print("\nYou grab the spear and start walking forward, only to arrive in front of 3 identical doors!")
            weapon_loop = 1
        elif weapon == "sword":
            print("\nYou grab the Sword and start walking forward, only to arrive in front of 3 identical doors!")
            weapon_loop = 1
        else:
            print("\nThat isn't an option...please try again")
            weapon = input("Do you pick up the AXE, SPEAR, SWORD?")


    print("There's a door on the left, middle, and on the right.")


    # Second Decision (DOORS)
    # Left door has a minotaur
    # Middle door has a four armed swordsman
    # Right door has a water serpent
    door = input("Do you choose to go through the door on the LEFT, MIDDLE, or RIGHT?")
    door = door.lower()
    door_loop = 0

    while door_loop == 0:
        if door == "left" and weapon == "axe":
            print("\nYou enter the door on the left and walk down a narrow hallway and enter a large chamber...")
            print("You see a dark figure start to approach you...it's a Minotaur!  He rushes at you with a huge double-bladed axe...")
            door_loop = 1
        elif door == "middle" and weapon == "axe":
            print("\nYou heave the middle door open, and descend down a spiral staircase and enter a dim room...")
            print("You see a four armed man sitting crosslegged on the floor with his head bowed.  He hears you approach and quickly stands, unsheathing 4 curved saber swords!")
            door_loop = 1
        elif door == "right" and weapon == "axe":
            print("\nYou slowly pull the right door open, and see a set of stairs going upwards.  You walk up them, only to find a spacious room with a large pool of water...")
            print("As you enter, the water starts to roil with movement...out comes a large water serpent, fangs bared! It screeches at you and menacingly swings its head...")
            door_loop = 1
        else:
            if weapon != "axe":
                door_loop = 1
            else:
                print("\nThat isn't an option...please try again")
                door = input("Do you pick the door on the LEFT, MIDDLE, or RIGHT?")

    door_loop = 0
    while door_loop == 0:
        if door == "left" and weapon == "spear":
            print("\nYou enter the door on the left and walk down a narrow hallway and enter a large chamber...")
            print("You see a dark figure start to approach you...it's a Minotaur!  He rushes at you with a huge double-bladed axe...")
            door_loop = 1
        elif door == "middle" and weapon == "spear":
            print("\nYou heave the middle door open, and descend down a spiral staircase and enter a dim room...")
            print("You see a four armed man sitting crosslegged on the floor with his head bowed.  He hears you approach and quickly stands, unsheathing 4 curved saber swords!")

            door_loop = 1
        elif door == "right" and weapon == "spear":
            print("\nYou slowly pull the right door open, and see a set of stairs going upwards.  You walk up them, only to find a spacious room with a large pool of water...")
            print("As you enter, the water starts to roil with movement...out comes a large water serpent, fangs bared! It screeches at you and menacingly swings its head...")
            door_loop = 1
        else:
            if weapon != "spear":
                door_loop = 1
            else:
                print("\nThat isn't an option...please try again")
                door = input("Do you pick the door on the LEFT, MIDDLE, or RIGHT?")

    door_loop = 0
    while door_loop == 0:    
        if door == "left" and weapon == "sword":
            print("\nYou enter the door on the left and walk down a narrow hallway and enter a large chamber...")
            print("You see a dark figure start to approach you...it's a Minotaur!  He rushes at you with a huge double-bladed axe...")
            door_loop = 1
        elif door == "middle" and weapon == "sword":
            print("\nYou heave the middle door open, and descend down a spiral staircase and enter a dim room...")
            print("You see a four armed man sitting crosslegged on the floor with his head bowed.  He hears you approach and quickly stands, unsheathing 4 curved saber swords!")
            door_loop = 1
        elif door == "right" and weapon == "sword":
            print("\nYou slowly pull the right door open, and see a set of stairs going upwards.  You walk up them, only to find a spacious room with a large pool of water...")
            print("As you enter, the water starts to roil with movement...out comes a large water serpent, fangs bared! It screeches at you and menacingly swings its head...")
            door_loop = 1
        else:
            if weapon != "sword":
                door_loop = 1
            else:
                print("\nThat isn't an option...please try again")
                door = input("Do you pick the door on the LEFT, MIDDLE, or RIGHT?")



    # Third Decision (MONSTERS) (RUN or FIGHT)
    # Left door has a minotaur
    # Middle door has a four armed swordsman
    # Right door has a water serpent
    
    fight_flight = input("Do you choose to FIGHT or RUN?")
    fight_flight = fight_flight.lower()
    fight_loop = 0

    # Weapon Axe
    while fight_loop == 0:
        if door == "left" and weapon == "axe":
            if fight_flight == "fight":
                print("\nYou rush the minotaur and duck under his first blow, the axe whistling through the air above your head.")
                print("You swing at his exposed side, and cut a long gash into him, angering him even further!")
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou dash away from his razor sharp axe, narrowly missing the blade by centimeters!!")
                print("You sprint out of the room, down the hall, and slam the door shut...")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()

        elif door == "middle" and weapon == "axe":
            if fight_flight == "fight":
                print("\nYou run at the man, but he dances lightly away, easily dodging your wild swing.")
                print("He slashes at you with two of the sabers, and they bite into the wooden haft of your axe!!  You only narrowly back away in time to dodge the other two swords, as he arcs them towards you.")
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou decide that fighting this man creature isn't worth it, and you retreat back the way you came, closing the door and sealing you off from attack.")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()

        elif door == "right" and weapon == "axe":
            if fight_flight == "fight":
                print("\nYou try taking a swing at the serpent, but it bobs it's head out of the way, easily avoiding your attack.")
                print("You brace yourself for another swing when you hear a gurgling and hissing sound....")
                print("The serpent rears it's head and then suddenly spits a column of boiling water at you!!!")
                print("\nYOU DIED...")
                start_over = input("\nTry again? YES or NO")
                start_over = start_over.lower()
                if start_over == "yes":
                    my_word_game()
                else:
                    sys.exit
                
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou decide that maybe fighting this monster with your current weapon isn't a good idea, and you quickly dash out of their, avoiding what perhaps might have been an untimely death...")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()



    # Weapon Spear
        if door == "left" and weapon == "spear":
            if fight_flight == "fight":
                print("\nYou sidestep, narrowly dodging a wild swing from this monstrous brute, and then lunge forward, burying the spear point deep in it's chest!")
                print("You jerk the weapon free, as the minotaur stumbles and falls to its knees, breathing heavy, painful groans...")
                print("It slowly sinks to the floor, breathing a last gasp of air before dying, conquered by a fearsome knight.")
                # At this point you can search for any doors or treasure in the room, and choose to go back to the other doors to explore...
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou dash away from his razor sharp axe, narrowly missing the blade by centimeters!!")
                print("You sprint out of the room, down the hall, and slam the door shut...")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()

        elif door == "middle" and weapon == "spear":
            if fight_flight == "fight":
                print("\nYou decide to keep the swordsman at a distance, circling around him, keeping your guard up.  He slowly approaches, judging you carefully...")
                print("All of a sudden he lunges at you, swirling the 4 sabers in a wheel of light!!  You duck down, and thrust the spear forward, feeling impact down the length of the shaft.")
                print("You look up and see the spear driven into the upper leg of the creature, disabling all further advances.")
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou decide that fighting this man creature isn't worth it, and you retreat back the way you came, closing the door and sealing you off from attack.")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()

        elif door == "right" and weapon == "spear":
            if fight_flight == "fight":
                print("\nThe serpent screeches in anger and dives towards you, fanged jaws snapping at your head!!")
                print("You drop to your knees, avoiding a violent end, only to roll out of the way from another swift attack!")
                print("You quickly come to your feet, and brace yourself.  You take your aim and throw the spear, arcing viciously through the air, driving into the eye of the ")
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou run away from the serpent in fear, going back the same way that you came...")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()



    # Weapon Sword
        if door == "left" and weapon == "sword":
            if fight_flight == "fight":
                print("\nYou dive to the side, avoiding the first strike of the beast.  You come to a standstill and then swing the sword in an upwards arc towards its throat!")
                print("CLANG!!!  Your sword is ground to a resounding halt mid-swing, as the minotaur's heavy axe intercepts the blow...")
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou dash away from his razor sharp axe, narrowly missing the blade by centimeters!!")
                print("You sprint out of the room, down the hall, and slam the door shut...")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()

        elif door == "middle" and weapon == "sword":
            if fight_flight == "fight":
                print("\nYou rush in and take stock of the situation.  The man starts to circle roud about, looking for weaknesses in your defense.")
                print("You feint to the side, and he takes the bait!  You then swing a reverse cut at him and cut one of his left arms clean off!!")
                print("He yells in pain, slowed by the shock of the wound, but still not deterred...")
                fight_loop = 1
            elif fight_flight == "run":
                print("\nYou decide that fighting this man creature isn't worth it, and you retreat back the way you came, closing the door and sealing you off from attack.")
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()

        elif door == "right" and weapon == "sword":
            if fight_flight == "fight":
                print("\nYou approach the vicious maw facing you, ready to defend against the razor sharp teeth...but the attack never comes...")
                print("A jet of boiling water hits you square in the chest, melting your armor and killing you instantly before you can even raise your sword...")
                print("\nYOU DIED...")
                start_over = input("\nTry again? YES or NO")
                start_over = start_over.lower()
                if start_over == "yes":
                    my_word_game()
                else:
                    sys.exit
                    
                fight_loop = 1
            elif fight_flight == "run":
                print()
                fight_loop = 1
            else:
                print("\nI'm sorry, that isn't an option...")
                fight_flight = input("Do you choose to FIGHT or RUN?")
                fight_flight = fight_flight.lower()


my_word_game()