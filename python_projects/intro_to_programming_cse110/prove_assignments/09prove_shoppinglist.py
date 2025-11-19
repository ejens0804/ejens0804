# Final Program Requirements:
# 1 Add a new item
# 2 Display the contents of the shopping cart
# 3 Remove an item 
# 4 Compute the total
# 5 Quit

# Milestone Requirements:
# 1 Have menu system that repeats until the user chooses quit
# 2 Create a list that will store the names of the items in the shopping cart
# 3 Complete the option to add the name of the item to the list
# 4 Complete the option to display the names of the items in the list



# Extra Stuff I want to add
# Implement a budget option and inform the user if the total is higher or lower than their budget
# If the total is higher than their budget prompt the user to remove items from the list


import time
import locale
locale.setlocale(locale.LC_ALL, "en_US.UTF-8")

item_name = []
item_price = []

print("Welcome to the Shopping Cart Program!")

budget_max = float(input("\nWhat is your budget before tax? Enter a number: "))
budget_with_tax = input("Would you like to include tax in your budget? (yes or no): ")

while budget_with_tax != "yes" and budget_with_tax != "no":
    print("Invalid input. Please enter yes or no.")
    budget_with_tax = input("Would you like to include tax in your budget? (yes or no): ")

if budget_with_tax == "yes":
    budget_max = float(round(budget_max * 1.06 , 2))
    print(f"Your budget is: {locale.currency(budget_max)}")
elif budget_with_tax == "no":
    budget_max = budget_max



print("\nPlease select one of the following options:")
print("1. Add item")
print("2. View cart")
print("3. Remove item")
print("4. Compute total")
print("5. Quit")
input_option = int(input("Please enter an option (Use Numbers):"))


while input_option != 5:
# Add item
    if input_option == 1:
        item_name.append(input("What is the item name? "))
        item_price.append(float(input("What is the item price? ")))
        print("Item added to cart!")
        total = 0
        for i in item_price:
            total += i
        if budget_max < total:
            print("\nYou are over budget! Consider removing items from your cart")
        elif budget_max == total:
            print("You are at your budget")
            
        
# View cart
    elif input_option == 2:
        for i in range(len(item_name)):
            print(f"{i+1}.{item_name[i]} {locale.currency(item_price[i])}")
        total = 0
        for i in item_price:
            total += i
        if budget_max < total:
            print("\nYou are over budget! Consider removing items from your cart")
        elif budget_max == total:
            print("You are at your budget")
        
        
# Remove item
    elif input_option == 3:
        for i in range(len(item_name)):
            print(f"{i+1}.{item_name[i]} {locale.currency(item_price[i])}")
        item_remove = int(input("What item would you like to remove? "))
        # .pop() removes the item from the list
        item_name.pop(item_remove-1)
        item_price.pop(item_remove-1)
        total = 0
        for i in item_price:
            total += i
        if budget_max < total:
            print("\nYou are over budget! Consider removing items from your cart")
        elif budget_max == total:
            print("You are at your budget")
        
# Compute total
    elif input_option == 4:
        total = 0
        for i in item_price:
            total += i
        print(f"Total price: {locale.currency(total)}")
        print(f"Total price with tax: {locale.currency(total*1.06)}")
        if budget_max < total:
            print("\nYou are over budget! Consider removing items from your cart")
        elif budget_max == total:
            print("You are at your budget")
        
        
    # I added a delay so you can see the output before the program asks for another input
    time.sleep(1.5)
    print("\nPlease select one of the following options (Use Numbers):")
    print("1. Add item")
    print("2. View cart")
    print("3. Remove item")
    print("4. Compute total")
    print("5. Quit")
    input_option = int(input("Please enter an option: "))

print("Thank you for using the Shopping Cart Program!")