CREATE DEFINER=`root`@`localhost` PROCEDURE `get_preventive_treatments_by_date_interval`(IN in_from_date DATETIME, IN in_to_date DATETIME)
BEGIN
    SELECT table_with_animal.owner_id, animal_id, name AS animal_name, visit_id, visit_time, id_treatment, name_treatment, days_treatment, date_reminder,
    remaining_days, id_next_treatment, next_name_treatment, type_treatment, visits_treatments_sent, first_name, last_name, phone, mobile
    FROM
        (SELECT owner_id, table_two_treatment.animal_id, name, visit_id, visit_time, id_treatment, name_treatment,
        days_treatment, date_reminder, remaining_days, id_next_treatment, next_name_treatment, type_treatment, visits_treatments_sent
        FROM
            (SELECT animal_id, visit_id, visit_time, table_one_treatment.id_treatment, table_one_treatment.name_treatment,
            table_one_treatment.days_treatment, DATE_ADD(visit_time, INTERVAL table_one_treatment.days_treatment DAY) AS date_reminder,
            datediff(DATE_ADD(visit_time, INTERVAL table_one_treatment.days_treatment DAY), CURRENT_TIMESTAMP()) AS remaining_days,
            table_one_treatment.id_next_treatment, treatments2.name_treatment AS next_name_treatment, table_one_treatment.type_treatment, visits_treatments_sent
            FROM
                (SELECT animal_id, visit_id, visit_time, id_treatment, name_treatment, days_treatment, id_next_treatment, type_treatment, visits_treatments_sent
                FROM
                    (SELECT visit_id, animal_id, visit_time, id_treatment_ref, visits_treatments_sent
                    FROM
                        (SELECT visit_id, animal_id, visit_time FROM petadmin.visits) AS visits
                    LEFT JOIN
                        (SELECT * FROM petadmin.visits_treatments) AS visits_treatments
                    ON visits.visit_id = visits_treatments.id_visit_ref
                    WHERE visits.visit_id = visits_treatments.id_visit_ref) AS main_visits_treatments
                LEFT JOIN
                    (SELECT * FROM petadmin.treatments) AS treatments
                ON main_visits_treatments.id_treatment_ref = treatments.id_treatment
                WHERE main_visits_treatments.id_treatment_ref = treatments.id_treatment) AS table_one_treatment
            LEFT JOIN
                (SELECT * FROM petadmin.treatments) AS treatments2
            ON table_one_treatment.id_next_treatment = treatments2.id_treatment) AS table_two_treatment
        LEFT JOIN
            (SELECT * FROM petadmin.animals) AS animals
        ON table_two_treatment.animal_id = animals.animal_id) AS table_with_animal
    LEFT JOIN
        (SELECT * FROM petadmin.owners) AS owners
    ON table_with_animal.owner_id = owners.owner_id
    where CAST(date_reminder AS DATE) between in_from_date and in_to_date;
END