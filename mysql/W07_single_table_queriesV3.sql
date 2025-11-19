-- Query 1
USE v_art;
INSERT INTO artist (artist_id, fname, lname, dob, dod, country, local)
values
(10, 'George', 'Washington', 1732, 1798, 'United States', 'y');

SELECT *
FROM artist; -- Used to check the result


 --  Query 2
 SELECT fname, mname, lname, dob, dod, country, local
 FROM artist
 ORDER BY lname ASC;
 
 
 -- Query 3
 UPDATE artist
 SET dod = 1799
 WHERE artist_id = 10;
 
SELECT *
FROM artist; -- Used to check the result

 
-- Query 4 
DELETE
FROM artist
WHERE artist_id = 10;

SELECT *
FROM artist; -- Used to check the result


-- Query 5
USE bike;
SELECT first_name, last_name, phone
FROM customer
WHERE phone IS NOT NULL;


-- Query 6
SELECT product_name, list_price, list_price - 100 AS DiscountPrice
FROM product
WHERE list_price >= 7000
ORDER BY list_price DESC;


-- Query 7 
SELECT first_name, last_name, phone
FROM staff
WHERE store_id = 3;


-- Query 8
SELECT product_name, model_year, list_price
FROM product
WHERE product_name LIKE '%Crockett%';


-- Query 9
SELECT product_name, list_price 
FROM product
WHERE list_price BETWEEN 100 and 250
ORDER BY list_price ASC;


-- Query 10
 SELECT first_name, last_name, phone, street, city, state, zip_code
 FROM customer
 WHERE (phone IS NOT NULL AND (city LIKE '%ton%' OR city LIKE 'field')) or last_name = 'Mccall'
 LIMIT 5;