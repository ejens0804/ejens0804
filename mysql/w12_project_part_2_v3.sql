-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema university_db
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `university_db` ;

-- -----------------------------------------------------
-- Schema university_db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `university_db` DEFAULT CHARACTER SET utf8mb3 ;
USE `university_db` ;

-- -----------------------------------------------------
-- Table `university_db`.`department`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`department` ;

CREATE TABLE IF NOT EXISTS `university_db`.`department` (
  `department_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `department_name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`department_id`),
  UNIQUE INDEX `department_name_UNIQUE` (`department_name` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `university_db`.`degree`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`degree` ;

CREATE TABLE IF NOT EXISTS `university_db`.`degree` (
  `degree_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `degree_name` VARCHAR(100) NOT NULL,
  `department_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`degree_id`),
  INDEX `fk_degree_department_idx` (`department_id` ASC) VISIBLE,
  CONSTRAINT `fk_degree_department`
    FOREIGN KEY (`department_id`)
    REFERENCES `university_db`.`department` (`department_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `university_db`.`course`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`course` ;

CREATE TABLE IF NOT EXISTS `university_db`.`course` (
  `course_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `course_code` VARCHAR(10) NOT NULL,
  `course_num` VARCHAR(10) NOT NULL,
  `course_title` VARCHAR(100) NOT NULL,
  `credits` INT UNSIGNED NOT NULL,
  `degree_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`course_id`),
  UNIQUE INDEX `course_code_num_UNIQUE` (`course_code` ASC, `course_num` ASC) VISIBLE,
  INDEX `fk_course_degree1_idx` (`degree_id` ASC) VISIBLE,
  CONSTRAINT `fk_course_degree1`
    FOREIGN KEY (`degree_id`)
    REFERENCES `university_db`.`degree` (`degree_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `university_db`.`person`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`person` ;

CREATE TABLE IF NOT EXISTS `university_db`.`person` (
  `person_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `first_name` VARCHAR(50) NOT NULL,
  `last_name` VARCHAR(50) NOT NULL,
  `gender` CHAR(1) NOT NULL,
  `city` VARCHAR(50) NOT NULL,
  `state` CHAR(2) NOT NULL,
  `birthdate` DATE NOT NULL,
  PRIMARY KEY (`person_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `university_db`.`role_type`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`role_type` ;

CREATE TABLE IF NOT EXISTS `university_db`.`role_type` (
  `role_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `role_name` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`role_id`),
  UNIQUE INDEX `role_name_UNIQUE` (`role_name` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `university_db`.`term`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`term` ;

CREATE TABLE IF NOT EXISTS `university_db`.`term` (
  `term_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `year` INT UNSIGNED NOT NULL,
  `term_name` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`term_id`),
  UNIQUE INDEX `year_term_UNIQUE` (`year` ASC, `term_name` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `university_db`.`section`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`section` ;

CREATE TABLE IF NOT EXISTS `university_db`.`section` (
  `section_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `section_number` INT UNSIGNED NOT NULL,
  `capacity` INT UNSIGNED NOT NULL,
  `course_id` INT UNSIGNED NOT NULL,
  `term_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`section_id`),
  UNIQUE INDEX `course_term_section_UNIQUE` (`course_id` ASC, `term_id` ASC, `section_number` ASC) VISIBLE,
  INDEX `fk_section_course_idx` (`course_id` ASC) VISIBLE,
  INDEX `fk_section_term_idx` (`term_id` ASC) VISIBLE,
  CONSTRAINT `fk_section_course`
    FOREIGN KEY (`course_id`)
    REFERENCES `university_db`.`course` (`course_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_section_term`
    FOREIGN KEY (`term_id`)
    REFERENCES `university_db`.`term` (`term_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `university_db`.`person_section_role`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `university_db`.`person_section_role` ;

CREATE TABLE IF NOT EXISTS `university_db`.`person_section_role` (
  `enrollment_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `person_id` INT UNSIGNED NOT NULL,
  `section_id` INT UNSIGNED NOT NULL,
  `role_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`enrollment_id`),
  UNIQUE INDEX `person_section_role_UNIQUE` (`person_id` ASC, `section_id` ASC, `role_id` ASC) VISIBLE,
  INDEX `fk_enrollment_person_idx` (`person_id` ASC) VISIBLE,
  INDEX `fk_enrollment_section_idx` (`section_id` ASC) VISIBLE,
  INDEX `fk_enrollment_role_idx` (`role_id` ASC) VISIBLE,
  CONSTRAINT `fk_enrollment_person`
    FOREIGN KEY (`person_id`)
    REFERENCES `university_db`.`person` (`person_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_enrollment_role`
    FOREIGN KEY (`role_id`)
    REFERENCES `university_db`.`role_type` (`role_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `fk_enrollment_section`
    FOREIGN KEY (`section_id`)
    REFERENCES `university_db`.`section` (`section_id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -- Insert initial role types
INSERT INTO `university_db`.`role_type` (`role_id`, `role_name`) VALUES (1, 'Teacher');
INSERT INTO `university_db`.`role_type` (`role_id`, `role_name`) VALUES (2, 'Student');
INSERT INTO `university_db`.`role_type` (`role_id`, `role_name`) VALUES (3, 'TA');

INSERT INTO `university_db`.`department` (`department_id`, `department_name`) VALUES 
(1, 'Computer Science and Engineering'),
(2, 'Mathematics'),
(3, 'Music'),
(4, 'Web Design and Development');

INSERT INTO `university_db`.`degree` (`degree_id`, `degree_name`, `department_id`) VALUES 
(1, 'Computer Science', 1),
(2, 'Web Design and Development', 4),
(3, 'Data Science', 2),
(4, 'Organ Performance', 3);

INSERT INTO `university_db`.`course` (`course_id`, `course_code`, `course_num`, `course_title`, `credits`, `degree_id`) VALUES 
(1, 'CSE', '251', 'Parallelism and Concurrency', 3, 1),
(2, 'WDD', '231', 'Web Frontend Development I', 2, 2),
(3, 'MATH', '113', 'Calculus II', 3, 3),
(4, 'MUSIC', '213', 'Musicianship 4', 3, 4);

INSERT INTO `university_db`.`term` (`term_id`, `year`, `term_name`) VALUES 
(1, 2024, 'Fall'),
(2, 2025, 'Winter');

-- Teachers
INSERT INTO `university_db`.`person` (`person_id`, `first_name`, `last_name`, `gender`, `city`, `state`, `birthdate`) VALUES 
(1, 'Brady', 'Meyer', 'M', 'Unknown', 'ID', '1980-01-01'),
(2, 'Andy', 'Kipner', 'M', 'Unknown', 'ID', '1980-01-01'),
(3, 'Lucy', 'Fuller', 'F', 'Unknown', 'ID', '1980-01-01'),
(4, 'Adam', 'Woods', 'M', 'Unknown', 'ID', '1980-01-01'),
(5, 'Bryan', 'Drew', 'M', 'Unknown', 'ID', '1980-01-01');

-- Students
INSERT INTO `university_db`.`person` (`person_id`, `first_name`, `last_name`, `gender`, `city`, `state`, `birthdate`) VALUES 
(6, 'Marshall', 'Spence', 'M', 'Garland', 'TX', '2000-06-23'),
(7, 'Maria', 'Clark', 'F', 'Akron', 'OH', '2002-01-25'),
(8, 'Tracy', 'Woodward', 'F', 'Newark', 'NJ', '2002-10-04'),
(9, 'Erick', 'Woodward', 'M', 'Newark', 'NJ', '1998-08-05'),
(10, 'Lillie', 'Summers', 'F', 'Reno', 'NV', '1999-11-05'),
(11, 'Nellie', 'Marquez', 'F', 'Atlanta', 'GA', '2001-06-25'),
(12, 'Allen', 'Stokes', 'M', 'Bozeman', 'MT', '2004-09-16'),
(13, 'Josh', 'Rollins', 'M', 'Decatur', 'TN', '1998-11-28'),
(14, 'Isabel', 'Meyers', 'F', 'Rexburg', 'ID', '2003-05-15'),
(15, 'Kerri', 'Shah', 'F', 'Mesa', 'AZ', '2003-04-05');

INSERT INTO `university_db`.`section` (`section_id`, `section_number`, `capacity`, `course_id`, `term_id`) VALUES 
(1, 1, 35, 1, 1),  -- CSE 251 Fall 2024
(2, 1, 30, 2, 1),  -- WDD 231 Fall 2024 Section 1
(3, 2, 30, 2, 1),  -- WDD 231 Fall 2024 Section 2
(4, 1, 45, 3, 1),  -- MATH 113 Fall 2024
(5, 1, 25, 4, 1),  -- MUSIC 213 Fall 2024
(6, 2, 35, 1, 2),  -- CSE 251 Winter 2025 Section 2
(7, 3, 35, 1, 2),  -- CSE 251 Winter 2025 Section 3
(8, 1, 30, 2, 2),  -- WDD 231 Winter 2025 Section 1
(9, 2, 40, 2, 2),  -- WDD 231 Winter 2025 Section 2
(10, 1, 25, 4, 2); -- MUSIC 213 Winter 2025

INSERT INTO `university_db`.`person_section_role` (`person_id`, `section_id`, `role_id`) VALUES
(1, 1, 1),  -- Brady Meyer CSE 251 Fall 2024 Section 1
(1, 6, 1),  -- Brady Meyer CSE 251 Winter 2025 Section 2
(2, 2, 1),  -- Andy Kipner WDD 231 Fall 2024 Section 1
(2, 3, 1),  -- Andy Kipner WDD 231 Fall 2024 Section 2
(2, 8, 1),  -- Andy Kipner WDD 231 Winter 2025 Section 1
(2, 9, 1),  -- Andy Kipner WDD 231 Winter 2025 Section 2
(3, 4, 1),  -- Lucy Fuller MATH 113 Fall 2024
(4, 5, 1),  -- Adam Woods MUSIC 213 Fall 2024
(4, 10, 1), -- Adam Woods MUSIC 213 Winter 2025
(5, 7, 1),  -- Bryan Drew CSE 251 Winter 2025 Section 3
(6, 1, 2), -- Marshall Spence CSE 251 Fall 2024 Section 1
(6, 9, 2), -- Marshall Spence CSE 251 Fall 2024 Section 2
(7, 4, 2), -- Maria Clark MATH 113 Fall 2024 Section 1
(8, 4, 2), -- Tracy Woodward Math 113 Fall 2024 Section 1
(9, 5, 2), -- Erick Woodward MUSIC 213 Fall 2024 Section 1
(10, 4, 2), -- Lillie Summers MATH 113 Fall 2024 Section 1
(10, 5, 3), -- Lillie Summers MUSIC 213 Fall 2024 Section 1
(11, 7, 2), -- Nellie Marquez CSE 251 Winter 2025 Section 3
(12, 6, 2), -- Allen Stokes CSE 251 Winter 2025 Section 2 
(12, 8, 3), -- Allen Stokes WDD 231 Winter 2025 Section 1
(12, 10, 2), -- Allen Stokes MUSIC 213 Winter 2025 Section 1
(13, 9, 2), -- Josh Rollins WDD 231 Winter 2025 Section 2
(14, 9, 2), -- Isabel Meyers WDD 231 Winter 2025 Section 2
(15, 6, 2); -- Kerri Shah CSE 251 Winter 2025 Section 2


USE university_db;

-- Query 1
SELECT first_name, last_name, DATE_FORMAT(birthdate, '%M %d, %Y') AS 'November Birthdays'
FROM person
WHERE monthname(birthdate) = 'November';

-- Query 2
SELECT DISTINCT last_name, first_name, birthdate AS dob, FLOOR(DATEDIFF('2023-09-11', birthdate)/365) AS Years, MOD(DATEDIFF('2023-09-11', birthdate), 365) AS Days, CONCAT(FLOOR(DATEDIFF('2023-09-11', birthdate)/365), ' - Yrs, ', MOD(DATEDIFF('2023-09-11', birthdate), 365), ' - Days') AS 'Years and Days'
FROM person
INNER JOIN person_section_role psr ON person.person_id = psr.person_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
WHERE role_name = 'Student'
ORDER BY ROUND(DATEDIFF('2023-09-11', birthdate)/365) DESC, MOD(DATEDIFF('2023-09-11', birthdate),365) ASC;

-- Query 3
SELECT DISTINCT first_name, last_name, role_name AS person_type
FROM person
INNER JOIN person_section_role psr ON person.person_id = psr.person_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
INNER JOIN section ON psr.section_id = section.section_id
INNER JOIN course ON course.course_id = section.course_id
WHERE course.course_id = 1
ORDER BY role_name; 

-- Query 4
SELECT first_name, last_name, role_name AS person_type, course_title AS course_name
FROM person
INNER JOIN person_section_role psr ON person.person_id = psr.person_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
INNER JOIN section ON psr.section_id = section.section_id
INNER JOIN course ON course.course_id = section.course_id
WHERE role_name = 'TA';

-- Query 5
SELECT first_name, last_name, term_name 
FROM person
INNER JOIN person_section_role psr ON person.person_id = psr.person_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
INNER JOIN section ON psr.section_id = section.section_id
INNER JOIN course ON course.course_id = section.course_id
INNER JOIN term ON section.term_id = term.term_id
WHERE course.course_id = 4 AND (term_name = 'Fall' OR term_name = 'Winter') AND role_name = 'Student'
ORDER BY last_name; 

-- Query 6
SELECT course.course_code, course.course_num AS 'course_number', course.course_title AS 'course_name', section.section_number, term.term_name
FROM person
INNER JOIN person_section_role psr ON person.person_id = psr.person_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
INNER JOIN section ON psr.section_id = section.section_id
INNER JOIN course ON course.course_id = section.course_id
INNER JOIN term ON section.term_id = term.term_id
WHERE first_name = "Brady" AND role_name = "Teacher"
ORDER BY term_name, section_number;

-- Query 7
SELECT DISTINCT term_name, term.year AS term_year, count(psr.enrollment_id) AS Enrollment
FROM term
INNER JOIN section ON section.term_id = term.term_id
INNER JOIN person_section_role psr ON psr.section_id = section.section_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
INNER JOIN person ON person.person_id = psr.person_id
WHERE term_name = 'Fall' AND term.year = 2024 AND role_type.role_name = 'Student';

-- Query 8  (I think that the query in the assignment is wrong because there are 4 departments, but in the assignment it says that there's only 3)
SELECT department.department_name, COUNT(course_id) AS 'Courses'
FROM department
INNER JOIN degree ON degree.department_id = department.department_id
INNER JOIN course ON degree.degree_id = course.degree_id
GROUP BY department_name;

-- Query 9
SELECT person.first_name, person.last_name, SUM(section.capacity) AS TeachingCapacity
FROM person
INNER JOIN person_section_role psr ON person.person_id = psr.person_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
INNER JOIN section ON psr.section_id = section.section_id
INNER JOIN course ON course.course_id = section.course_id
INNER JOIN term ON section.term_id = term.term_id
WHERE role_name = 'Teacher' AND term_name = 'Fall'
GROUP BY person.first_name, person.last_name
ORDER BY SUM(section.capacity);

-- Query 10
SELECT DISTINCT person.last_name, person.first_name, SUM(course.credits) AS Credits
FROM person 
INNER JOIN person_section_role psr ON person.person_id = psr.person_id
INNER JOIN role_type ON role_type.role_id = psr.role_id
INNER JOIN section ON psr.section_id = section.section_id
INNER JOIN course ON course.course_id = section.course_id
INNER JOIN term ON section.term_id = term.term_id
WHERE (term_name = 'Winter') AND (role_name = 'Student')
GROUP BY person.person_id, person.last_name, person.first_name
HAVING SUM(course.credits) <= 3
ORDER BY SUM(course.credits) DESC
LIMIT 4;