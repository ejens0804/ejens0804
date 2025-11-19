import locale
locale.setlocale(locale.LC_ALL, "en_US.UTF-8")
kid_meal_price = float(input("What is the price of a kid's meal?"))
adult_meal_price = float(input("What is the price of an adult's meal?"))
kid_number = int(input("How many kids are there?"))
adult_number = int(input("How many adults are there?"))

sales_tax_rate = float(input("What is the sales tax percentage rate in your state?"))
tip_percentage = float(input("What percentage would you like to tip today?"))

#"Did you get any appetizers?" if yes, how many and how much per appetizer? if no, skip
# Ask user for appetizers, quantity and cost

subtotal = 0
# Need to declare variable names outside of loops and if/elif statements

while subtotal == 0:
    appetizers = input("Did you get any appetizers?").lower()
    if appetizers.__contains__("y"):
        appetizer_quantity = int(input("How many appetizers did you get?"))
        appetizer_cost = float(input("How much was the cost per appetizer?"))
        subtotal = (kid_meal_price*kid_number) + (adult_meal_price*adult_number) + (appetizer_quantity*appetizer_cost)
        break
    elif appetizers.__contains__("n"):
        subtotal = (kid_meal_price*kid_number) + (adult_meal_price*adult_number)
        break
    elif "yes" or "no" not in appetizers:
        print("Please answer with yes or no.")


print("\nSubtotal: " + str(locale.currency(subtotal)))

# Calculate and print sales tax
total_sales_tax = (sales_tax_rate/100)*subtotal
print("Sales Tax: " + str(locale.currency(total_sales_tax)))

# Calculate the total tip and print the value
tip_total = (tip_percentage/100)*subtotal
print("Tip Amount: " + str(locale.currency(tip_total)))

# Calculate the total cost and print the result
total = float(subtotal + total_sales_tax + tip_total)
print("Total: " + str(locale.currency(total))+"\n")

# Ask the user for a payment quantity, and then print the remainder
payment_amount = float(input("What is the payment amount?"))

while payment_amount < total:
    print("Insufficient funds")
    print("You still owe: " + str(locale.currency(total-payment_amount)))
    total = float(total-payment_amount)
    payment_amount = float(input("Please enter additional payment amount."))
    if payment_amount > total:
        break

change = payment_amount-total
print("Change: " + str(locale.currency(change)))
