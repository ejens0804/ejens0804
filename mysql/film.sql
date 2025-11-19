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
