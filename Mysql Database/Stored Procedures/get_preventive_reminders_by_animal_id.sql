CREATE DEFINER=`root`@`localhost` PROCEDURE `get_preventive_reminders_by_animal_id`(IN id_num int(10))
BEGIN
    SET @is_not_deleted = 0;
    SET @preventive_type = 1;
    SELECT id_visit_ref, id_treatment_ref, id_next_treatment, animal_id, next_treatment_name,
    DATE_ADD(visit_time, INTERVAL days_treatment DAY) AS date_reminder,
    datediff(DATE_ADD(visit_time, INTERVAL days_treatment DAY), CURRENT_TIMESTAMP()) AS remaining_days, type_treatment AS is_preventive_treatment,
    visits_treatments_is_reminder_deleted
    FROM
        (SELECT id_visit_ref, id_treatment_ref, table1.visit_id, table1.animal_id, table1.visit_time, table1.id_treatment,
        table1.treatment_name, table1.days_treatment, table1.id_next_treatment, name_treatment AS next_treatment_name,table1.type_treatment,
        visits_treatments_is_reminder_deleted
        FROM
            (SELECT id_visit_ref, id_treatment_ref, visit_id, animal_id, visit_time,
            id_treatment, name_treatment AS treatment_name, days_treatment, id_next_treatment,type_treatment,
            visits_treatments_is_reminder_deleted
            FROM petadmin.visits
            LEFT JOIN visits_treatments
            ON petadmin.visits.visit_id = petadmin.visits_treatments.id_visit_ref
            LEFT JOIN petadmin.treatments
            ON petadmin.visits_treatments.id_treatment_ref = petadmin.treatments.id_treatment
            WHERE (id_num = animal_id) AND (treatments.type_treatment = @preventive_type) AND (visits_treatments_is_reminder_deleted = @is_not_deleted)) AS table1
        LEFT JOIN petadmin.treatments ON table1.id_next_treatment = petadmin.treatments.id_treatment) AS table2
    UNION
    SELECT null, null, id_reminder, animal_id_reminder AS animal_id, name_reminder AS next_treatment_name, date_reminder,
    datediff(date_reminder, CURRENT_TIMESTAMP()) AS remaining_days, null, null
    FROM petadmin.reminders
    WHERE (id_num = animal_id_reminder) ORDER BY date_reminder DESC;
END