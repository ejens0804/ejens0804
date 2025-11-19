-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema film
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `film` ;

-- -----------------------------------------------------
-- Schema film
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `film` DEFAULT CHARACTER SET utf8 ;
USE `film` ;

-- -----------------------------------------------------
-- Table `film`.`rating`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`rating` ;

CREATE TABLE IF NOT EXISTS `film`.`rating` (
  `rating_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `rating_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`rating_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`movie_title`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`movie_title` ;

CREATE TABLE IF NOT EXISTS `film`.`movie_title` (
  `movie_title_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `movie_title_name` VARCHAR(35) NOT NULL,
  `release_year` SMALLINT UNSIGNED NOT NULL,
  `rating_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`movie_title_id`),
  INDEX `fk_movie_title_rating1_idx` (`rating_id` ASC) VISIBLE,
  CONSTRAINT `fk_movie_title_rating1`
    FOREIGN KEY (`rating_id`)
    REFERENCES `film`.`rating` (`rating_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`media_type`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`media_type` ;

CREATE TABLE IF NOT EXISTS `film`.`media_type` (
  `media_type_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `media_type_name` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`media_type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`movie_title_media_type`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`movie_title_media_type` ;

CREATE TABLE IF NOT EXISTS `film`.`movie_title_media_type` (
  `movie_title_id` INT UNSIGNED NOT NULL,
  `media_type_id` INT UNSIGNED NOT NULL,
  `price` DECIMAL(4,2) UNSIGNED NULL,
  PRIMARY KEY (`movie_title_id`, `media_type_id`),
  INDEX `fk_movie_title_media_type_media_type1_idx` (`media_type_id` ASC) VISIBLE,
  INDEX `fk_movie_title_media_type_movie_title1_idx` (`movie_title_id` ASC) VISIBLE,
  CONSTRAINT `fk_movie_title_media_type_movie_title1`
    FOREIGN KEY (`movie_title_id`)
    REFERENCES `film`.`movie_title` (`movie_title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_movie_title_media_type_media_type1`
    FOREIGN KEY (`media_type_id`)
    REFERENCES `film`.`media_type` (`media_type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`special_features`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`special_features` ;

CREATE TABLE IF NOT EXISTS `film`.`special_features` (
  `special_features_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `special_features_type` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`special_features_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`special_features_movie_title`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`special_features_movie_title` ;

CREATE TABLE IF NOT EXISTS `film`.`special_features_movie_title` (
  `special_features_id` INT UNSIGNED NOT NULL,
  `movie_title_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`special_features_id`, `movie_title_id`),
  INDEX `fk_special_features_movie_title_movie_title1_idx` (`movie_title_id` ASC) VISIBLE,
  INDEX `fk_special_features_movie_title_special_features1_idx` (`special_features_id` ASC) VISIBLE,
  CONSTRAINT `fk_special_features_movie_title_special_features1`
    FOREIGN KEY (`special_features_id`)
    REFERENCES `film`.`special_features` (`special_features_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_special_features_movie_title_movie_title1`
    FOREIGN KEY (`movie_title_id`)
    REFERENCES `film`.`movie_title` (`movie_title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`actors`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`actors` ;

CREATE TABLE IF NOT EXISTS `film`.`actors` (
  `actors_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `actors_fname` VARCHAR(15) NOT NULL,
  `actors_lname` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`actors_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`actors_movie_title`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`actors_movie_title` ;

CREATE TABLE IF NOT EXISTS `film`.`actors_movie_title` (
  `actors_id` INT UNSIGNED NOT NULL,
  `movie_title_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`actors_id`, `movie_title_id`),
  INDEX `fk_actors_movie_title_movie_title1_idx` (`movie_title_id` ASC) VISIBLE,
  INDEX `fk_actors_movie_title_actors1_idx` (`actors_id` ASC) VISIBLE,
  CONSTRAINT `fk_actors_movie_title_actors1`
    FOREIGN KEY (`actors_id`)
    REFERENCES `film`.`actors` (`actors_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_actors_movie_title_movie_title1`
    FOREIGN KEY (`movie_title_id`)
    REFERENCES `film`.`movie_title` (`movie_title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`genre`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`genre` ;

CREATE TABLE IF NOT EXISTS `film`.`genre` (
  `genre_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `genre_name` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`genre_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`genre_movie_title`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`genre_movie_title` ;

CREATE TABLE IF NOT EXISTS `film`.`genre_movie_title` (
  `genre_id` INT UNSIGNED NOT NULL,
  `movie_title_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`genre_id`, `movie_title_id`),
  INDEX `fk_genre_movie_title_movie_title1_idx` (`movie_title_id` ASC) VISIBLE,
  INDEX `fk_genre_movie_title_genre1_idx` (`genre_id` ASC) VISIBLE,
  CONSTRAINT `fk_genre_movie_title_genre1`
    FOREIGN KEY (`genre_id`)
    REFERENCES `film`.`genre` (`genre_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_genre_movie_title_movie_title1`
    FOREIGN KEY (`movie_title_id`)
    REFERENCES `film`.`movie_title` (`movie_title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`studio`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`studio` ;

CREATE TABLE IF NOT EXISTS `film`.`studio` (
  `studio_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `studio_name` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`studio_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `film`.`studio_movie_title`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `film`.`studio_movie_title` ;

CREATE TABLE IF NOT EXISTS `film`.`studio_movie_title` (
  `studio_id` INT UNSIGNED NOT NULL,
  `movie_title_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`studio_id`, `movie_title_id`),
  INDEX `fk_studio_movie_title_movie_title1_idx` (`movie_title_id` ASC) VISIBLE,
  INDEX `fk_studio_movie_title_studio1_idx` (`studio_id` ASC) VISIBLE,
  CONSTRAINT `fk_studio_movie_title_studio1`
    FOREIGN KEY (`studio_id`)
    REFERENCES `film`.`studio` (`studio_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_studio_movie_title_movie_title1`
    FOREIGN KEY (`movie_title_id`)
    REFERENCES `film`.`movie_title` (`movie_title_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

USE film;

-- Rating First
INSERT INTO rating (rating_id, rating_name)
VALUES 
(DEFAULT, 'G'), -- 1
(DEFAULT, 'PG'), -- 2
(DEFAULT, 'PG-13'); -- 3

INSERT INTO movie_title (movie_title_id, movie_title_name, release_year, rating_id)
VALUES 
(DEFAULT, 'Toy Story', 1995, 1), -- 1
(DEFAULT, 'Toy Story 2', 1999, 1), -- 2
(DEFAULT, 'Brigadoone', 1954, 1), -- 3
(DEFAULT, 'The Empire Strikes Back', 1977, 2), -- 4
(DEFAULT, 'Coda', 2021, 3), -- 5
(DEFAULT, 'Guardians of the Galaxy', 2014, 3); -- 6

INSERT INTO actors (actors_fname, actors_lname) -- If the id is auto incrementing you don't need to write it in
VALUES
('Tom', 'Hanks'), -- 1
('Tim', 'Allen'), -- 2
('Annie','Potts'), -- 3
('John','Ratzenberger'), -- 4
('Gene','Kelly'), -- 5
('Cyd','Charisse'), -- 6
('Van','Johnson'), -- 7
('Harrison','Ford'), -- 8
('Carrie','Fisher'), -- 9
('Mark','Hamill'), -- 10
('Emilia','Jones'), -- 11
('Marlee','Matlin'), -- 12
('Troy','Kotsur'), -- 13
('Chris','Pratt'), -- 14
('Zoe','Saldana'), -- 15
('Dave','Bautista'), -- 16
('Vin','Diesel'), -- 17
('Bradley','Cooper'), -- 18
('Lee','Pace'); -- 19

INSERT INTO media_type (media_type_name)
VALUES
('DVD'), -- 1
('Blu-ray'), -- 2
('Streaming'), -- 3
('4K'); -- 4

INSERT INTO studio (studio_name)
VALUES
('Pixar'), -- 1
('MGM'), -- 2
('20th Century Fox'), -- 3
('Apple TV+'), -- 4
('Marvel'), -- 5
('Disney'); -- 6

INSERT INTO special_features (special_features_type)
VALUES
('Bloopers'), -- 1
('Actor Interviews'), -- 2
('Cut Scenes'), -- 3
('Trailers'), -- 4
('Extended Scenes'), -- 5
('Deleted Scenes'); -- 6

INSERT INTO genre (genre_name)
VALUES
('Family'), -- 1
('Animated'), -- 2
('Musical'), -- 3
('Romance'), -- 4
('Sci Fi'), -- 5
('Comedy'), -- 6
('Drama'), -- 7
('Music'), -- 8
('Action'), -- 9
('Adventure'); -- 10

INSERT INTO genre_movie_title (genre_id, movie_title_id)
VALUES 
(1,1), -- First movie, Family 
(2,1), -- First movie, Animated
(1,2), -- Second movie, Family
(2,2), -- Second movie, Animated
(3,3), -- Third movie, Musical
(4,3), -- Third movie, Romance
(5,4), -- Fourth movie, Sci Fi
(6,5), -- Fifth movie, Comedy
(7,5), -- Fifth movie, Drama
(8,5), -- Fifth movie, Music
(5,6), -- Sixth movie, Sci Fi
(6,6), -- Sixth movie, Comedy
(9,6), -- Sixth movie, Action
(10,6); -- Sixth movie, Adventure

INSERT INTO studio_movie_title (studio_id, movie_title_id)
VALUES
(1,1), -- First movie, Pixar
(1,2), -- Second movie, Pixar
(2,3), -- Third movie, MGM
(3,4), -- Fourth movie, 20th Century Fox
(4,5), -- Fifth movie, Apple TV+
(5,6), -- Sixth movie, Marvel
(6,6); -- Sixth movie, Disney

INSERT INTO special_features_movie_title (special_features_id, movie_title_id)
VALUES
(1,1), -- First movie, Bloopers
(2,2), -- Second movie, Actor Interviews
-- Third movie doesn't have any special features
(3,4), -- Fourth movie, Cut Scenes
(1,4), -- Fourth movie, Bloopers
(4,5), -- Fifth movie, Trailers
(5,6), -- Sixth movie, Extended Scenes
(6,6), -- Sixth movie, Deleted Scenes
(4,6); -- Sixth movie, Trailers

INSERT INTO movie_title_media_type (movie_title_id, media_type_id, price)
VALUES
(1, 1, 19.95), -- First movie, DVD, $19.95
(2, 1, 24.95), -- Second movie, DVD, $24.95
(3, 1, 19.95), -- Third movie, DVD, $19.95
(4, 2, 35.00), -- Fourth movie, Blu-ray, $35.00
(5, 3, null), -- Fifth movie, Streaming, NO PRICE
(6, 4, 21.95), -- Sixth movie, 4K, $21.95
(6, 2, 19.95); -- Sixth movie, Blu-ray, $19.95

INSERT INTO actors_movie_title (actors_id, movie_title_id)  
VALUES  
(1, 1), (2, 1), (3, 1), (4, 1),  
(2, 2), (1, 2), (4, 2), (3, 2),  
(5, 3), (6, 3), (7, 3),  
(8, 4), (9, 4), (10, 4),  
(11, 5), (12, 5), (13, 5),  
(14, 6), (15, 6), (16, 6), (17, 6), (18, 6), (19, 6);

-- SELECT *
-- FROM movie_title;

-- SELECT *
-- FROM movie_title
-- WHERE movie_title_id = 3;

-- UPDATE movie_title
-- SET movie_title_name = 'Brigadoone'
-- WHERE movie_title_id = 3;
	