CREATE DEFINER=`root`@`localhost` PROCEDURE `get_followups_list_by_date_from`(IN in_from_date DATE)
BEGIN
    SET @not_archived = 0;

    SELECT *
    FROM
        (SELECT followup_id, animal_id, date_followup, cause_followup, status_followup,
        owner_id, created_date, name, type, breed, color, gender, date_of_birth AS animal_date_of_birth, active,
        sterilized, date_of_sterilization, chip_number, chip_mark_date, comment, status, animal_user_id, animal_archive
        FROM
            (SELECT followup_id, animal_id AS followup_animal_id, date_followup, cause_followup, status_followup FROM petadmin.followups
            WHERE date_followup >= in_from_date) AS followupsByDate
        Left JOIN
            (SELECT * FROM petadmin.animals) AS animals
        ON animals.animal_id = followupsByDate.followup_animal_id
        WHERE animal_archive = @not_archived) AS followup_animals
    Left JOIN
        (SELECT * FROM petadmin.owners) AS owners
    ON followup_animals.owner_id = owners.owner_id
    ORDER BY date_followup ASC;
END