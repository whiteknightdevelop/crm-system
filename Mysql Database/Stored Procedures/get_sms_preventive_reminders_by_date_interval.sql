CREATE DEFINER=`root`@`localhost` PROCEDURE `get_sms_preventive_reminders_by_date_interval`(IN in_from_date DATETIME, IN in_to_date DATETIME)
BEGIN
    SET @is_not_deleted = 0;
    SET @animal_not_archived = 0;
    SET @animal_active = 1;
    SET @owner_not_archived = 0;
    SET @preventive_type = 1;

    SELECT animal_id, next_treatment_name, date_reminder, remaining_days, name, type, breed, color, gender,
    active, sterilized, animal_archive, petadmin.owners.owner_id, first_name, last_name, phone, mobile, email, owner_archive
    FROM
        (SELECT id_visit_ref, id_treatment_ref, id_next_treatment, preventive_reminders_table.animal_id AS animal_id, next_treatment_name,
        date_reminder, remaining_days, is_preventive_treatment, visits_treatments_is_reminder_deleted, owner_id, created_date, name, type, breed, color,
        gender, date_of_birth, active, sterilized, date_of_sterilization, chip_number, chip_mark_date, comment, status, animal_user_id, animal_archive
        FROM
            (SELECT id_visit_ref, id_treatment_ref, id_next_treatment, animal_id, next_treatment_name, date_reminder, remaining_days,
            is_preventive_treatment, visits_treatments_is_reminder_deleted
            FROM
                (SELECT id_visit_ref, id_treatment_ref, id_next_treatment, animal_id, next_treatment_name,
                DATE_ADD(visit_time, INTERVAL days_treatment DAY) AS date_reminder, datediff(DATE_ADD(visit_time, INTERVAL days_treatment DAY),
                CURRENT_TIMESTAMP()) AS remaining_days, type_treatment AS is_preventive_treatment, visits_treatments_is_reminder_deleted
                FROM
                    (SELECT id_visit_ref, id_treatment_ref, table1.visit_id, table1.animal_id, table1.visit_time, table1.id_treatment,
                    table1.treatment_name, table1.days_treatment, table1.id_next_treatment, name_treatment AS next_treatment_name, 
                    table1.type_treatment, visits_treatments_is_reminder_deleted
                    FROM
                        (SELECT id_visit_ref, id_treatment_ref, visit_id, animal_id, visit_time, id_treatment, name_treatment AS treatment_name,
                        days_treatment, id_next_treatment,type_treatment, visits_treatments_is_reminder_deleted
                        FROM petadmin.visits
                        LEFT JOIN visits_treatments
                        ON petadmin.visits.visit_id = petadmin.visits_treatments.id_visit_ref
                        LEFT JOIN petadmin.treatments
                        ON petadmin.visits_treatments.id_treatment_ref = petadmin.treatments.id_treatment
                        WHERE (treatments.type_treatment = @preventive_type) AND (visits_treatments_is_reminder_deleted = @is_not_deleted)) AS table1
                    LEFT JOIN petadmin.treatments ON table1.id_next_treatment = petadmin.treatments.id_treatment) AS table2
                UNION
                SELECT null, null, id_reminder, animal_id_reminder AS animal_id, name_reminder AS next_treatment_name, date_reminder,
                datediff(date_reminder, CURRENT_TIMESTAMP()) AS remaining_days, null, null
                FROM petadmin.reminders) AS main
            WHERE main.date_reminder between in_from_date and in_to_date
            ORDER BY date_reminder ASC) AS preventive_reminders_table
        LEFT JOIN petadmin.animals ON preventive_reminders_table.animal_id = petadmin.animals.animal_id
        WHERE animal_archive=@animal_not_archived AND active=@animal_active) AS reminder_animals
    LEFT JOIN petadmin.owners ON reminder_animals.owner_id = petadmin.owners.owner_id WHERE owner_archive=@owner_not_archived
    ORDER BY date_reminder ASC;
END