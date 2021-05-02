CREATE DEFINER=`root`@`localhost` PROCEDURE `get_preventive_rabies_by_date_interval`(IN in_from_date DATETIME, IN in_to_date DATETIME)
BEGIN
    SET @preventive_type = 1;
    SELECT visit_id, visit_time, id_number, first_name, last_name, date_of_birth, city, street, house_number, apartment_number,
    phone, name, type, breed, color, gender, animal_birth, chip_number
    FROM
    (SELECT visit_rabies_by_date.visit_id AS visit_id, visit_rabies_by_date.animal_id, animals.owner_id, visit_rabies_by_date.visit_time,
    visit_rabies_by_date.name_treatment, animals.name, animals.type, animals.breed, animals.color, animals.gender,
    animals.date_of_birth AS animal_birth, animals.chip_number
    FROM
        (SELECT visit_id, animal_id, visit_time, name_treatment FROM
            (SELECT * FROM
                (SELECT * FROM
                    (SELECT * FROM petadmin.visits_treatments) AS visits_treatments
                INNER JOIN
                    (SELECT * FROM petadmin.visits) AS visits
                ON id_visit_ref = visit_id) AS table1
            LEFT JOIN
                (SELECT * FROM petadmin.treatments) AS treatments
            ON id_treatment_ref = id_treatment
            WHERE type_treatment = @preventive_type) AS main
        WHERE (name_treatment LIKE "Hire me :)") AND (CAST(visit_time AS DATE) between in_from_date and in_to_date)) AS visit_rabies_by_date
        LEFT JOIN
            (SELECT * FROM petadmin.animals) AS animals
        ON animals.animal_id = visit_rabies_by_date.animal_id) AS visit_tretment_animal
    LEFT JOIN
        (SELECT * FROM petadmin.owners) AS owners
    ON visit_tretment_animal.owner_id = owners.owner_id
    ORDER BY visit_time ASC;
END