CREATE DEFINER=`root`@`localhost` FUNCTION `update_old_treatment_reminders_as_deleted`(in_animal_id int(10), in_id_visit_ref int(10), in_id_treatment_ref int(10),
in_current_visit_time DATETIME) RETURNS int
    DETERMINISTIC
BEGIN
    SET @is_not_deleted = 0;
    SET @is_deleted = 1;

    UPDATE petadmin.visits_treatments a
    INNER JOIN
        (SELECT animal_id, id_visit_ref, id_treatment_ref, visits_treatments_sent, visits_treatments_is_reminder_deleted, visit_time
        FROM
            (SELECT * FROM petadmin.visits_treatments) AS visits_treatments
        INNER JOIN
            (SELECT * FROM petadmin.visits where animal_id=in_animal_id) AS visits
        ON visit_id=id_visit_ref
        WHERE id_treatment_ref = in_id_treatment_ref AND visits_treatments_is_reminder_deleted = @is_not_deleted) b
    ON a.id_visit_ref = b.id_visit_ref

    SET a.visits_treatments_is_reminder_deleted = @is_deleted
    WHERE a.id_treatment_ref = in_id_treatment_ref AND b.visit_time < in_current_visit_time;
    return 1;
END