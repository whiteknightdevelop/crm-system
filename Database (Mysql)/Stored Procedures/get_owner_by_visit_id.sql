CREATE DEFINER=`root`@`localhost` PROCEDURE `get_owner_by_visit_id`(IN id_num int(10))
BEGIN
    SET @is_not_deleted = 0;
    SELECT *
    FROM
        (SELECT owners.owner_id, owners.id_number, owners.first_name, owners.last_name, owners.date_of_birth, owners.city, owners.city_2,
        owners.street, owners.street_2, owners.house_number, owners.house_number_2, owners.apartment_number, owners.apartment_number_2,
        owners.postal_code, owners.postal_code_2, owners.phone, owners.mobile, owners.mailbox, owners.email, owners.comment,
        owners.owner_created_date, owners.owner_user_id, owners.owner_archive
        FROM petadmin.visits
        INNER JOIN petadmin.animals ON petadmin.visits.animal_id=petadmin.animals.animal_id
        INNER JOIN petadmin.owners ON petadmin.animals.owner_id =petadmin.owners.owner_id
        WHERE (id_num = visit_id AND visit_archive=@is_not_deleted)) AS table1
    LEFT JOIN petadmin.users ON table1.owner_user_id = petadmin.users.Id;
END