CREATE DEFINER=`root`@`localhost` PROCEDURE `get_animal_by_visit_id`(IN id_num int(10))
BEGIN
    SET @is_not_deleted = 0;
    SELECT * FROM
        (SELECT petadmin.animals.animal_id, owner_id, created_date, name, type, breed, color, gender, date_of_birth,
        active, sterilized, date_of_sterilization, chip_number, chip_mark_date, petadmin.animals.comment, status, animal_user_id, animal_archive
        FROM petadmin.visits
        INNER JOIN petadmin.animals
        ON petadmin.visits.animal_id=petadmin.animals.animal_id
        WHERE (id_num = visit_id AND visit_archive=@is_not_deleted)) AS table1
    LEFT JOIN petadmin.users ON table1.animal_user_id = petadmin.users.Id;
END