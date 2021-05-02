CREATE DEFINER=`root`@`localhost` PROCEDURE `add_animal`(IN in_owner_id INT(10),  IN in_name VARCHAR(45), IN in_type VARCHAR(45), IN in_breed VARCHAR(45),
IN in_color VARCHAR(45), IN in_gender VARCHAR(45), IN in_date_of_birth DATETIME, IN in_sterilized TINYINT(1),
IN in_date_of_sterilization DATETIME, IN in_chip_number VARCHAR(45), IN in_chip_mark_date DATETIME, IN in_comment VARCHAR(501), IN in_animal_user_id  INT(10))

BEGIN
    INSERT INTO petadmin.animals (owner_id, created_date, name, type, breed, color, gender, date_of_birth, active, sterilized,
    date_of_sterilization, chip_number, chip_mark_date, comment, animal_user_id, animal_archive)
    values (in_owner_id, CURRENT_TIMESTAMP(), in_name, in_type, in_breed, in_color, in_gender, in_date_of_birth, true, in_sterilized,
    in_date_of_sterilization, in_chip_number, in_chip_mark_date, in_comment, in_animal_user_id, false);
    SELECT LAST_INSERT_ID();
END