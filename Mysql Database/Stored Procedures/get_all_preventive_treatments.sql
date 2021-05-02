CREATE DEFINER=`root`@`localhost` PROCEDURE `get_all_preventive_treatments`()
BEGIN
    SET @preventive_type = 1;
    SELECT null as visit_id, table1.id_treatment as id_treatment_ref, table1.treatment as name_treatment,
    table1.next_time_days as remaining_days, name_treatment AS name_next_treatment,
    null as user_id, null as user_first_name, null as user_last_name, null as user_username, null as user_email,
    null as user_password, null as user_create_time, null as user_gender, null as user_license
    FROM
        (SELECT id_treatment, name_treatment AS treatment, type_treatment, days_treatment AS next_time_days, id_next_treatment
        FROM petadmin.treatments
        WHERE (treatments.type_treatment = @preventive_type)) AS table1
    LEFT JOIN petadmin.treatments
    ON table1.id_next_treatment = petadmin.treatments.id_treatment
    WHERE (treatments.type_treatment = @preventive_type)
    ORDER BY treatment ASC;
END