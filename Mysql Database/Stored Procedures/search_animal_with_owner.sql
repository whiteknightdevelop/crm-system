CREATE DEFINER=`root`@`localhost` PROCEDURE `search_animal_with_owner`(
IN animal_name VARCHAR(45), IN animal_type VARCHAR(45), IN animal_breed VARCHAR(45), IN animal_color VARCHAR(45),
IN animal_gender VARCHAR(45), IN animal_chip_number VARCHAR(45))
BEGIN
    SELECT animal_id, animal_table.owner_id, name, type, breed, color, gender, chip_number, first_name, last_name, city, street, phone, email
    FROM
        (SELECT * FROM petadmin.animals
        WHERE
        ((animal_name IS NULL) OR (animal_name = '') OR (name LIKE CONCAT(animal_name,'%')))
        AND ((animal_type IS NULL) OR (animal_type = '') OR (type LIKE CONCAT(animal_type,'%')))
        AND ((animal_breed IS NULL) OR (animal_breed = '') OR (breed LIKE CONCAT(animal_breed,'%')))
        AND ((animal_color IS NULL) OR (animal_color = '') OR (color LIKE CONCAT(animal_color,'%')))
        AND ((animal_gender IS NULL) OR (animal_gender = '') OR (gender LIKE CONCAT(animal_gender,'%')))
        AND ((animal_chip_number IS NULL) OR (animal_chip_number  = '') OR (chip_number LIKE CONCAT(animal_chip_number ,'%')))
        AND (animal_archive = 0)) AS animal_table
    LEFT JOIN
    petadmin.owners
    ON animal_table.owner_id = petadmin.owners.owner_id;
END