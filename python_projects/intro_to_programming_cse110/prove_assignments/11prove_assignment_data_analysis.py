year_of_interest = int(input("Enter the year of interest: "))

max_life_expectancy = float('-inf')
min_life_expectancy = float('inf')
min_country = ""
min_year = 0
max_country = ""
max_year = 0
counter = 0
total_life_expectancy = 0
max_life_expectancy_year_of_interest = float('-inf')
min_life_expectancy_year_of_interest = float('inf')
interest_min_country = ""
interest_max_country = ""
interest_min_year = 0
interest_max_year = 0

with open("prove_assignments/life-expectancy.csv") as life_file:
    next(life_file)
    for line in life_file:
        parts = line.strip().split(',')
        country = parts[0]
        year = int(parts[2])
        life_expectancy = float(parts[3])
        if year == year_of_interest:
            total_life_expectancy += life_expectancy
            counter += 1 
        
        if life_expectancy < min_life_expectancy:
            min_life_expectancy = life_expectancy
            min_country = country
            min_year = year
            
        if life_expectancy > max_life_expectancy:
            max_life_expectancy = life_expectancy
            max_country = country
            max_year = year

        if year == year_of_interest and life_expectancy < min_life_expectancy_year_of_interest:
            min_life_expectancy_year_of_interest = life_expectancy
            interest_min_country = country
            interest_min_year = year
        
        if year == year_of_interest and life_expectancy > max_life_expectancy_year_of_interest:
            max_life_expectancy_year_of_interest = life_expectancy
            interest_max_country = country
            interest_max_year = year
            
average_life_expectancy = total_life_expectancy / counter

print(f"\nOverall maximum life expectancy: {max_life_expectancy} from {max_country} in {max_year}")
print(f"Overall minimum life expectancy: {min_life_expectancy} from {min_country} in {min_year}\n")

print(f"For the year {year_of_interest}:")
print(f"The average life expectancy across all countries was {round(average_life_expectancy,2)}")
print(f"The max life expectancy was in {interest_max_country} with {max_life_expectancy_year_of_interest}")
print(f"The min life expectancy was in {interest_min_country} with {min_life_expectancy_year_of_interest}\n")