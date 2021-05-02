CREATE DEFINER=`root`@`localhost` PROCEDURE `get_preventive_treatment_by_id`(IN id_num int(10))
BEGIN
    SET @preventive_type = 1;
    SELECT visit_id, id_treatment_ref, name_treatment, remaining_days, name_treatment AS name_next_treatment,
    user_id, user_first_name, user_last_name, user_username, user_email, user_password, user_create_time, user_gender, user_license
    FROM
        (SELECT visit_id, id_treatment_ref, table1.name_treatment, remaining_days, treatments.name_treatment AS name_next_treatment, treatment_user_id
        FROM
            (SELECT visits.visit_id, id_treatment_ref, visits.visit_time, visits.cause, treatments.id_treatment, treatments.name_treatment,
            datediff(DATE_ADD(visits.visit_time, INTERVAL treatments.days_treatment DAY), CURRENT_TIMESTAMP()) AS remaining_days,
            treatments.id_next_treatment, treatment_user_id
            FROM petadmin.visits
            LEFT JOIN visits_treatments ON petadmin.visits.visit_id = petadmin.visits_treatments.id_visit_ref
            LEFT JOIN petadmin.treatments ON petadmin.visits_treatments.id_treatment_ref = petadmin.treatments.id_treatment
            WHERE (id_num = id_treatment_ref) AND (treatments.type_treatment = @preventive_type)) AS table1
        LEFT JOIN petadmin.treatments
        ON petadmin.treatments.id_treatment = table1.id_next_treatment) AS table2
    LEFT JOIN petadmin.users ON table2.treatment_user_id = petadmin.users.Id;
END